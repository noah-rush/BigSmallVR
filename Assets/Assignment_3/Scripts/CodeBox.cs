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
    // Start is called before the first frame update
    private TMP_Text m_TextComponent;
    [SerializeField]
    GameObject door;
    float timer = 0f;
    bool activated = false;
    [SerializeField]
    public GameObject ColorPlane;

    // private RealtimeView _doorRealtime;
    private RealtimeTransform _doorTransform;
    public void activate()
    {

        activated = true;
        var cubeRenderer = ColorPlane.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_TintColor", Color.green);
        // ColorPlane.GetComponent<BoxCo÷÷llider>().enabled = false;
    }
    public void deactivate()
    {
        activated = false;
        var cubeRenderer = ColorPlane.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_TintColor", Color.red);
        // ColorPlane.GetComponent<BoxCollider>().enabled = true;

    }
    void Start()
    {
        keycode = "";
        _doorTransform = door.GetComponent<RealtimeTransform>();

        m_TextComponent = entrypanel.GetComponent<TMP_Text>();
        var cubeRenderer = ColorPlane.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_EmissionColor", Color.red);
        // StartCoroutine("Resetter");
    }

    // Update is called once per frame
    void Update()
    {

        // m_TextComponent.text = keycode;
        if(keycode == answer && activated)
        {
            _doorTransform.RequestOwnership();
            StartCoroutine("OpenDoor");

        }
    }

    public void addToKeyCode(string number)
    {
        // return keycode;
        if(keycode == "")
        {
            StartCoroutine("Resetter");

        }
        keycode = keycode + number;
        m_TextComponent.text = keycode;


    }

    IEnumerator Resetter()
    {

        yield return new WaitForSeconds(10f);
        keycode = "";
        m_TextComponent.text = keycode;
        // print("WaitAndPrint " + Time.time);
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
