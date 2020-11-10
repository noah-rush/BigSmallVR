using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;

public class PlayerSizingContinuous : MonoBehaviour
{
    [SerializeField]
    public GameObject m_PlayerController;

    [SerializeField]
    public GameObject m_CenterEyeAnchor;

    [SerializeField]
    public GameObject m_OVRCameraRig;

    [SerializeField]
    public Vector3 maxPlayerSize;
    [SerializeField]
    public Vector3 minPlayerSize;

    public float playerSizeIncrement;

    // Start is called before the first frame update
    void Start()
    {
        XRDevice.SetTrackingSpaceType(UnityEngine.XR.TrackingSpaceType.RoomScale);
    }

    // Update is called once per frame
    void Update()
    {
        /* Reposition the player objects to be where the head is */
        // detach child
        m_OVRCameraRig.transform.parent = null;
        // change position
        transform.position = new Vector3(m_CenterEyeAnchor.transform.position.x, transform.position.y, m_CenterEyeAnchor.transform.position.z);
        // reattach child
        m_OVRCameraRig.transform.parent = m_PlayerController.transform;
        
        /* Handle player resizing */
        // If the joystick is going up, increase player size
        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0)
        {
            if(gameObject.transform.localScale.y < maxPlayerSize.y)
            {
                gameObject.transform.localScale += playerSizeIncrement * Vector3.one;
            }
        }
        // If the joystick is going down, decrease player size
        else if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0)
        {
            if (gameObject.transform.localScale.y > minPlayerSize.y)
            {
                gameObject.transform.localScale -= playerSizeIncrement * Vector3.one;
            }
        }
    }
}
