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

    public GameObject debugger;

    [SerializeField]
    float scaleRateLimit = 0.1f;
    float scaleFactor = 1f;
    float minPlayerScale = 0.1f;
    float maxPlayerScale = 10f;
    Vector3 playerInitialScale;

    void Start()
    {
        XRDevice.SetTrackingSpaceType(UnityEngine.XR.TrackingSpaceType.RoomScale);
        playerInitialScale = transform.localScale;
    }
    void ReadjustHeadCamera()
    {
        /* Reposition the player objects to be where the head is */
        // detach child
        m_OVRCameraRig.transform.parent = null;
        // change position
        transform.position = new Vector3(m_CenterEyeAnchor.transform.position.x, transform.position.y, m_CenterEyeAnchor.transform.position.z);
        // reattach child
        m_OVRCameraRig.transform.parent = m_PlayerController.transform;
    }
    bool IsPlayerScaling()
    {
        return Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") != 0;
    }
    // [-1, 1] * rate limit
    float GetNewScale()
    {
        return Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") * scaleRateLimit;
    }
    public float GetScaleRateLimit()
    {
        return scaleRateLimit;
    }
    public float GetMinScale()
    {
        return minPlayerScale;
    }
    public float GetMaxScale()
    {
        return maxPlayerScale;
    }
    // Update is called once per frame
    void ResizePlayer()
    {
        /* Handle player resizing */
        if (IsPlayerScaling())
        {
            float newScaleFactor = scaleFactor + GetNewScale();
            if(newScaleFactor > minPlayerScale && newScaleFactor < maxPlayerScale)
            {
                int layerMask = 1 << 9;
                layerMask = ~layerMask;
                RaycastHit hit;
                if(GetNewScale() <0 || !Physics.Raycast(m_OVRCameraRig.transform.position, Vector3.up, out hit, 0.04f, layerMask))
                {
                    scaleFactor = newScaleFactor;
                    transform.localScale = playerInitialScale * scaleFactor;
                    transform.position += GetNewScale() / 2.0F * Vector3.up;
                }
            }
        }
    }

    void Update()
    {
        ReadjustHeadCamera();
        ResizePlayer();


    }
}
