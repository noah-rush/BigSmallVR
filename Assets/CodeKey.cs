using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeKey : MonoBehaviour
{

    public float endstop;//how far down you want the button to be pressed before it triggers
    public bool Pressed;

    private Transform Location;
    private Vector3 StartPos;
    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        Location = transform;
        StartPos = Location.position;
    }
    void Update()
    {

        if (Mathf.Abs(Location.position.z - StartPos.z) > endstop ) //check to see if the button has been pressed all the way down
        {
            // Location.position = new Vector3(Location.position.x, Location.position.y, endstop + StartPos.z);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Pressed = true;//update pressed
            endPos = Location.position;
            StartCoroutine("buttonReset");
            
        }else{

        }

       
    }
    // void OnCollisionExit(Collision collision)//check for when to unlock the button
    // {
    //     Debug.Log("unlock");
    //     if (collision.gameObject.name != "CodeBox")
    //     {
    //         GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ; //Remove Y movement constraint.
    //         Pressed = false;//update pressed
    //         StartCoroutine("buttonReset");
    //     }
    // }
    IEnumerator buttonReset(){
         for (float ft = 0f; ft <= 1f; ft += 0.1f) 
        {
            Location.position = Vector3.Lerp(endPos, StartPos, ft);

            yield return new WaitForSeconds(.1f);
        }
    }


}