﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerInstructions : MonoBehaviour
{
    [SerializeField]
    GameObject controllerInHand, controllerInOtherHand;

    OVRGrabbable slingshot;
    bool slingshotHelp = true;
    bool bookHelp = false;


    // Start is called before the first frame update
    void Start()
    {
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
    }


    public void ShowBookHelp(string _name)
    {
        if (bookHelp) return;
        controllerInOtherHand.SetActive(true);
        controllerInHand.GetComponent<ControllerBehavior>().SetBookTextHelp();
        controllerInHand.SetActive(true);
        controllerInOtherHand.GetComponent<ControllerBehavior>().SetBookTextHelp();
        bookHelp = true;
    }
}
