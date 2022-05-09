using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : CompositeNodeBase
{
    private int _currentNodeIndex;

    public void Construct(params BaseNode[] baseNodes)
    {
        _nodes.Clear();
        _nodes.AddRange(baseNodes);
    }
    
    protected override void OnStart()
    {
        _currentNodeIndex = 0;
    }

    protected override void OnStop()
    {
        
    }

    protected override NodeStateType OnUpdate()
    {
        var child = _nodes[_currentNodeIndex];

        if (child.State != NodeStateType.Success)
        {
            return child.Update();
        }

        _currentNodeIndex++;

        return _currentNodeIndex == _nodes.Count ? NodeStateType.Success : NodeStateType.Running;
    }
}
