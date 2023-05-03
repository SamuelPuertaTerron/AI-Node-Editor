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
            m_editor = Editor.CreateEditor(nodeView.node);
            IMGUIContainer container = new IMGUIContainer(() => { m_editor.OnInspectorGUI(); });
            Add(container);
        }
    }
}


