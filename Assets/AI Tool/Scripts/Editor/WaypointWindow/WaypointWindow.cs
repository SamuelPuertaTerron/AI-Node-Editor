using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AINodeToolEditor
{
    public class WaypointWindow : EditorWindow
    {
        private bool m_IsOrtho = false;

        [MenuItem("Window/AI/Way Point Window")]
        private static void ShowWindow()
        {
            var window = GetWindow<WaypointWindow>();
            window.titleContent = new GUIContent("WaypointWindow");
            window.Show();
        }

        private void OnGUI()
        {
            SceneView view = SceneView.sceneViews[0] as SceneView;
            m_IsOrtho = GUI.Toggle(new Rect(425, 0, 50, 50), m_IsOrtho, "Is Orthographic");

            if (GUI.Button(new Rect(0, 0, 200, 50), "Top Down Camera"))
            {
                Vector3 position = SceneView.lastActiveSceneView.pivot;
                view.LookAt(position, Quaternion.Euler(90, 0, 0));
                view.orthographic = m_IsOrtho;
                SceneView.lastActiveSceneView.Repaint();
            }
            if (GUI.Button(new Rect(225, 0, 200, 50), "Reset Camera"))
            {
                view.orthographic = false;
                m_IsOrtho = false;
            }
        }
    }
}



