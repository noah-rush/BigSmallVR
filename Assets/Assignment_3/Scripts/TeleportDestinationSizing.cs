using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDestinationSizing : MonoBehaviour
{
    [SerializeField]
    GameObject m_PlayerParent;

    private Vector3 playerSizeAtStart;
    private Vector3 teleportSizeAtStart;
    // Start is called before the first frame update
    void Start()
    {
        playerSizeAtStart = m_PlayerParent.transform.localScale;
        teleportSizeAtStart = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerSize = m_PlayerParent.transform.localScale;
        //Vector3 playerScale = new Vector3(playerSize.x / playerSizeAtStart.x, playerSize.y / playerSizeAtStart.y, playerSize.z / playerSizeAtStart.z);
        float playerScale = playerSize.x / playerSizeAtStart.x;
        gameObject.transform.localScale = teleportSizeAtStart * playerScale;
    }
}
