using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class CodeSyncModel {
    [RealtimeProperty(1, true, true)]
    private bool _activated;

    [RealtimeProperty(2, true, true)]
    private string _keycode;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class CodeSyncModel : RealtimeModel {
    public bool activated {
        get {
            return _cache.LookForValueInCache(_activated, entry => entry.activatedSet, entry => entry.activated);
        }
        set {
            if (this.activated == value) return;
            _cache.UpdateLocalCache(entry => { entry.activatedSet = true; entry.activated = value; return entry; });
            InvalidateReliableLength();
            FireActivatedDidChange(value);
        }
    }
    
    public string keycode {
        get {
            return _cache.LookForValueInCache(_keycode, entry => entry.keycodeSet, entry => entry.keycode);
        }
        set {
            if (this.keycode == value) return;
            _cache.UpdateLocalCache(entry => { entry.keycodeSet = true; entry.keycode = value; return entry; });
            InvalidateReliableLength();
            FireKeycodeDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(CodeSyncModel model, T value);
    public event PropertyChangedHandler<bool> activatedDidChange;
    public event PropertyChangedHandler<string> keycodeDidChange;
    
    private struct LocalCacheEntry {
        public bool activatedSet;
        public bool activated;
        public bool keycodeSet;
        public string keycode;
    }
    
    private LocalChangeCache<LocalCacheEntry> _cache = new LocalChangeCache<LocalCacheEntry>();
    
    public enum PropertyID : uint {
        Activated = 1,
        Keycode = 2,
    }
    
    public CodeSyncModel() : this(null) {
    }
    
    public CodeSyncModel(RealtimeModel parent) : base(null, parent) {
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        UnsubscribeClearCacheCallback();
    }
    
    private void FireActivatedDidChange(bool value) {
        try {
            activatedDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireKeycodeDidChange(string value) {
        try {
            keycodeDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        int length = 0;
        if (context.fullModel) {
            FlattenCache();
            length += WriteStream.WriteVarint32Length((uint)PropertyID.Activated, _activated ? 1u : 0u);
            length += WriteStream.WriteStringLength((uint)PropertyID.Keycode, _keycode);
        } else if (context.reliableChannel) {
            LocalCacheEntry entry = _cache.localCache;
            if (entry.activatedSet) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.Activated, entry.activated ? 1u : 0u);
            }
            if (entry.keycodeSet) {
                length += WriteStream.WriteStringLength((uint)PropertyID.Keycode, entry.keycode);
            }
        }
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var didWriteProperties = false;
        
        if (context.fullModel) {
            stream.WriteVarint32((uint)PropertyID.Activated, _activated ? 1u : 0u);
            stream.WriteString((uint)PropertyID.Keycode, _keycode);
        } else if (context.reliableChannel) {
            LocalCacheEntry entry = _cache.localCache;
            if (entry.activatedSet || entry.keycodeSet) {
                _cache.PushLocalCacheToInflight(context.updateID);
                ClearCacheOnStreamCallback(context);
            }
            if (entry.activatedSet) {
                stream.WriteVarint32((uint)PropertyID.Activated, entry.activated ? 1u : 0u);
                didWriteProperties = true;
            }
            if (entry.keycodeSet) {
                stream.WriteString((uint)PropertyID.Keycode, entry.keycode);
                didWriteProperties = true;
            }
            
            if (didWriteProperties) InvalidateReliableLength();
        }
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            switch (propertyID) {
                case (uint)PropertyID.Activated: {
                    bool previousValue = _activated;
                    _activated = (stream.ReadVarint32() != 0);
                    bool activatedExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.activatedSet);
                    if (!activatedExistsInChangeCache && _activated != previousValue) {
                        FireActivatedDidChange(_activated);
                    }
                    break;
                }
                case (uint)PropertyID.Keycode: {
                    string previousValue = _keycode;
                    _keycode = stream.ReadString();
                    bool keycodeExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.keycodeSet);
                    if (!keycodeExistsInChangeCache && _keycode != previousValue) {
                        FireKeycodeDidChange(_keycode);
                    }
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
        }
    }
    
    #region Cache Operations
    
    private StreamEventDispatcher _streamEventDispatcher;
    
    private void FlattenCache() {
        _activated = activated;
        _keycode = keycode;
        _cache.Clear();
    }
    
    private void ClearCache(uint updateID) {
        _cache.RemoveUpdateFromInflight(updateID);
    }
    
    private void ClearCacheOnStreamCallback(StreamContext context) {
        if (_streamEventDispatcher != context.dispatcher) {
            UnsubscribeClearCacheCallback(); // unsub from previous dispatcher
        }
        _streamEventDispatcher = context.dispatcher;
        _streamEventDispatcher.AddStreamCallback(context.updateID, ClearCache);
    }
    
    private void UnsubscribeClearCacheCallback() {
        if (_streamEventDispatcher != null) {
            _streamEventDispatcher.RemoveStreamCallback(ClearCache);
            _streamEventDispatcher = null;
        }
    }
    
    #endregion
}
/* ----- End Normal Autogenerated Code ----- */
