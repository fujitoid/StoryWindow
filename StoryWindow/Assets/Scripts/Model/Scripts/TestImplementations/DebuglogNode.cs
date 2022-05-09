using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DebuglogNode : ActionNodeBase
{
    [SerializeField] private string _message;

    public void Construct(string message)
    {
        _message = message;
    }
    
    protected override void OnStart()
    {
        Debug.Log($"OnStart: {_message}");
    }

    protected override void OnStop()
    {
        Debug.Log($"OnStop: {_message}");
    }

    protected override NodeStateType OnUpdate()
    {
        Debug.Log($"OnUpdate: {_message}");
        return NodeStateType.Success;
    }
}
