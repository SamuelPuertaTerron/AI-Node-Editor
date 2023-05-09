using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace NodeToolEditor
{
    using NodeTool;

    [CustomEditor(typeof(NodeTool.BaseGraph))]
    public class GraphInspector : Editor
    {
        BaseGraph baseGraph;

        void OnEnable()
        {
            baseGraph = target as BaseGraph;
        }

        public override void OnInspectorGUI()
        {
            NodeToolGUI.Header(baseGraph.graphName);

            EditorGUILayout.LabelField("Graph Name");
            EditorGUILayout.LabelField("This will change the title of graph window, NOT the name in the Assets Menu");
            baseGraph.graphName = EditorGUILayout.TextField(baseGraph.graphName);

            EditorGUI.indentLevel++;
            {
                EditorGUI.BeginDisabledGroup(true);

                EditorGUILayout.PropertyField(serializedObject.FindProperty("nodes"), true);

                EditorGUI.EndDisabledGroup();
            }
        }
    }
}


