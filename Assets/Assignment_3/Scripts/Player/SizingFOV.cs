using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizingFOV : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    public Vector3 minSize;
    public Vector3 maxSize;

    public float shrinkRate = 8.0f;
    public float growRate = 2.0f;

    bool isResizing = false;

    float checkTime = .1f;
    IEnumerator TrackSize()
    {
        Vector3 lastSize = player.transform.localScale;
        while (true)
        {
            if(player.transform.localScale != lastSize)
            {
                isResizing = true;
            } else
            {
                isResizing = false;
            }
            lastSize = player.transform.localScale;
            yield return new WaitForSeconds(checkTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TrackSize());
    }

    // Update is called once per frame
    void Update()
    {
        if(isResizing)
        {
            //gameObject.transform.localScale = minSize;
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, minSize, shrinkRate * Time.deltaTime);
        } else
        {
            //gameObject.transform.localScale = maxSize;
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, maxSize, growRate * Time.deltaTime);
        }
    }
}
