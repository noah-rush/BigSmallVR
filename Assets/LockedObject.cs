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
    PlayerSizingContinuous grabbingPlayer;

    [SerializeField]
    bool changeColorOnGrab = true;


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
        if(grabbable.isGrabbed)
        {
            grabbingPlayer = grabbable.grabbedBy.GetComponentInParent<PlayerSizingContinuous>();
            grabbingPlayer.vibrateRightHand = true;
            grabbingPlayer.vibratePower = transform.position.y - 0.9f;
            if (changeColorOnGrab) gameObject.GetComponent<Renderer> ().material.color = Color.green;
        }
        else
        {
            if (changeColorOnGrab) gameObject.GetComponent<Renderer> ().material.color = Color.red;
            grabbingPlayer.vibrateRightHand = false;
 
 
        }

    }
   

    void LateUpdate()
    {
        transform.rotation = defaultRotation;
    }
}
