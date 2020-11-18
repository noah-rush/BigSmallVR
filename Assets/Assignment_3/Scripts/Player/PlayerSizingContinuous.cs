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



    public GameObject debugger;

    [SerializeField]
    float scaleRateLimit = 0.0001f;
    public float scaleFactor = 1f; // Used in grabbable objects to check if they can be grabbed by player
    float minPlayerScale = 0.25f;
    float maxPlayerScale = 10f;
    Vector3 playerInitialScale;
    public bool vibrateRightHand = false;
    public bool vibrateLeftHand = false;
    public float vibratePower = 0.0f;

    void Start()
    {
        // track the floor
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
    public float GetNewScale()
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
    public float GetScaleFactor()
    {
        return scaleFactor;
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
                Vector3 raycastStart = new Vector3(transform.position.x, gameObject.GetComponent<Collider>().bounds.max.y, transform.position.z);
                if (GetNewScale() < 0)
                {
                    scaleFactor = newScaleFactor;
                    transform.localScale = playerInitialScale * scaleFactor;
                    transform.position += GetNewScale() / 2.0F * Vector3.up;

                }
                else
                {
                    if(!Physics.Raycast(raycastStart, Vector3.up, out hit, GetNewScale() + GetNewScale() / 2.0f, layerMask))
                    {
                        scaleFactor = newScaleFactor;
                        transform.localScale = playerInitialScale * scaleFactor;
                        transform.position += GetNewScale() / 2.0F * Vector3.up;
                    }
                    else
                    {
                        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
                        // OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);

                    }
                }

            }
        }
    }

    void Update()
    {
        // Clear Haptics
        OVRInput.SetControllerVibration(1, 0, OVRInput.Controller.RTouch);
        ReadjustHeadCamera();
        ResizePlayer();
        if(vibrateRightHand){
            OVRInput.SetControllerVibration(vibratePower/2.0f, 1, OVRInput.Controller.RTouch);
        }
        if(vibrateLeftHand){
            OVRInput.SetControllerVibration(vibratePower/2.0f, 1, OVRInput.Controller.LTouch);
        }


    }

}
