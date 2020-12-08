using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerBehavior : MonoBehaviour
{
    float timer = 0f;
    [SerializeField] 
    float timeToLive;
    TextMeshPro bookText;



    // Start is called before the first frame update
    void Start()
    {
        bookText = GetComponentInChildren<TextMeshPro>();
        bookText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf) timer += Time.deltaTime;

        if (timer > timeToLive)
        {
            gameObject.SetActive(false);
            bookText.gameObject.SetActive(false);
            timer = 0f;
        }

    }

    public void SetBookTextHelp()
    {
        bookText.gameObject.SetActive(true);
    }
}
