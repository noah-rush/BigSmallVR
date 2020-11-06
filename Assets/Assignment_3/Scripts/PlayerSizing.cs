using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizing : MonoBehaviour
{
    [SerializeField]
    public GameObject m_PlayerParentObject;
    private float playerParentObjectStartMagnitude;

    [SerializeField]
    public GameObject m_PlayerTeleportDestination;
    private Vector3 teleportDestinationStartScale;

    public float playerSizeIncrement;

    // Start is called before the first frame update
    void Start()
    {
        teleportDestinationStartScale = m_PlayerTeleportDestination.transform.localScale;
        playerParentObjectStartMagnitude = m_PlayerParentObject.transform.localScale.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        // If the joystick is going up, increase player size
        if(Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0)
        {
            m_PlayerParentObject.transform.localScale += playerSizeIncrement * Vector3.one;//new Vector3(playerSizeIncrement, playerSizeIncrement, playerSizeIncrement);
        }
        // If the joystick is going down, decrease player size
        else if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0)
        {
            m_PlayerParentObject.transform.localScale -= playerSizeIncrement * Vector3.one;//new Vector3(playerSizeIncrement, playerSizeIncrement, playerSizeIncrement);
        }

        // Change the size of the teleport destination object to match player scale
        //float playerScale = m_PlayerParentObject.transform.localScale.magnitude / playerParentObjectStartMagnitude;
        //m_PlayerTeleportDestination.transform.localScale = playerScale * teleportDestinationStartScale;
    }
}
