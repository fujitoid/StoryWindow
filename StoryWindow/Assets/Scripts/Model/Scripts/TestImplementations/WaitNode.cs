using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : ActionNodeBase
{
    [SerializeField] private float _duration = 1;

    private float _startTime;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override NodeStateType OnUpdate()
    {
        if (Time.time - _startTime > _duration)
            return NodeStateType.Success;

        return NodeStateType.Running;
    }
}
