using UnityEngine.UIElements;
#if UNITY_EDITOR
namespace Project.RecipeTree.Editor
{
    public class RecipeInspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<RecipeInspectorView, VisualElement.UxmlTraits> { }

        UnityEditor.Editor editor;

        public RecipeInspectorView() { }

        public void UpdateSelection(RecipeNodeView nodeView) 
        {
            Clear();

            UnityEngine.Object.DestroyImmediate(editor);

            editor = UnityEditor.Editor.CreateEditor(nodeView.GetNode());
            IMGUIContainer container = new IMGUIContainer(() => { 
                if (editor.target) editor.OnInspectorGUI(); 
            });
            Add(container);
        }
    }
}
#endif