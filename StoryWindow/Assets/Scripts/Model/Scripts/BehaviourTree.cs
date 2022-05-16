using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Nekonata.SituationCreator.StoryWindow.Model
{
    [Serializable]

    public class BehaviourTree
    {
        [JsonProperty] protected NodeStateType _treeState = NodeStateType.Running;
        [JsonProperty] protected List<BaseNode> _nodes = new List<BaseNode>();

        [JsonIgnore] private BaseNode _currentSelectedNode;

        [JsonIgnore] public NodeStateType TreeState => _treeState;
        [JsonIgnore] public IReadOnlyList<BaseNode> Nodes => _nodes;
        [JsonIgnore] public BaseNode CurrentSelectedNode => _currentSelectedNode;

        [JsonConstructor]
        public BehaviourTree()
        {
            _treeState = NodeStateType.Running;
            _nodes = new List<BaseNode>();
        }

        internal BaseNode CreateNode(Type type)
        {
            BaseNode node = Activator.CreateInstance(type) as BaseNode;
            node.Name = type.Name;
            node.Guid = GUID.Generate().ToString();
            _nodes.Add(node);

            return node;
        }

        internal void SetCurrentSelectedNode(BaseNode node)
        {
            _currentSelectedNode = node;
        }

        internal void DeleteNode(BaseNode node)
        {
            _nodes.Remove(node);
        }

        internal void AddChild(BaseNode parent, BaseNode child)
        {
            parent.Children.Add(child);
        }

        internal void RemoveChild(BaseNode parent, BaseNode child)
        {
            parent.Children.Remove(child);
        }

        internal List<BaseNode> GetChildren(BaseNode parent)
        {
            return parent.Children;
        }
    }
}