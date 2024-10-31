using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;

namespace Celeste.Tools.BehaviourTree
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

        Editor editor;

        public InspectorView() { }

        public void UpdateSelection(NodeView nodeView) 
        {
            Clear();

            UnityEngine.Object.DestroyImmediate(editor);

            editor = Editor.CreateEditor(nodeView.GetNode());
            IMGUIContainer container = new IMGUIContainer(() => { 
                if (editor.target) editor.OnInspectorGUI(); 
            });
            Add(container);
        }
    }
}