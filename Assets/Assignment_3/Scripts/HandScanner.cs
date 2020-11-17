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

    [SerializeField]
    float maxPlayerScale = 1f;

    PlayerSizingContinuous player;
    bool collided;
    GrabRequest requester; 
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = objectToMove.transform.position + toMove;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerSizingContinuous>();
        requester = objectToMove.GetComponent<GrabRequest>();

        moveObject = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveObject)
        {
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, targetPosition, Time.deltaTime * .3f);
        }

        // playerSizeText.text = player.scaleFactor.ToString("F2") + moveWalls.ToString();
    }


    IEnumerator OnTriggerEnter(Collider collider)
    {
        if (player.scaleFactor <= maxPlayerScale && collider.gameObject.name == "GrabVolumeSmall")
        {
        collided = true;
        }
        yield return new WaitForSeconds(2);
        if (collided)
        {
            // something
            moveObject = true;
            objectToMove.GetComponent<Rigidbody>().useGravity = false;
            objectToMove.GetComponent<Rigidbody>().isKinematic = true;
            objectToMove.GetComponent<Rigidbody>().detectCollisions = true;
            requester.request_ownership();

        }
    }

    void OnCollisionExit ()
    {
        collided = false;
    }
}
