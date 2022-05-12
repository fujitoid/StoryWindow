using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

[Serializable]
public sealed class SplitNode : BaseNode
{
    [JsonConstructor]
    public SplitNode()
    {
        Children = new List<BaseNode>();
        _executables = new List<IExecutable>();
        _position = new Vector2();
    }
}
