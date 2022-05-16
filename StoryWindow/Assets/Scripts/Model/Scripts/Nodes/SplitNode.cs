using Nekonata.SituationCreator.StoryWindow.Model;
using Nekonata.SituationCreator.StoryWindow.Model.Context;
using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Nekonata.SituationCreator.Model.Implementations
{
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
}
