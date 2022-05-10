using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BehaviourTree : ScriptableObject
{
    protected BaseNode _rooNode;
    protected NodeStateType _treeState = NodeStateType.Running;
    [SerializeField] protected List<BaseNode> _nodes = new List<BaseNode>();

    public NodeStateType TreeState => _treeState;
    public IReadOnlyList<BaseNode> Nodes => _nodes;

    public void Construct(BaseNode rootNode)
    {
        _rooNode = rootNode;
    }

    public NodeStateType Update()
    {
        if (_rooNode.State == NodeStateType.Running)
        {
            _treeState = _rooNode.Update();
        }

        return _treeState;
    }

    public BaseNode CreateNode(System.Type type)
    {
        BaseNode node = ScriptableObject.CreateInstance(type) as BaseNode;
        node.name = type.Name;
        node.Construct(GUID.Generate().ToString());
        _nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();

        return node;
    }

    public void DeleteNode(BaseNode node)
    {
        _nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(BaseNode parent, BaseNode child)
    {
        DecoratorNodeBase decorator = parent as DecoratorNodeBase;
        if(decorator != null)
            decorator.SetChild(child);
        
        CompositeNodeBase composite = parent as CompositeNodeBase;
        if(composite != null)
            composite.SetNodes(parent);
    }

    public void RemoveChild(BaseNode parent, BaseNode child)
    {
        DecoratorNodeBase decorator = parent as DecoratorNodeBase;
        if(decorator != null)
            decorator.SetChild(null);
        
        CompositeNodeBase composite = parent as CompositeNodeBase;
        if(composite != null)
            composite.RemoveNodes(parent);
    }

    public List<BaseNode> GetChildren(BaseNode parent)
    {
        var children = new List<BaseNode>();
        
        DecoratorNodeBase decorator = parent as DecoratorNodeBase;
        if(decorator != null)
            children.Add(decorator.Child);
        
        CompositeNodeBase composite = parent as CompositeNodeBase;
        if(composite != null)
            children.AddRange(composite.Nodes);

        return children;
    }
}