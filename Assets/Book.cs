using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    // Start is called before the first frame update
    OVRGrabbable grabbable;
    // boolean open = false;
    Transform axis;
    void Start()
    {
        axis = this.gameObject.transform.GetChild(0);
        grabbable = gameObject.GetComponent<OVRGrabbable>();

    }

    // Update is called once per frame
    void Update()
    {
    	
		// OVRInput.Get(OVRInput.RawButton.LIndexTrigger);
        if (grabbable.isGrabbed){
                axis.eulerAngles = new Vector3(axis.rotation.eulerAngles.x,axis.rotation.eulerAngles.y,  axis.rotation.eulerAngles.z - OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger));   
                axis.eulerAngles = new Vector3(axis.rotation.eulerAngles.x,axis.rotation.eulerAngles.y,  axis.rotation.eulerAngles.z + OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger));   
                
        }
    }
}
