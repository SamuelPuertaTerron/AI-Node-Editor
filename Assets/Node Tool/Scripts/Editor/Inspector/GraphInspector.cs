using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace NodeToolEditor {
    using NodeTool;

    [CustomEditor(typeof(NodeTool.BaseGraph))]
    public class GraphInspector : Editor {
        BaseGraph baseGraph;

        void OnEnable() {
            baseGraph = target as BaseGraph;
        }

        public override void OnInspectorGUI() {
            NodeToolGUI.Header(baseGraph.graphName);

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


