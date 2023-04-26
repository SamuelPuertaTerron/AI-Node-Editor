using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AINodeToolEditor
{
    public class WaypointWindow : EditorWindow
    {
        struct CurrentCameraData
        {
            public Vector3 CameraPosition;
            public Quaternion CameraRotation;
            public bool IsOrtho;

            public CurrentCameraData(Vector3 cameraPosition, Quaternion cameraRotation, bool isOrtho)
            {
                CameraPosition = cameraPosition;
                CameraRotation = cameraRotation;
                IsOrtho = isOrtho;
            }
        }

        private static CurrentCameraData m_CameraData;

        [MenuItem("Window/AI/Way Point Window")]
        private static void ShowWindow()
        {
            var window = GetWindow<WaypointWindow>();
            window.titleContent = new GUIContent("WaypointWindow");
            CreateCameraView();
            window.Show();
        }

        private void ResetCameraView()
        {
            SceneView view = SceneView.sceneViews[0] as SceneView;
            view.camera.transform.position = m_CameraData.CameraPosition;
            view.camera.transform.rotation = m_CameraData.CameraRotation;
            view.camera.orthographic = m_CameraData.IsOrtho;
        }

        private static void CreateCameraView()
        {
            SceneView view = SceneView.sceneViews[0] as SceneView;

            m_CameraData = new CurrentCameraData(view.camera.transform.position, view.camera.transform.rotation, view.camera.orthographic);

            view.camera.transform.position = new Vector3(m_CameraData.CameraPosition.x, 40.0f,  m_CameraData.CameraPosition.z);
            view.camera.transform.rotation = new Quaternion(-90, 0.0f, 0.0f, 0.0f);
            view.camera.orthographic = true;
        }

        private void OnDestroy()
        {
        }
    }
}



