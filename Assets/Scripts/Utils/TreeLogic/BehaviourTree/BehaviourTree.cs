using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;
using static Unity.VisualScripting.Metadata;
using UnityEditor.IMGUI.Controls;

[CreateAssetMenu(menuName = "BehaviourTree/BehaviourTree")]
public class BehaviourTree : ScriptableObject
{
    [SerializeField] private Node _rootNode;
    [SerializeField] private List<Node> _nodes = new List<Node>();
    private Node.State _treeState = Node.State.Running;
    [HideInInspector] public Blackboard _blackboard = new Blackboard();

    public Node.State Update()
    {
        if (_rootNode.GetState() == Node.State.Running) _rootNode.Update();
        return _treeState;
    }

    public Node GetRootNode() => _rootNode;
    public Node SetRootNode(Node node) => _rootNode = node;
    public Node.State GetTreeState() => _treeState;
    public List<Node> GetNodes() => _nodes;

    public Node CreateNode(System.Type type)
    {
        if (type == typeof(RootNode))
        {
            if (_rootNode != null) throw new InvalidOperationException("There already exists a Root Node.");
        }

        Node node = ScriptableObject.CreateInstance(type) as Node;

        node.name = type.Name;
        node.SetGUID(GUID.Generate().ToString());

        Undo.RecordObject(node, "Behaviour Tree (CreateNode)");
        _nodes.Add(node);

        if (!Application.isPlaying)
            AssetDatabase.AddObjectToAsset(node, this);
        Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (CreateNode)");
        AssetDatabase.SaveAssets();
        return node;
    }

    public void DeleteNode(Node node)
    {
        if (node.GetType() == typeof(RootNode))
        {
            throw new InvalidOperationException("Root Node cannot be deleted.");
        }

        Undo.RecordObject(this, "Behaviour Tree (DeleteNode)");
        _nodes.Remove(node);

        Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator != null)
        {
            Undo.RecordObject(decorator, "Behaviour Tree (AddChild)");
            decorator._child = child;
            EditorUtility.SetDirty(decorator);
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode != null)
        {
            Undo.RecordObject(rootNode, "Behaviour Tree (AddChild)");
            rootNode._child = child;
            EditorUtility.SetDirty(rootNode);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite != null)
        {
            Undo.RecordObject(composite, "Behaviour Tree(AddChild)");
            composite._children.Add(child);
            EditorUtility.SetDirty(composite);
        }
    }

    public void RemoveChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator != null)
        {
            Undo.RecordObject(decorator, "Behaviour Tree (AddChild)");
            decorator._child = child;
            EditorUtility.SetDirty(decorator);
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode != null)
        {
            Undo.RecordObject(rootNode, "Behaviour Tree (RemoveChild)");
            rootNode._child = null;
            EditorUtility.SetDirty(rootNode);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite != null)
        {
            Undo.RecordObject(composite, "Behaviour Tree(RemoveChild)");
            composite._children.Remove(child);
            EditorUtility.SetDirty(composite);
        }
    }

    public List<Node> GetChildren(Node parent)
    {
        List<Node> result = new List<Node>();

        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator != null) result.Add(decorator._child);

        RootNode rootNode = parent as RootNode;
        if (rootNode != null) result.Add(rootNode._child);

        CompositeNode composite = parent as CompositeNode;
        if (composite != null) result = composite._children;

        return result;
    }

    public void RestartTree()
    {
        foreach (Node node in _nodes) node.RestartNode();
    }

    public void CreateRoot()
    {
        if (_rootNode == null)
        {
            RootNode root = CreateNode(typeof(RootNode)) as RootNode;
            _rootNode = root;
        }
    }

    private void Traverse(Node node, Action<Node> func)
    {
        if (node)
        { 
            List<Node> children = GetChildren(node);
            children.ForEach((n) => Traverse(n, func));
            func.Invoke(node);
        }
    }

    public BehaviourTree Clone() 
    {
        BehaviourTree tree = Instantiate(this);
        tree._rootNode = _rootNode.Clone();
        tree._nodes = new List<Node>();
        Traverse(tree._rootNode, (n) =>
        {
            tree._nodes.Add(n);
        });
        return tree;
    }

    public void Bind()
    {
        Traverse(_rootNode, (n) =>
        { 
            n._blackboard = _blackboard;
        });
    }
}
