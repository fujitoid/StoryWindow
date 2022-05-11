using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

[Serializable]
public class BehaviourTree
{
    [JsonProperty] protected NodeStateType _treeState = NodeStateType.Running;
    [JsonProperty] protected List<BaseNode> _nodes = new List<BaseNode>();

    [JsonIgnore] public NodeStateType TreeState => _treeState;
    [JsonIgnore] public IReadOnlyList<BaseNode> Nodes => _nodes;

    [JsonConstructor]
    public BehaviourTree()
    {
        _treeState = NodeStateType.Running;
        _nodes = new List<BaseNode>();
    }

    public BaseNode CreateNode(System.Type type)
    {
        BaseNode node = Activator.CreateInstance(type) as BaseNode;
        node.Name = type.Name;
        node.Guid = GUID.Generate().ToString();
        _nodes.Add(node);

        return node;
    }

    public void DeleteNode(BaseNode node)
    {
        _nodes.Remove(node);
    }

    public void AddChild(BaseNode parent, BaseNode child)
    {
        parent.Children.Add(child);
    }

    public void RemoveChild(BaseNode parent, BaseNode child)
    {
        parent.Children.Remove(child);
    }

    public List<BaseNode> GetChildren(BaseNode parent)
    {
        return parent.Children;
    }
}