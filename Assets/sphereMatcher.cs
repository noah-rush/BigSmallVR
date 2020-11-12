using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;

public class sphereMatcher : MonoBehaviour
{


    [SerializeField]
    public Vector3 maxPlayerSize;
    [SerializeField]
    public Vector3 minPlayerSize;

    public float playerSizeIncrement;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
   
        

        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0)
        {
            if(gameObject.transform.localScale.y < maxPlayerSize.y)
            {
                gameObject.transform.localScale += playerSizeIncrement * Vector3.one;
                gameObject.transform.position = gameObject.transform.position + Vector3.up * playerSizeIncrement / 2.0f;

            

            }
        }
        // If the joystick is going down, decrease player size
        else if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0)
        {
            if (gameObject.transform.localScale.y > minPlayerSize.y)
            {
                gameObject.transform.localScale -= playerSizeIncrement * Vector3.one;
                gameObject.transform.position = gameObject.transform.position - Vector3.up * playerSizeIncrement / 2.0f;
                
            }
        }
    }
}
