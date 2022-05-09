using System.Collections.Generic;

public abstract class CompositeNodeBase : BaseNode
{
    protected List<BaseNode> _nodes = new List<BaseNode>();

    public IReadOnlyList<BaseNode> Nodes => _nodes;
}
