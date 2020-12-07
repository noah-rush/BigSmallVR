using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Normal.Realtime;

public class DogDoorButton : MonoBehaviour
{
    public GameObject dogDoor;

    public float endstop;//how far down you want the button to be pressed before it triggers
    public bool Pressed = false;

    private Transform Location;
    private Vector3 StartPos;
    private Vector3 endPos;
    private string number;
    CodeBox codeBox;
    TMP_Text entryPanel;
    // Start is called before the first frame update
    void Start()
    {
        Location = transform;
        StartPos = Location.position;
        //number = transform.GetChild(0).GetComponent<TMP_Text>().text;
        //codeBox = transform.parent.gameObject.GetComponent<CodeBox>();
        Physics.IgnoreCollision(codeBox.ColorPlane.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
    }
    void Update()
    {

        if (Mathf.Abs(Location.position.z - StartPos.z) > endstop && !Pressed) //check to see if the button has been pressed all the way down
        {
            // Location.position = new Vector3(Location.position.x, Location.position.y, endstop + StartPos.z);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Pressed = true;//update pressed
            //codeBox.addToKeyCode(number);
            RealtimeTransform dogDoorTransform = dogDoor.GetComponent<RealtimeTransform>();
            dogDoorTransform.RequestOwnership();
            HingeJoint dogDoorHinge = dogDoor.GetComponent<HingeJoint>();
            dogDoorHinge.useSpring = true;
            endPos = Location.position;
            StartCoroutine("buttonReset");



        }
        else
        {

        }


    }
    IEnumerator buttonReset()
    {
        yield return new WaitForSeconds(.4f);
        for (float ft = 0f; ft <= 1f; ft += 0.01f)
        {
            Location.position = Vector3.Lerp(endPos, StartPos, ft);
            yield return new WaitForSeconds(.005f);
        }
        Pressed = false;
        GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ; //Remove Y movement constraint.

    }


}