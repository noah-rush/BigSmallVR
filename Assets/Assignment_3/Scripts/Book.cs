using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Book : MonoBehaviour
{
    // Start is called before the first frame update
    OVRGrabbable grabbable;
    // boolean open = false;
    // [SerializeField]
    // GameObject controllerHelp1, controllerHelp2;

    Transform axis;
    private RealtimeView _axisRealtime;
    private RealtimeTransform _axisTransform;
    // bool bookHelp = true;


    void Start()
    {
        axis = this.gameObject.transform.GetChild(0);
        grabbable = gameObject.GetComponent<OVRGrabbable>();
        _axisRealtime = axis.gameObject.GetComponent<RealtimeView>();
        _axisTransform = axis.gameObject.GetComponent<RealtimeTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbable.isGrabbed )
        {
            _axisTransform.RequestOwnership();
            axis.eulerAngles = new Vector3(axis.rotation.eulerAngles.x, axis.rotation.eulerAngles.y,  axis.rotation.eulerAngles.z - OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger));
            axis.eulerAngles = new Vector3(axis.rotation.eulerAngles.x, axis.rotation.eulerAngles.y,  axis.rotation.eulerAngles.z + OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger));

            grabbable.grabbedBy.gameObject.GetComponent<ControllerInstructions>().ShowBookHelp();
        }
    }
}
