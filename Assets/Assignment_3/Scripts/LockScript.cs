using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    [SerializeField]
    GameObject jailDoor;
    Vector3 jailDoorStartPos;
    // Start is called before the first frame update
    void Start()
    {
        jailDoorStartPos = jailDoor.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "key")
        {
            //Rigidbody rb = jailDoor.GetComponent<Rigidbody>();
            //rb.isKinematic = true;
            jailDoor.transform.position = jailDoorStartPos + new Vector3(0, 8, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
