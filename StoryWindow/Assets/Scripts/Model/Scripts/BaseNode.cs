using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNode : ScriptableObject
{
    protected NodeStateType _state = NodeStateType.Running;
    protected bool _isStarted = false;

    public NodeStateType State => _state;
    public bool IsStarted => _isStarted;

    public NodeStateType Update()
    {
        
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract NodeStateType OnUpdate();
}
