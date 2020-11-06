using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizingContinuous : MonoBehaviour
{
    [SerializeField]
    public GameObject m_PlayerController;

    public float playerSizeIncrement;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the joystick is going up, increase player size
        if(Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0)
        {
            gameObject.transform.localScale += playerSizeIncrement * Vector3.one;
        }
        // If the joystick is going down, decrease player size
        else if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0)
        {
            gameObject.transform.localScale -= playerSizeIncrement * Vector3.one;
        }

        //gameObject.transform.position = gameObject.transform.position - m_PlayerController.transform.position;
    }
}
