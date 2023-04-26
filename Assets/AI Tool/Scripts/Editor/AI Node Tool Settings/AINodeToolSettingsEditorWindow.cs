using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AINodeToolEditor
{
    public class AINodeToolSettingsEditorWindow : EditorWindow
    {
        [MenuItem("Window/AI/Settings Window")]
        private static void ShowWindow()
        {
            var window = GetWindow<AINodeToolSettingsEditorWindow>();
            window.titleContent = new GUIContent("Settings Window");
            window.Show();
        }

        private void OnGUI()
        {
            AINodeToolInternal.AINodeToolSettings.UsingUnityAI = EditorGUILayout.Toggle(AINodeToolInternal.AINodeToolSettings.UsingUnityAI);
        }
    }
}



