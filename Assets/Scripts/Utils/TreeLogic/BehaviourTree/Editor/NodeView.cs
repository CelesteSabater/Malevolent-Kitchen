using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public Node _node;
    [SerializeField] private Port _input, _output;

    public NodeView(Node node) : base("Assets/Scripts/Utils/TreeLogic/BehaviourTree/Editor/NodeView.uxml")
    {
        _node = node;
        title = node.name;
        viewDataKey = node.GetGUID();

        Vector2 v = node.GetPosition();
        style.left = v.x;
        style.top = v.y;

        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();
    }

    private void SetupClasses()
    {
        switch (_node)
        {
            case RootNode _node:
                AddToClassList("root");
                break;
            case ActionNode _node:
                AddToClassList("action");
                break;
            case CompositeNode _node:
                AddToClassList("composite");
                break;
            case DecoratorNode _node:
                AddToClassList("decorator");
                break;
        }
    }

    private void CreateInputPorts()
    {
        switch (_node) 
        {
            case RootNode _node:
                break;
            case ActionNode _node:
                _input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case CompositeNode _node:
                _input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case DecoratorNode _node:
                _input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
        }

        if (_input != null)
        {
            _input.portName = "";
            _input.style.flexDirection = FlexDirection.Column;
            inputContainer.Add(_input);
        }
    }

    private void CreateOutputPorts()
    {
        switch (_node)
        {
            case RootNode _node:
                _output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
            case ActionNode _node:
                break;
            case CompositeNode _node:
                _output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
                break;
            case DecoratorNode _node:
                _output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
        }

        if (_output != null)
        {
            _output.portName = "";
            _output.style.flexDirection = FlexDirection.ColumnReverse;
            outputContainer.Add(_output);
        }
    }

    public Node GetNode() => _node;

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        _node.SetPosition(new Vector2(newPos.xMin, newPos.yMin));
    }

    internal Port GetOutput() => _output;
    internal Port GetInput() => _input;

    public override void OnSelected()
    {
        base.OnSelected();
        if (OnNodeSelected != null) OnNodeSelected.Invoke(this);
    }

    public void SortChildren()
    { 
        CompositeNode node = _node as CompositeNode;
        if (node) 
            node._children.Sort((left,right) => left._position.x < right._position.x ? -1 : 1);
    }

    public void UpdateState()
    {
        RemoveFromClassList("running");
        RemoveFromClassList("failure");
        RemoveFromClassList("success");

        if (Application.isPlaying) 
        {
            switch (_node.GetState())
            {
                case Node.State.Running:
                    if (_node.GetStarted())
                        AddToClassList("running");
                    break;
                case Node.State.Failure:
                    AddToClassList("failure");
                    break;
                case Node.State.Success:
                    AddToClassList("success");
                    break;
            }
        }
    }
}
