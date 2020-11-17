using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonInteraction : MonoBehaviour
{
    
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }

    [SerializeField]
    GameObject midWallParent;

    bool moveWalls;

    public float pressLength;
    public bool pressed;
    public ButtonEvent downEvent;

    Vector3 startPos;
    Rigidbody rb;


    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        moveWalls = false;
    }

    void Update()
    {
        // If our distance is greater than what we specified as a press
        // set it to our max distance and register a press if we haven't already
        float distance = Mathf.Abs(transform.position.z - startPos.z);
        if (distance >= pressLength)
        {
            // Prevent the button from going past the pressLength
            transform.position = new Vector3(transform.position.x, startPos.y - pressLength, transform.position.z);
            if (!pressed)
            {
                pressed = true;
                moveWalls = true;

            }
        } else
        {
            // If we aren't all the way down, reset our press
            pressed = false;
        }
        // Prevent button from springing back up past its original position
        if (transform.position.z > startPos.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z);
        }

        if (moveWalls)
            midWallParent.transform.position = Vector3.Lerp(midWallParent.transform.position, new Vector3(0,10,0), Time.deltaTime * 1.2f);

    }
}
