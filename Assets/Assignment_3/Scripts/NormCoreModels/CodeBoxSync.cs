using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public class CodeBoxSyncModel {
    [RealtimeProperty(1, true, true)]
    private bool activated;

    [RealtimeProperty(2, true, true)]
    private string keycode;
}