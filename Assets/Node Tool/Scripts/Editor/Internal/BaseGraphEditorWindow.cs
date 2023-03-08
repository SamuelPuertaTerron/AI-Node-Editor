using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NodeToolEditor
{
    using NodeToolEditor.UI;
    using NodeTool;

    public class BaseGraphEditorWindow : BaseWindow
    {
        private GraphEditor m_graphEditor;

        protected override void Init()
        {
            m_graphEditor = Root.Query<GraphEditor>();
            OnSelected();
        }

        private void OnSelected()
        {
            BaseGraph graph = Selection.activeObject as BaseGraph;
            if(graph) { m_graphEditor.PopulateGraph(graph); }
        }
    }
}


