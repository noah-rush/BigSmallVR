using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class CodeSync : RealtimeComponent<CodeSyncModel>
{
    private CodeBox _codeBox;

    private void Awake()
    {
        // Get a reference to the mesh renderer
        _codeBox = GetComponent<CodeBox>();
    }

    void Update(){
        UpdateActivation();
    }
  
    private void UpdateActivation()
    {
        // Get the color from the model and set it on the mesh renderer.
      
            _codeBox.setReady(model.activated);
      
       
    }
 
    public void ready()
    {
        model.activated = true;
    }
    public void notReady()
    {
        model.activated = false;
    }
 
}