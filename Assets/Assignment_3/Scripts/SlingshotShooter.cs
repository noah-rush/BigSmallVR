using OculusSampleFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;


public class SlingshotShooter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform crossbowTip;
    private Realtime _realtime;
    private OVRGrabber grabbingPlayer;
    private DistanceGrabbable crossbow;

    bool needsReload = false;


    void Start()
    {
        crossbow = gameObject.GetComponent<DistanceGrabbable>();
        _realtime = GetComponent<Realtime>();
    }

    private bool GetTriggerPulled()
    {
        bool pulledTrigger = false;
        if (grabbingPlayer.m_controller == OVRInput.Controller.LTouch && OVRInput.Get(OVRInput.RawButton.LIndexTrigger)
            || grabbingPlayer.m_controller == OVRInput.Controller.RTouch && OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            pulledTrigger = true;
        }

        return pulledTrigger;
    }

    private void Reload()
    {
        // check if the player has let go of the trigger
        if (needsReload)
        {
            if (grabbingPlayer.m_controller == OVRInput.Controller.LTouch && OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) == 0f
                || grabbingPlayer.m_controller == OVRInput.Controller.RTouch && OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) == 0f)
            {
                needsReload = false;
            }
        }
    }

    private void Shoot()
    {
        // /   / force a reload 
        needsReload = true;
        // fire bullet
        GameObject currBullet = Realtime.Instantiate("Bullet",                 // Prefab name
                             position: crossbowTip.position,          // Start 1 meter in the air
                             rotation: crossbowTip.rotation, // No rotation
                             ownedByClient: false,   // Make sure the RealtimeView on this prefab is NOT owned by this client
                             preventOwnershipTakeover: false,                // DO NOT prevent other clients from calling RequestOwnership() on the root RealtimeView.
                             useInstance: _realtime);           // Use the instance of Realtime that fired the didConnectToRoom event.

        float crossbowScaleFactor = gameObject.GetComponent<GrabbableSizing>().scaleFactor;
        currBullet.transform.localScale = currBullet.transform.localScale * crossbowScaleFactor;
        currBullet.GetComponent<Rigidbody>().AddForce((currBullet.transform.position - transform.position) * (40 /* * crossbowScaleFactor*/), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        /* Handle firing the gun */
        grabbingPlayer = crossbow.grabbedBy;
        if (!grabbingPlayer) return;

        // check if the player has let go of the trigger
        Reload();

        // check if the player is pulling the trigger more than 50% of the way
        bool pulledTrigger = GetTriggerPulled();

        // if the player pulled the trigger and has let go of the trigger since the last time, shoot
        if (pulledTrigger && !needsReload)
        {
            Shoot();
        }
    }
}
