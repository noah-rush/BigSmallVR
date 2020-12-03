using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wick : MonoBehaviour
{
    // Start is called before the first frame update
    bool lit = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
    	if(other.gameObject.name == "Fire"){
    		this.transform.GetChild(0).gameObject.SetActive(true);
    	}
    }
}
