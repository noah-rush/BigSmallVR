using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerInstructions : MonoBehaviour
{
    [SerializeField]
    GameObject controllerInHand, controllerInOtherHand;
    [SerializeField]
    GameObject bookHelp1, bookHelp2;

    OVRGrabber thisHand;
    OVRGrabbable slingshot;
    bool slingshotHelp = true;
    bool bookHelp = true;


    // Start is called before the first frame update
    void Start()
    {
        thisHand = GetComponent<OVRGrabber>();
        slingshot = GameObject.Find("Slingshot").GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slingshot.isGrabbed)
        {
            //slingText.text = slingshot.grabbedBy
            if (slingshot.grabbedBy.name.Equals(gameObject.name) && slingshotHelp)
            {
                controllerInOtherHand.SetActive(true);
                slingshotHelp = false;
            }
        }
        if(thisHand.grabbedObject != null){
        if(thisHand.grabbedObject.gameObject.tag == "Book" && bookHelp){
                bookHelp1.SetActive(true);
                bookHelp2.SetActive(true);
                bookHelp = false;
        }
    }


    }



}
