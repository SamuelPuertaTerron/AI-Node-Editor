using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AINodeToolInternal {
    using AINodeTool;

    public static class AIToolMenu {
        [MenuItem("GameObject/AI Tool/ Create NavMesh", false, 10)]
        private static void CretaeNavMesh() {
            GameObject grid = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/AI Tool/Prefabs/Grid.prefab", typeof(GameObject)) as GameObject) as GameObject;
            grid.transform.position = Vector3.zero;
        }

        [MenuItem("GameObject/AI Tool/ Create Empty AI Object", false, 10)]
        private static void CreateEmptyAIObject() {
            GameObject emptyAI = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/AI Tool/Prefabs/EmptyAIObject.prefab", typeof(GameObject)) as GameObject) as GameObject;
            emptyAI.transform.position = new Vector3(0, 1, 0);
        }

        [MenuItem("GameObject/AI Tool/ Create Waypoint Manager", false, 10)]
        private static void CretaeWayPointManager() {
            GameObject waypointManager = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/AI Tool/Prefabs/WaypointPathManager.prefab", typeof(GameObject)) as GameObject) as GameObject;
            waypointManager.transform.position = Vector3.zero;
        }

        [MenuItem("GameObject/AI Tool/ Create Audio Distraction", false, 10)]
        private static void CreateAudioDistraction() {
            GameObject audioDistraction = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/AI Tool/Prefabs/Audio Distraction.prefab", typeof(GameObject)) as GameObject) as GameObject;
            audioDistraction.transform.position = Vector3.zero;
        }
    }
}


