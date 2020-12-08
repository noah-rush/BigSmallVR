using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Normal.Realtime;

public class CodeBox : MonoBehaviour
{
    private string answer = "498";
    public string keycode;
    public GameObject entrypanel;
    private TMP_Text m_TextComponent;

    // [SerializeField]
    GameObject door;

    bool ready = false;
    [SerializeField]
    public GameObject ColorPlane;

    private RealtimeTransform _doorTransform;
    // private CodeSync _codeSync;

    AudioSource beep;
    
    void Start()
    {
        // _codeSync = GetComponent<CodeSync>();
        keycode = "";
        beep = GetComponent<AudioSource>();
        door = GameObject.FindWithTag("door");
        _doorTransform = GameObject.FindWithTag("door").GetComponent<RealtimeTransform>();
        m_TextComponent = entrypanel.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
   
        // m_TextComponent.text = keycode;
        if(keycode == answer )
        {
            _doorTransform.RequestOwnership();
            StartCoroutine("OpenDoor");

        }
    }
    public void clearCode(){
        keycode = "";
        m_TextComponent.text = keycode;

    }
    public void addToKeyCode(string number)
    {

        keycode = keycode + number;
        m_TextComponent.text = keycode;
        beep.Play();

    }
    public bool isReady()
    {
        return ready;
    }

    public void activate()
    {
        ready = true;
        // _codeSync.ready();
    }
     public void deactivate()
    {
        ready = false;
        // _codeSync.notReady();
    }
    public void setReady(bool val){
        ready = val;
    }

    IEnumerator OpenDoor()
    {
        Vector3 closed = new Vector3(0, 90, 0);
        Vector3 open = new Vector3(0, 0, 0);

        for (float ft = 0f; ft <= 1f; ft += 0.01f)
        {

            door.transform.rotation = Quaternion.Lerp(Quaternion.Euler(closed), Quaternion.Euler(open), ft);
            // door.transform.rotation = Vector3.Lerp(endPos, StartPos, ft);
            yield return new WaitForSeconds(.005f);
        }
        // Pressed = false;
        // GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ; //Remove Y movement constraint.

    }
}
