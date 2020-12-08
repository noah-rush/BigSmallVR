using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Wick : MonoBehaviour
{
    // Start is called before the first frame update
    bool lit = false;
    public bool tunnelCandle;
    [SerializeField]
    GameObject book;
    // private RealtimeView  _codeLockRealtime;
    // private RealtimeTransform _codeLockTransform;

    Realtime _realtime;

    void Start()
    {
        // _codeLockRealtime = codeLock.GetComponent<RealtimeView>();
        // _codeLockTransform = codeLock.GetComponent<RealtimeTransform>();
        
        // codeLock = GameObject.Find("CodeLock");
        // codeLockPosition = new Vector3(-6.75f, 10.95f, -14.33f);
            if(tunnelCandle){
        
        book.SetActive(false);
    }
    }

    // Update is called once per frame
    void Update()
    {

    

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Fire" && !lit)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            lit = true;
            this.transform.GetChild(1).gameObject.SetActive(true);

            if(tunnelCandle)
            {
                // _codeLockTransform.RequestOwnership();
                // codeLock.GetComponent<CodeBox>().activate();
                // StartCoroutine("showCodeLock");
                book.SetActive(true);
            
                
            }
        }

    }
    // IEnumerator showCodeLock(){
    //     yield return new WaitForSeconds(10.0f);
    //     this.transform.GetChild(0).gameObject.SetActive(false);
    //     lit = false;
    //     codeLock.GetComponent<CodeBox>().deactivate();

    //     // _codeLockTransform.RequestOwnership();
    //      // codeLock.transform.GetChild(0).gameObject.SetActive(false);
        
    // }
}
