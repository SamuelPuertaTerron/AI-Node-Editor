using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace NodeToolEditor.UI
{
    using NodeToolEditor;

    public class InsepctorEditor : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InsepctorEditor, VisualElement.UxmlTraits> { }

        private Editor m_editor;

        public InsepctorEditor()
        {

        }

        public void UpdateInspector(BaseNodeView nodeView)
        {
            Clear();
            Object.DestroyImmediate(m_editor);
            m_editor = Editor.CreateEditor(nodeView.BaseNode);
            IMGUIContainer container = new IMGUIContainer(() => { m_editor.OnInspectorGUI(); }); //Creates a new inspector element on the inspector window
            Add(container);
        }

        public void ClearInspector()
        {
            Clear();
        }
    }
}


