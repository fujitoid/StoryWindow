using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

[Serializable]
public abstract class BaseNode
{
    public event Action AllActionsFinished;
    
    [JsonProperty] internal string Guid;
    [JsonProperty] internal string Name;
    [JsonProperty] internal List<BaseNode> Children = new List<BaseNode>();
    
    [JsonProperty] protected NodeStateType _state = NodeStateType.Running;
    [JsonProperty] protected bool _isStarted = false;
    [JsonProperty] protected Vector2 _position;

    [JsonProperty] protected List<IExecutable> _executables = new List<IExecutable>();
    
    [JsonIgnore] private int _currentActionIndex = 0;

    [JsonIgnore] public NodeStateType State => _state;
    [JsonIgnore] public bool IsStarted => _isStarted;
    [JsonIgnore] public Vector2 Position => _position;

    [JsonConstructor]
    public BaseNode()
    {
        Children = new List<BaseNode>();
        _position = new Vector2();
        _executables = new List<IExecutable>();
    }

    internal virtual void SetPosition(Vector2 position)
    {
        _position = position;
    }

    public void SetActions(params IExecutable[] executables)
    {
        _executables.AddRange(executables);
    }

    public void StartExecute()
    {
        _currentActionIndex = 0;
        _executables[_currentActionIndex].Execute();
    }

    public void Continue()
    {
        if (_currentActionIndex > _executables.Count)
        {
            AllActionsFinished?.Invoke();
        }
        
        _currentActionIndex++;
        _executables[_currentActionIndex].Execute();
    }
}
