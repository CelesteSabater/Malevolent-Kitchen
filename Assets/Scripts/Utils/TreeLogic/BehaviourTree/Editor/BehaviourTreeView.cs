using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Diagnostics;

public class BehaviourTreeView : GraphView
{
    public Action<NodeView> OnNodeSelected;
    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }
    BehaviourTree _tree;
    public BehaviourTreeView() 
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Utils/TreeLogic/BehaviourTree/Editor/BehaviourTreeEditor.uss");
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    public void UpdateNodeStates()
    {
        if (_tree != null) 
            _tree.GetNodes().ForEach(n => 
            {
                NodeView node = FindNodeView(n); 
                node.UpdateState();
            });
    }

    private void OnUndoRedo()
    {
        PopulateView(_tree);
        AssetDatabase.SaveAssets();
    }

    private NodeView FindNodeView(Node node)
    {
        return GetNodeByGuid(node.GetGUID()) as NodeView;
    }

    internal void PopulateView(BehaviourTree tree)
    {
        _tree = tree;

        if (_tree != null)
        {
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            if (_tree.GetRootNode() == null)
            {
                _tree.CreateRoot();
                EditorUtility.SetDirty(_tree);
                AssetDatabase.SaveAssets();
            }

            foreach (Node node in _tree.GetNodes()) 
                CreateNodeView(node);

            foreach (Node parent in _tree.GetNodes())
            {
                List<Node> children = _tree.GetChildren(parent);
                foreach (Node child in children)
                {
                    if (child != null)
                    {
                        NodeView parentView = FindNodeView(parent);
                        NodeView childView = FindNodeView(child);

                        Edge edge = parentView.GetOutput().ConnectTo(childView.GetInput());
                        AddElement(edge);
                    }
                }
            }
        }
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null) _tree.DeleteNode(nodeView.GetNode());

                Edge edge = elem as Edge;
                if (edge != null)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childrenView = edge.input.node as NodeView;
                    _tree.RemoveChild(parentView.GetNode(), childrenView.GetNode());
                }
            });
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childrenView = edge.input.node as NodeView;
                _tree.AddChild(parentView.GetNode(), childrenView.GetNode());
            });
        }

        if (graphViewChange.movedElements != null)
        {
            nodes.ForEach((node) =>
            {
                NodeView nodeView = node as NodeView;
                nodeView.SortChildren();
            });
        }
        
        return graphViewChange;
    }

    private void CreateNodeView(Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => 
            endPort.direction != startPort.direction && 
            endPort.node != startPort.node
        ).ToList();
    }

    private void CreateNode(Type type)
    {
        Node node = _tree.CreateNode(type);
        CreateNodeView(node);
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
        foreach (var type in types) evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));

        types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
        foreach (var type in types) evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));

        types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
        foreach (var type in types) evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
    }
}
