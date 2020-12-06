using OculusSampleFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;


public class SlingshotShooter : MonoBehaviour
{

    [SerializeField]
    Transform crossbowTip;
    [SerializeField]
    Transform leftStringPos;
    [SerializeField]
    Transform rightStringPos;

    Realtime _realtime;
    OVRGrabber grabbingPlayer;
    DistanceGrabbable crossbow;
    LineRenderer stringRenderer;
    GrabbableSizing slingshot;
    float stringWidth = 0.2f;

    GameObject currBullet;

    bool needsReload = false;


    void Start()
    {
        slingshot = GetComponentInParent<GrabbableSizing>();
        crossbow = GetComponentInParent<DistanceGrabbable>();
        currBullet = null;
        _realtime = GetComponentInParent<Realtime>();

        // set up the string
        stringRenderer = GetComponent<LineRenderer>();
        SetStringWidth();
        SetStringPos();
    }

    void OnTriggerStay(Collider collision)
    {
        if (!grabbingPlayer) return;

        if (currBullet == null 
            && ((collision.gameObject.tag == "LeftHand" 
            && OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
            || (collision.gameObject.tag == "RightHand"
            && OVRInput.Get(OVRInput.RawButton.RIndexTrigger))))
        {
            currBullet = Realtime.Instantiate("SlingshotBullet",                 // Prefab name
                             position: collision.transform.position,          // Start 1 meter in the air
                             rotation: crossbowTip.rotation, // No rotation
                             ownedByClient: false,   // Make sure the RealtimeView on this prefab is NOT owned by this client
                             preventOwnershipTakeover: false,                // DO NOT prevent other clients from calling RequestOwnership() on the root RealtimeView.
                             useInstance: _realtime);           // Use the instance of Realtime that fired the didConnectToRoom event.

            SlingshotBullet currBulletScript = currBullet.GetComponent<SlingshotBullet>();
            currBulletScript.slingshot = slingshot;//GetComponentInParent<GrabbableSizing>();
            currBulletScript.playerHand = collision.transform;
            currBulletScript.grabbingPlayer = grabbingPlayer;
            currBulletScript.slingshotTip = crossbowTip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // set the string pos based on if the player is pulling it
        if (currBullet != null)
        {
            // set the middle pos to the ball vector
            SetStringPos(currBullet.transform.position);
        }
        else
        {
            // set the middle pos to between the two branches
            SetStringPos();
        }
        SetStringWidth();

        /* Handle firing the gun */
        grabbingPlayer = crossbow.grabbedBy;
        if (!grabbingPlayer)
        {
            return;
        }

        // Check if player let go of the ball
        if (currBullet != null
            && (grabbingPlayer.m_controller == OVRInput.Controller.LTouch && OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) <= 0.05f // players holding in left hand lets go of ball with right
            || grabbingPlayer.m_controller == OVRInput.Controller.RTouch && OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) <= 0.05f))
        {
            SetStringPos();
            currBullet = null;
        }
    }

    void SetStringWidth()
    {
        stringRenderer.startWidth = stringWidth * slingshot.scaleFactor;
        stringRenderer.endWidth = stringWidth * slingshot.scaleFactor;
    }
    void SetStringPos()
    {
        stringRenderer.SetPosition(0, leftStringPos.transform.position);
        stringRenderer.SetPosition(1, Vector3.Lerp(rightStringPos.transform.position, leftStringPos.transform.position, 0.5f));
        stringRenderer.SetPosition(2, rightStringPos.transform.position);
    }

    void SetStringPos(Vector3 middlePos)
    {
        stringRenderer.SetPosition(0, leftStringPos.transform.position);
        stringRenderer.SetPosition(1, middlePos);
        stringRenderer.SetPosition(2, rightStringPos.transform.position);
    }
}
