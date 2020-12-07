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
            previousModel.keycodeDidChange -= keycodeDidChange;

        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.activated = _codeBox.isActive();
            currentModel.keycode = _codeBox.getKeycode();


            // Update the mesh render to match the new model
            // UpdateMatchLit();
            UpdateActivation();
            UpdateKeycode();


            // Register for events so we'll know if the color changes later
            currentModel.keycodeDidChange += keycodeDidChange;
            currentModel.activatedDidChange += activatedDidChange;

        }
    }

    private void activatedDidChange(CodeSyncModel model, bool value)
    {
        UpdateActivation();

    }
    private void keycodeDidChange(CodeSyncModel model, string value)
    {
        // Update the mesh renderer
        UpdateKeycode();
    }
    private void UpdateActivation()
    {
        // Get the color from the model and set it on the mesh renderer.
        if(model.activated)
        {
            _codeBox.activate();

        }
        else
        {
            _codeBox.deactivate();

        }
        // _codeBox.activateFromModel(model.activated);
    }
    private void UpdateKeycode()
    {
        // Get the color from the model and set it on the mesh renderer.
        _codeBox.setKeycode(model.keycode);
    }

    public void SetActivation(bool value)
    {
        model.activated = value;
    }
    public void SetKeycode(string keycode)
    {
        // Set the color on the model
        // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
        model.keycode = keycode;
    }
}