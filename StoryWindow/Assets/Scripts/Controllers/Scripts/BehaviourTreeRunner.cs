using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    [SerializeField] private BehaviourTree _behaviourTree;

    private void Update()
    {
        _behaviourTree.Update();
    }
}
