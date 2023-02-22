using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace NodeToolEditor
{
    using NodeTool;

    [CustomEditor(typeof(BaseGraph))]
    public class BaseGraphEditor : Editor
    {
        private static BaseGraph m_graph;
        private SerializedProperty m_graphNodes;

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            if (Selection.activeObject as BaseGraph != null)
            {
                BaseGraphEditorWindow window = BaseWindow.GetWindow<BaseGraphEditorWindow>();
                if (window == null) window = CreateInstance<BaseGraphEditorWindow>();

                m_graph.isActive = true;
                return true;
            }

            return false;
        }

        private void OnEnable()
        {
            m_graph = (BaseGraph)target;
            m_graphNodes = serializedObject.FindProperty("nodes");
            m_graph.name = m_graph.graphName;
        }

        public override void OnInspectorGUI()
        {
            if (m_graph == null)
            {
                Debug.LogError("Graph is null");
            }

            serializedObject.Update();

            string isActiveText = string.Format("Is Active: {0}", m_graph.isActive);

            string numbOfNodes = string.Format("Number of Nodes: {0}", m_graph.nodes.Count);

            string titleText = m_graph.graphName;

            EditorUI.Header(m_graph.graphName);

            GUILayout.Label(isActiveText);
            GUILayout.Label(numbOfNodes);

            
            GUILayout.Label("Graph Name");
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(m_graph), m_graph.graphName);
            m_graph.graphName = GUILayout.TextField(titleText);
            m_graph.name = m_graph.graphName;
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(m_graph), m_graph.graphName);
            AssetDatabase.Refresh();
            

            EditorGUI.indentLevel++;
            {
                EditorGUI.BeginDisabledGroup(true);
                {
                    EditorGUILayout.PropertyField(m_graphNodes, new GUIContent("Nodes"), true);
                }
                EditorGUI.EndDisabledGroup();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}


