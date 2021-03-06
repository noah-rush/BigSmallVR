using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    // Start is called before the first frame update
    bool lit = false;
    Transform matchParent;
    OVRGrabbable grabbable;
    Quaternion defaultRotation;
    PlayerSizingContinuous grabbingPlayer;
    bool collided = false;
    Vector3 swipeStart;
    AudioSource matchSound;
    private MatchSync _matchSync;

    void Start()
    {
        matchSound = GetComponent<AudioSource>();
        matchParent = transform.parent;
        grabbable = matchParent.gameObject.GetComponent<OVRGrabbable>();
        _matchSync = GetComponent<MatchSync>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool isLit()
    {
        return lit;
    }
    // void OnTriggerEnter(Collider other)
    // {
    //     if(other.gameObject.name == "MatchStrip" && grabbable.isGrabbed){
    //         // Transform matchParent = transform.parent;
    //         grabbingPlayer = grabbable.grabbedBy.GetComponentInParent<PlayerSizingContinuous>();
    //         grabbingPlayer.vibrateRightHand = true;
    //         grabbingPlayer.vibratePower = 0.5f;
    //         // matchParent.transform.GetChild(1).gameObject.SetActive(true);
    //     }
    // }
    void OnTriggerExit ()
    {
        collided = false;
        grabbingPlayer.vibrateRightHand = false;
        // grabbingPlayer.vibratePower = 0.5f;
    }
    IEnumerator OnTriggerEnter(Collider collider)
    {
        if ( collider.gameObject.name == "MatchStrip" )
        {
            collided = true;
            grabbingPlayer = grabbable.grabbedBy.GetComponentInParent<PlayerSizingContinuous>();
            grabbingPlayer.vibrateRightHand = true;
            grabbingPlayer.vibratePower = 0.5f;
            swipeStart = transform.position;
        }
        yield return new WaitForSeconds(0.5f);
        if (collided && !lit)
        {
            if(Vector3.Distance(swipeStart, transform.position) > 0.3)
            {
                grabbingPlayer.vibrateRightHand = false;
                lit = true;
                matchParent.transform.GetChild(1).gameObject.SetActive(true);
                StartCoroutine("litMatch");
                _matchSync.SetMatch();
                matchSound.Play();
                
            }
        }
    }
    IEnumerator litMatch(){
        yield return new WaitForSeconds(6.0f);
        lit = false;
        matchParent.transform.GetChild(1).gameObject.SetActive(false);
        // StartCoroutine(ExampleCoroutine());
    }
    public void light(){
        lit = true;
        matchParent.transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine("litMatch");
        matchSound.Play();

    }


}
