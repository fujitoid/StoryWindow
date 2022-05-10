using System.Collections.Generic;

public abstract class CompositeNodeBase : BaseNode
{
    protected List<BaseNode> _nodes = new List<BaseNode>();

    public IReadOnlyList<BaseNode> Nodes => _nodes;

    internal void SetNodes(params BaseNode[] nodes) => _nodes.AddRange(nodes);

    internal void RemoveNodes(params BaseNode[] nodes)
    {
        foreach (var node in nodes)
        {
            _nodes.Remove(node);
        }
    }
}
