using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableSizing : MonoBehaviour
{
    [SerializeField]
    OVRGrabbable m_Grabbable;

    public Vector3 maxObjectSize;
    public Vector3 minObjectSize;
    public float objectSizeIncrement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Grabbable.isGrabbed)
        {
            // If the joystick is going up, increase player size
            if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0)
            {
                if (m_Grabbable.gameObject.transform.localScale.y < maxObjectSize.y)
                {
                    m_Grabbable.gameObject.transform.localScale += objectSizeIncrement * Vector3.one;
                }
            }
            // If the joystick is going down, decrease player size
            else if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0)
            {
                if (m_Grabbable.gameObject.transform.localScale.y > minObjectSize.y)
                {
                    m_Grabbable.gameObject.transform.localScale -= objectSizeIncrement * Vector3.one;
                }
            }
        }
    }
}
