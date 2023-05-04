using UnityEngine;
using UnityEditor;

namespace AINodeToolEditor
{
    using AINodeTool;
    using UnityEngine.InputSystem;

    public class WaypointWindow : EditorWindow
    {
        internal struct CurrentCameraData
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

        private static SceneView m_View;

        [MenuItem("Window/AI/Way Point Window")]
        private static void ShowWindow()
        {
            var window = GetWindow<WaypointWindow>();
            window.titleContent = new GUIContent("WaypointWindow");
            window.Show();
        
            CreateCameraView();
        }

        private void ResetCameraView()
        {
            m_View.LookAt(m_CameraData.CameraPosition, m_CameraData.CameraRotation);
            m_View.camera.orthographic = m_CameraData.IsOrtho;
            m_View.Repaint();
        }

        private static void CreateCameraView()
        {
            m_View = SceneView.sceneViews[0] as SceneView;

            m_CameraData = new CurrentCameraData(m_View.camera.transform.position, m_View.camera.transform.rotation, m_View.camera.orthographic);

            Vector3 position = SceneView.lastActiveSceneView.pivot;
            m_View.LookAt(position, Quaternion.Euler(90, 0, 0));
            m_View.orthographic = true;
            m_View.Repaint();

            Debug.Log(m_CameraData.IsOrtho);
        }

        private void OnGUI() {

            WayPointManager wayPointManager = FindObjectOfType<WayPointManager>();

            Vector3 worldPoint = m_View.camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            
            if (GUILayout.Button("Close")) {
                this.Close();
            }

            if(GUILayout.Button("Create new waypoint")) {
                worldPoint.y = 0.0f; //Keeps the object on the ground
                GameObject obj = Instantiate(wayPointManager.WaypointObject, worldPoint, Quaternion.identity, wayPointManager.transform);
                wayPointManager.WaypointPath.Add(obj);
                string saveData = JsonUtility.ToJson(wayPointManager.WaypointPath.Count, false);
                System.IO.File.WriteAllText(AINodeToolInternal.FileManager.GetPath() + "SaveData.Json", saveData);
            }

            if(GUILayout.Button("Remove Waypoint")) {
                //Only remove a waypoint from the waypoint path if it has a waypoint script
                if( Selection.activeGameObject.GetComponent<AINodeToolInternal.Waypoint>() != null) {
                    wayPointManager.WaypointPath.Remove(Selection.activeGameObject);
                    DestroyImmediate(Selection.activeGameObject);
                }
            }
        }

        private void OnDestroy() {
            ResetCameraView();
        }
    }
}



