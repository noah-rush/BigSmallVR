using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableSizing : MonoBehaviour
{

    PlayerSizingContinuous player;

    OVRGrabbable grabbable;

    [SerializeField]
    float playerScaleRequired = 2f;

    public float scaleFactor = 1f;

    // Default values if values not defined by player
    float maxScale = 3f;
    float minScale = .01f;

    float playerMinScale;
    float playerMaxScale;

    float scaleRateLimit;
    Vector3 originalLocalScale;
    [SerializeField]
    bool resizable = true;
    bool hasBeenGrabbed = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerSizingContinuous>();
        originalLocalScale = transform.localScale;
        grabbable = gameObject.GetComponent<OVRGrabbable>();

        // Scaling behavior same as player's
        playerMaxScale = player.GetMaxScale();
        playerMinScale = player.GetMinScale();
        scaleRateLimit = player.GetScaleRateLimit();
    }

    // Update is called once per frame
    void Update()
    {
        /* If the object is too heavy, drop it */
        // playerScaleRequired = scaleFactor * playerScaleRequired;
        if (!grabbable.isGrabbed){
            hasBeenGrabbed = false;
            return;
        }
        if(!hasBeenGrabbed){
            setScaleRateLimit();
            hasBeenGrabbed = true;
        }
        CompareGrabbedObjectWeight();
        /* If the player is holding an object, scale it with the player */
        if (!resizable) return;
        ScaleGrabbedObject();
    }
    void setScaleRateLimit(){
        PlayerSizingContinuous grabbingPlayer = grabbable.grabbedBy.GetComponentInParent<PlayerSizingContinuous>();
        scaleRateLimit = player.GetScaleRateLimit() * scaleFactor / grabbingPlayer.scaleFactor;
        // Debug.log(scale)
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
            float newScale = GetNewScale() + scaleFactor;
            float playerNewScale = player.GetNewScale() + player.GetScaleFactor();
            if(playerNewScale > playerMinScale && playerNewScale < playerMaxScale)
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
    float GetNewScale()
    {
        return Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") * scaleRateLimit;
    }
}
