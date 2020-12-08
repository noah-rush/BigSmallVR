using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailDoorMechanics : MonoBehaviour
{
    [SerializeField]
    float jailDoorHeightDelay = 3f;
    GameObject jailDoor;
    Vector3 startPos, jailDoorStartPos;
    float heightDiff = 0f;
    OVRGrabbable grabbable;

    // Start is called before the first frame update
    void Start()
    {
        jailDoor = GameObject.Find("Jail Door");

        startPos = transform.position;
        jailDoorStartPos = jailDoor.transform.position;
        grabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {

        heightDiff = transform.position.y - startPos.y;
        if(heightDiff > 0)
        {
            jailDoor.transform.position = jailDoorStartPos + new Vector3(0, heightDiff / 3, 0);
        }
    }
}
