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

    [SerializeField]
    GameObject door;

    bool activated = false;
    [SerializeField]
    public GameObject ColorPlane;

    private RealtimeTransform _doorTransform;
    private CodeSync _codeSync;


    public bool isActive()
    {
        return activated;
    }

    public void activate()
    {
        activated = true;
        _codeSync.SetActivation(true);
    }
     public void deactivate()
    {
        activated = false;
        _codeSync.SetActivation(false);
    }
    public void setActivation(bool val){
        activated = val;
    }
    void Start()
    {
        _codeSync = GetComponent<CodeSync>();
        keycode = "";
        _doorTransform = door.GetComponent<RealtimeTransform>();
        m_TextComponent = entrypanel.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var cubeRenderer = ColorPlane.GetComponent<Renderer>();

        if(activated)
        {
            cubeRenderer.material.SetColor("_TintColor", Color.green);
        }
        else
        {
            cubeRenderer.material.SetColor("_TintColor", Color.red);
        }
        // m_TextComponent.text = keycode;
        if(keycode == answer && activated)
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
