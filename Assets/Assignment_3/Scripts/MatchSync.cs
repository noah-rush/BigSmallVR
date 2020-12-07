using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class MatchSync : RealtimeComponent<MatchSyncModel> {
    private Match _matchClass;

    private void Awake() {
        // Get a reference to the mesh renderer
        _matchClass = GetComponent<Match>();
    }

    protected override void OnRealtimeModelReplaced(MatchSyncModel previousModel, MatchSyncModel currentModel) {
        if (previousModel != null) {
            // Unregister from events
            previousModel.litDidChange -= litDidChange;
        }
        
        if (currentModel != null) {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.lit = _matchClass.isLit();
        
            // Update the mesh render to match the new model
            UpdateMatchLit();

            // Register for events so we'll know if the color changes later
            currentModel.litDidChange += litDidChange;
        }
    }

    private void litDidChange(MatchSyncModel model, bool value) {
        // Update the mesh renderer
        UpdateMatchLit();
    }

    private void UpdateMatchLit() {
        // Get the color from the model and set it on the mesh renderer.
        _matchClass.light();
    }
    public void SetMatch() {
        // Set the color on the model
        // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
        model.lit = true;
    }
}