using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableSizing : MonoBehaviour
{

    PlayerSizingContinuous player;

    OVRGrabbable grabbable;

    [SerializeField]
    float playerScaleRequired = 2f;

    float scaleFactor;

    // Default values if values not defined by player
    float maxScale = 3f;
    float minScale = .01f;
    float scaleRateLimit = .05f;

    Vector3 originalLocalScale;
    [SerializeField]
    bool resizable = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerSizingContinuous>();
        originalLocalScale = transform.localScale;
        scaleFactor = 1f;
        grabbable = gameObject.GetComponent<OVRGrabbable>();

        // Scaling behavior same as player's
        maxScale = player.GetMaxScale();
        minScale = player.GetMinScale();
        scaleRateLimit = player.GetScaleRateLimit();
    }

    // Update is called once per frame
    void Update()
    {
        /* If the object is too heavy, drop it */
        if (!grabbable.isGrabbed) return;
        CompareGrabbedObjectWeight();
        /* If the player is holding an object, scale it with the player */
        if (!resizable) return;
        ScaleGrabbedObject();
    }

    void CompareGrabbedObjectWeight()
    {
        // grabbingPlayer could come back null
        PlayerSizingContinuous grabbingPlayer = grabbable.grabbedBy.GetComponentInParent<PlayerSizingContinuous>();
        if(grabbingPlayer.scaleFactor < playerScaleRequired)
        {
            grabbable.grabbedBy.ForceRelease(grabbable);
        }
    }

    void ScaleGrabbedObject()
    {
        // object is not being grabbed, nothing to do
        // if (!grabbable.isGrabbed) return;

        if (IsPlayerScaling())
        {
            float newScale = GetnewScale() + scaleFactor;
            float playerNewScale = GetnewScale() + player.GetScaleFactor();
            if(playerNewScale > minScale && playerNewScale < maxScale)
            {
                if (newScale > minScale && newScale < maxScale)
                {
                    scaleFactor = newScale;
                    transform.localScale = originalLocalScale * scaleFactor;
                }
            }
        }
    }

    bool IsPlayerScaling()
    {
        return Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") != 0;
    }

    // [-1, 1] * rate limit
    float GetnewScale()
    {
        return Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") * scaleRateLimit;
    }
}
