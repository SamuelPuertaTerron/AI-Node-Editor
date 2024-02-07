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
        private InsepctorEditor m_inspectorEditor;

        protected override void Init()
        {
            m_graphEditor = Root.Query<GraphEditor>();
            m_inspectorEditor = Root.Query<InsepctorEditor>();
            m_graphEditor.OnNodeSelected = OnNodeSelected;
            m_graphEditor.OnNodeUnselected = OnNodeUnselected;
            OnSelected();
        }

        private void OnSelected()
        {
            BaseGraph graph = Selection.activeObject as BaseGraph;
            if (graph) { m_graphEditor.PopulateGraph(graph); }
        }

        private void OnNodeSelected(BaseNodeView nodeView)
        {
            m_inspectorEditor.UpdateInspector(nodeView);
        }

        private void OnNodeUnselected(BaseNodeView nodeView)
        {
            m_inspectorEditor.Clear();
        }
    }
}


