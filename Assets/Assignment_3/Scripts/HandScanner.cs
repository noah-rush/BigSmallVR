using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandScanner : MonoBehaviour
{
    [SerializeField]
    GameObject objectToMove;
    [SerializeField]
    Vector3 toMove;

    Vector3 targetPosition;

    [SerializeField]
    TextMeshPro playerSizeText;

    bool moveObject;

   

    PlayerSizingContinuous player;
    bool collided;
    GrabRequest requester; 
    Material m_Material;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = objectToMove.transform.position + toMove;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerSizingContinuous>();
        requester = objectToMove.GetComponent<GrabRequest>();

        moveObject = false;
        m_Material = GetComponent<Renderer>().material;
        
    }

    // Update is called once per frame
    public float shaderLerp = 0f;
    public float collideStart;
    void Update()
    {
        if(moveObject)
        {
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, targetPosition, Time.deltaTime * .3f);
        }
        if(collided){
            shaderLerp+= 0.008f;
        }else{
            if(shaderLerp > 0){
                shaderLerp-= 0.008f;
            }
        }
        float greenGlow = Mathf.Lerp(-1f, 0.95f, shaderLerp);
        // Shader.SetGlobalFloat("_fadeEnd", greenGlow);
        m_Material.SetFloat("_FadeEnd", greenGlow);
        // playerSizeText.text = player.scaleFactor.ToString("F2") + moveWalls.ToString();
    }

    
    IEnumerator OnTriggerEnter(Collider collider)
    {
        if ( collider.gameObject.name == "CubeKey" )
        {   
            collided = true;
            // collideStart = Time.time;
        }
        yield return new WaitForSeconds(2);
        if (collided)
        {
            // something
            openDoor();
            collided = false;
           

        }
    }

    void openDoor(){
            requester.request_ownership();
            moveObject = true;
            objectToMove.GetComponent<Rigidbody>().useGravity = false;
            objectToMove.GetComponent<Rigidbody>().isKinematic = true;
            objectToMove.GetComponent<Rigidbody>().detectCollisions = true;
            objectToMove.GetComponent<OVRGrabbable>().enabled = false;
    }

    void OnCollisionExit ()
    {
        collided = false;
    }
}
