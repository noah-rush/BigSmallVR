using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerBehavior : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] 
    float timeToLive;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf) timer += Time.deltaTime;

        if (timer > timeToLive)
        {
            gameObject.SetActive(false);
            GetComponentInChildren<TextMeshPro>().gameObject.SetActive(false);
            timer = 0f;
        }

    }

    public void ShowBookText()
    {
        GetComponentInChildren<TextMeshPro>().gameObject.SetActive(true);
    }
}
