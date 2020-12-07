using Normal.Realtime;
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
        if (other.gameObject.name == "Unlocker" || other.tag == "key")
            //if (other.tag == "key")
        {
            //Destroy(gameObject);
            RealtimeTransform jailDoorTransform = jailDoor.GetComponent<RealtimeTransform>();
            jailDoorTransform.RequestOwnership();
            Rigidbody rb = jailDoor.GetComponent<Rigidbody>();
            rb.useGravity = false;
            jailDoor.transform.position = jailDoorStartPos + new Vector3(0, 8, 0);

            //Realtime.Destroy(jailDoor);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
