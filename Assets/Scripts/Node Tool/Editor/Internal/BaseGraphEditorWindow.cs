using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NodeToolEditor
{
    using NodeTool;
    using GraphEditor = UnityEditor.Experimental.GraphView.GraphView;

    public class BaseGraphEditorWindow : BaseWindow
    {
        private GraphEditor m_grapEditor;
        private NodeToolEditor.UI.GraphEditor m_baseGraphEditor;

        [MenuItem("Node Tool / Node Graph Window")]
        public static void OpenWindow()
        {
            Title = "Base Node Graph";
            GetWindow<BaseGraphEditorWindow>(Title);

            if(Selection.activeObject as BaseGraph == null)
            {
                Debug.LogError("There is no graph selected");
            }
        }

        protected override void Init()
        {
            m_grapEditor = Root.Query<GraphEditor>();
            m_baseGraphEditor = Root.Query<NodeToolEditor.UI.GraphEditor>();

            OnSelectionChange();
        }

        protected override void CloseWindow()
        {
            if(Selection.activeObject as BaseGraph != null)
            {
                BaseGraph graph = Selection.activeObject as BaseGraph;
                graph.isActive = false;
            }
        }
        

        private void OnSelectionChange() {
            BaseGraph graph = Selection.activeObject as BaseGraph;
            if(graph && AssetDatabase.CanOpenAssetInEditor(graph.GetInstanceID())) m_baseGraphEditor.PopulateGraph(graph);
        }
    }
}


