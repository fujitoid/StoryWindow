using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BehaviourTree : ScriptableObject
{
    protected BaseNode _rooNode;
    protected NodeStateType _treeState = NodeStateType.Running;
    protected List<BaseNode> _nodes = new List<BaseNode>();

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
}
