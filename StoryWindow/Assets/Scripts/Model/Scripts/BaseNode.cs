using Nekonata.SituationCreator.StoryWindow.Model.Context;
using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Nekonata.SituationCreator.StoryWindow.Model
{
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

        [JsonIgnore] private int _currentActionIndex = 0;

        [JsonIgnore] protected List<IExecutable> _executables = new List<IExecutable>();

        [JsonIgnore] public string GUID => Guid;
        [JsonIgnore] public NodeStateType State => _state;
        [JsonIgnore] public bool IsStarted => _isStarted;
        [JsonIgnore] public Vector2 Position => _position;

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
}
