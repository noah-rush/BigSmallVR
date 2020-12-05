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
    private Realtime _realtime;
    private OVRGrabber grabbingPlayer;
    private DistanceGrabbable crossbow;

    GameObject currBullet;

    bool needsReload = false;


    void Start()
    {
        crossbow = GetComponentInParent<DistanceGrabbable>();
        currBullet = null;
        _realtime = GetComponentInParent<Realtime>();
    }

    void OnTriggerStay(Collider collision)
    {
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
            currBulletScript.slingshot = GetComponentInParent<GrabbableSizing>();
            currBulletScript.playerHand = collision.transform;
            currBulletScript.grabbingPlayer = grabbingPlayer;
            currBulletScript.slingshotTip = crossbowTip;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            currBullet = null;
        }
    }
}
