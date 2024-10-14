using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    [SerializeField] private BehaviourTree _tree;

    public BehaviourTree GetTree() => _tree;

    private void Start()
    {
        if (_tree != null)
        {
            _tree.RestartTree();
            _tree = _tree.Clone();
            _tree.Bind();
        }
    }

    void Update()
    {
        if (_tree != null) _tree.Update();
    }
}