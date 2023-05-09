using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace AINodeToolEditor
{
    using AINodeTool;
    using AINodeToolInternal;
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
        private WaypointPath m_CurrentPath;

        private bool m_HasCreatedPath = false;

        [MenuItem("Window/AI/Way Point Window")]
        private static void ShowWindow()
        {
            var window = GetWindow<WaypointWindow>();
            window.titleContent = new GUIContent("WaypointWindow");
            window.Show();

            CreateCameraView();
            OnGUICreate();
        }

        private void ResetCameraView()
        {
            m_View.LookAt(m_CameraData.CameraPosition, m_CameraData.CameraRotation);
            m_View.Repaint();
        }

        private static void CreateCameraView()
        {
            m_View = SceneView.sceneViews[0] as SceneView;

            m_CameraData = new CurrentCameraData(m_View.camera.transform.position, m_View.camera.transform.rotation, m_View.camera.orthographic);

            Vector3 position = SceneView.lastActiveSceneView.pivot;
            m_View.LookAt(position, Quaternion.Euler(90, 0, 0));
            m_View.Repaint();
        }

        private static void OnGUICreate()
        {

        }

        private void OnGUI()
        {
            WayPointManager wayPointManager = FindObjectOfType<WayPointManager>(); //Really want to move this out of here

            Vector3 worldPoint = m_View.camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (GUILayout.Button("Close"))
            {
                this.Close();
            }

            if (GUILayout.Button("Create New Path"))
            {
                m_HasCreatedPath = true;
                WaypointPath waypointPath = Instantiate(wayPointManager.WaypointPathObject, wayPointManager.transform);
                waypointPath.gameObject.name = "Waypoint Path";
                waypointPath.Waypoints = new System.Collections.Generic.List<Waypoint>();
                m_CurrentPath = waypointPath;
                if (waypointPath != null)
                    Debug.Log(waypointPath.gameObject.name);
            }

            if (GUILayout.Button("Delete Path"))
            {
                //Only remove a path if it has a path script
                if (Selection.activeGameObject.GetComponent<WaypointPath>() != null)
                {
                    wayPointManager.WaypointPath.Remove(Selection.activeGameObject.GetComponent<WaypointPath>());
                    DestroyImmediate(Selection.activeGameObject);
                }
            }

            if (!m_HasCreatedPath)
            {
                EditorGUILayout.HelpBox("Created to new path to add waypoint elements", MessageType.Info);
            }

            EditorGUI.indentLevel++;

            EditorGUI.BeginDisabledGroup(!m_HasCreatedPath);
            {
                if (m_CurrentPath != null)
                {
                    if (GUILayout.Button("Create new waypoint"))
                    {
                        worldPoint.y = 0.0f; //Keeps the object on the ground
                        GameObject obj = Instantiate(wayPointManager.WaypointObject, worldPoint, Quaternion.identity, m_CurrentPath.transform);
                        m_CurrentPath.Waypoints.Add(obj.GetComponent<Waypoint>());
                    }

                    if (GUILayout.Button("Complete Path"))
                    {
                        //Draw our last waypoint to the first waypoint
                        m_CurrentPath.hasCompletedPath = true;
                        WaypointPath waypointPath = m_CurrentPath;
                        wayPointManager.WaypointPath.Add(waypointPath);
                        m_HasCreatedPath = false;
                    }

                    if (GUILayout.Button("Remove Waypoint"))
                    {
                        //Only remove a waypoint from the waypoint path if it has a waypoint script
                        if (Selection.activeGameObject.GetComponent<Waypoint>() != null)
                        {
                            m_CurrentPath.Waypoints.Remove(Selection.activeGameObject.GetComponent<Waypoint>());
                            DestroyImmediate(Selection.activeGameObject);
                        }
                    }
                }
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space(10);

            EditorGUI.BeginDisabledGroup(true);
            {
                //List of current path in the waypoint managers
                EditorGUILayout.TextField("Current Waypoint Paths");
                EditorGUI.BeginDisabledGroup(true);

                for (int i = 0; i < wayPointManager.WaypointPath.Count; i++)
                {
                    EditorGUILayout.TextField("Path Index: " + i.ToString());
                    EditorGUILayout.ObjectField(wayPointManager.WaypointPath[i], typeof(WaypointPath), true);
                }

                EditorGUI.EndDisabledGroup();
            }
            EditorGUI.EndDisabledGroup();
        }

        private void OnDestroy()
        {
            SaveChanges();
            ResetCameraView();
        }
    }
}