using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandScanner : MonoBehaviour
{
    [SerializeField]
    GameObject wallParent, parent;

    [SerializeField]
    TextMeshPro playerSizeText;

    bool moveWalls;

    [SerializeField]
    float maxPlayerScale = 1f;

    PlayerSizingContinuous player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerSizingContinuous>();
        moveWalls = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveWalls) 
        {
            wallParent.transform.position = Vector3.Lerp(wallParent.transform.position, new Vector3(0,5,0), Time.deltaTime * .3f);
        }

        playerSizeText.text = player.scaleFactor.ToString("F2") + moveWalls.ToString();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (player.scaleFactor <= maxPlayerScale) {
            MeshRenderer[] meshRenderers = parent.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer mr in meshRenderers)
                mr.enabled = false;
            moveWalls = true;
        }
    }
}
