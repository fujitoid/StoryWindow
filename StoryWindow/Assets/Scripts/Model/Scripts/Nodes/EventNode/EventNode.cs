using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

[Serializable]
public sealed class EventNode : BaseNode
{
    [JsonProperty] private int _event;
    
    [JsonIgnore] public EventType Event => (EventType)_event;

    [JsonConstructor]
    public EventNode()
    {
        Children = new List<BaseNode>();
        _executables = new List<IExecutable>();
        _position = new Vector2();
    }
}
