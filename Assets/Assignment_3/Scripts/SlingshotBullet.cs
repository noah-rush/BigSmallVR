using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotBullet : MonoBehaviour
{
    public Transform playerHand;
    public Transform slingshotTip;
    public GrabbableSizing slingshot;
    public OVRGrabber grabbingPlayer;

    Vector3 originalScale;
    bool launched;
    int launchForce = 30;
    float maxStringPulled = 2.5f;

    float lifeSpan = 3f;
    // Start is called before the first frame update
    void Start()
    {
        launched = false;
        originalScale = transform.localScale;
        transform.localScale = transform.localScale * slingshot.scaleFactor;
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.SetControllerVibration(1, 0, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(1, 0, OVRInput.Controller.RTouch);
        if (!launched)
        {
            if (playerHand && slingshot && grabbingPlayer)
            {
                // if the player lets go, launch the ball
                if ((grabbingPlayer.m_controller == OVRInput.Controller.LTouch && OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) <= 0.05f // players holding in left hand lets go of ball with right
                || grabbingPlayer.m_controller == OVRInput.Controller.RTouch && OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) <= 0.05f))
                {
                    launched = true;
                    Rigidbody rb = GetComponent<Rigidbody>();
                    rb.mass = slingshot.scaleFactor;
                    rb.AddForce((slingshotTip.position - transform.position) * launchForce, ForceMode.Impulse);
                    Destroy(gameObject, lifeSpan); 

                }
                else // else move the ball with the players hand
                {
                    // move and scale
                    transform.position = playerHand.position;
                    transform.localScale = originalScale * slingshot.scaleFactor;
                    // vibrate if the ball is far away from slingshot tip
                    float stringPulled = (slingshotTip.position - transform.position).magnitude / slingshot.scaleFactor;
                    if(stringPulled >= maxStringPulled)
                    {
                        // vibrate
                        if(grabbingPlayer.m_controller == OVRInput.Controller.LTouch)
                        {
                            // vibrate left controller
                            OVRInput.SetControllerVibration(1, Mathf.Min(0.1f * stringPulled, 1f), OVRInput.Controller.RTouch);

                        } else if(grabbingPlayer.m_controller == OVRInput.Controller.RTouch)
                        {
                            // vibrate right controller
                            OVRInput.SetControllerVibration(1, Mathf.Min(0.1f * stringPulled, 1f), OVRInput.Controller.LTouch);
                        }
                    }
                }
            }
        }
    }
}
