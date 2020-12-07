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

    protected override void OnRealtimeModelReplaced(CodeSyncModel previousModel, CodeSyncModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.activatedDidChange -= activatedDidChange;

        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.activated = _codeBox.isReady();


            // Update the mesh render to match the new model
            // UpdateMatchLit();
            // UpdateActivation();


            // Register for events so we'll know if the color changes later
            currentModel.activatedDidChange += activatedDidChange;

        }
    }

    private void activatedDidChange(CodeSyncModel model, bool value)
    {
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