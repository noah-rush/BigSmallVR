using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class CodeSync : RealtimeComponent<CodeSyncModel> {
    private CodeBox _codeBox;

    private void Awake() {
        // Get a reference to the mesh renderer
        _codeBox = GetComponent<CodeBox>();
    }

    protected override void OnRealtimeModelReplaced(CodeSyncModel previousModel, CodeSyncModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.activatedDidChange -= activatedDidChange;
            previousModel.keycodeDidChange -= keycodeDidChange;

        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.activated = _codeBox.isActive();
                currentModel.keycode = _codeBox.getKeycode();

        
            // Update the mesh render to match the new model
            // UpdateMatchLit();

            // Register for events so we'll know if the color changes later
            currentModel.keycodeDidChange += keycodeDidChange;
            currentModel.activatedDidChange += activatedDidChange;

        }
    }

    private void activatedDidChange(CodeSyncModel model, bool value) {
        // Update the mesh renderer
        	_codeBox.activateFromModel(value);

    }
    private void keycodeDidChange(CodeSyncModel model, string value) {
        // Update the mesh renderer
        // UpdateMatchLit();
        _codeBox.setKeycode(value);
    }

    // private void UpdateMatchLit() {
    //     // Get the color from the model and set it on the mesh renderer.
    //     _matchClass.light();
    // }
    public void SetActivation(bool value) {
        // Set the color on the model
        // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
        model.activated = value;
    }
    public void SetKeycode(string keycode) {
        // Set the color on the model
        // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
        model.keycode = keycode;
    }
}