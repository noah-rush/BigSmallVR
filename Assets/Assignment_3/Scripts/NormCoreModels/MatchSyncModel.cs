using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;
[RealtimeModel]
public partial class MatchSyncModel {
	[RealtimeProperty(1, true, true)]
    private bool _lit;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class MatchSyncModel : RealtimeModel {
    public bool lit {
        get {
            return _cache.LookForValueInCache(_lit, entry => entry.litSet, entry => entry.lit);
        }
        set {
            if (this.lit == value) return;
            _cache.UpdateLocalCache(entry => { entry.litSet = true; entry.lit = value; return entry; });
            InvalidateReliableLength();
            FireLitDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(MatchSyncModel model, T value);
    public event PropertyChangedHandler<bool> litDidChange;
    
    private struct LocalCacheEntry {
        public bool litSet;
        public bool lit;
    }
    
    private LocalChangeCache<LocalCacheEntry> _cache = new LocalChangeCache<LocalCacheEntry>();
    
    public enum PropertyID : uint {
        Lit = 1,
    }
    
    public MatchSyncModel() : this(null) {
    }
    
    public MatchSyncModel(RealtimeModel parent) : base(null, parent) {
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        UnsubscribeClearCacheCallback();
    }
    
    private void FireLitDidChange(bool value) {
        try {
            litDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        int length = 0;
        if (context.fullModel) {
            FlattenCache();
            length += WriteStream.WriteVarint32Length((uint)PropertyID.Lit, _lit ? 1u : 0u);
        } else if (context.reliableChannel) {
            LocalCacheEntry entry = _cache.localCache;
            if (entry.litSet) {
                length += WriteStream.WriteVarint32Length((uint)PropertyID.Lit, entry.lit ? 1u : 0u);
            }
        }
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var didWriteProperties = false;
        
        if (context.fullModel) {
            stream.WriteVarint32((uint)PropertyID.Lit, _lit ? 1u : 0u);
        } else if (context.reliableChannel) {
            LocalCacheEntry entry = _cache.localCache;
            if (entry.litSet) {
                _cache.PushLocalCacheToInflight(context.updateID);
                ClearCacheOnStreamCallback(context);
            }
            if (entry.litSet) {
                stream.WriteVarint32((uint)PropertyID.Lit, entry.lit ? 1u : 0u);
                didWriteProperties = true;
            }
            
            if (didWriteProperties) InvalidateReliableLength();
        }
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            switch (propertyID) {
                case (uint)PropertyID.Lit: {
                    bool previousValue = _lit;
                    _lit = (stream.ReadVarint32() != 0);
                    bool litExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.litSet);
                    if (!litExistsInChangeCache && _lit != previousValue) {
                        FireLitDidChange(_lit);
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
        _lit = lit;
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
