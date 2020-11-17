using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;
public class LockedObject : MonoBehaviour
{
    // Start is called before the first frame update
    OVRGrabbable grabbable;
    Quaternion defaultRotation;

    void Start()
    {
        grabbable = gameObject.GetComponent<OVRGrabbable>();

    }

    void Awake()
    {
        defaultRotation = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        // Transform grabbingHand = grabbable.grabbedBy.GetComponentInParent<Transform>();
        if(grabbable.isGrabbed)
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
             gameObject.GetComponent<Renderer> ().material.color = Color.green;
            // OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }
        else
        {
             gameObject.GetComponent<Renderer> ().material.color = Color.red;

            // OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);

        }

    }
   

    void LateUpdate()
    {
        transform.rotation = defaultRotation;
    }
}
