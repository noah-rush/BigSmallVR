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

    public float playerSizeIncrement = 0.15f;

    public float scaleFactor = 1f;

    public float minPlayerScale = 0.5f;

    public float maxPlayerScale = 25f;

    private Vector3 playerInitialScale;

    // Start is called before the first frame update
    void Start()
    {
        // track the floor
        XRDevice.SetTrackingSpaceType(UnityEngine.XR.TrackingSpaceType.RoomScale);

        playerInitialScale = transform.localScale;
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
        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") != 0)
        {
            float newScaleFactor = scaleFactor + Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") * playerSizeIncrement;
            if(newScaleFactor > minPlayerScale && newScaleFactor < maxPlayerScale)
            {
                scaleFactor = newScaleFactor;
                transform.localScale = playerInitialScale * scaleFactor;
            }
        }
    }
}
