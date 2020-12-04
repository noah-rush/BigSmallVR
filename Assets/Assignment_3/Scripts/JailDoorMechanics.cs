using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailDoorMechanics : MonoBehaviour
{
    GameObject jailDoor;
    Vector3 startPos, jailDoorStartPos;
    float heightDiff = 0f;

    // Start is called before the first frame update
    void Start()
    {
        jailDoor = GameObject.Find("Jail Door");

        startPos = transform.position;
        jailDoorStartPos = jailDoor.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        heightDiff = transform.position.y - startPos.y;
        jailDoor.transform.position = jailDoorStartPos + new Vector3(0, heightDiff, 0);
    }
}
