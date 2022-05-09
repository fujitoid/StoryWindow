using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    private BehaviourTree _behaviourTree;

    private void Start()
    {
        _behaviourTree = ScriptableObject.CreateInstance<BehaviourTree>();

        var logNode = ScriptableObject.CreateInstance<DebuglogNode>();
        logNode.Construct("Hello world!");
        
        _behaviourTree.Construct(logNode);
    }

    private void Update()
    {
        _behaviourTree.Update();
    }
}
