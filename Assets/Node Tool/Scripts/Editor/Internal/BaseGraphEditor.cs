using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace NodeToolEditor
{
    using NodeTool;

    public class BaseGraphEditor : Editor
    {
        private static BaseGraph m_graph;
        private SerializedProperty m_graphNodes;

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            if (Selection.activeObject as BaseGraph != null)
            {
                m_graph = Selection.activeObject as BaseGraph;

                BaseGraphEditorWindow window = BaseWindow.GetWindow<BaseGraphEditorWindow>();

                if (window == null) window = CreateInstance<BaseGraphEditorWindow>();

                window.titleContent = new GUIContent(m_graph.graphName);
                return true;
            }

            return false;
        }
    }
}


