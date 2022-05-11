using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

[Serializable]
public sealed class EventNode : BaseNode
{
    [JsonProperty][SerializeField] private EventType _event;
    
    [JsonIgnore] public EventType Event => _event;
}