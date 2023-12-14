using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeToolEditor {
    using UnityEditor;

    public class MassAISpawnerWindow : EditorWindow {
        private int m_SpawnAmount = 5;


        [MenuItem("Window/AI/AI Spawner")]
        private static void ShowWindow() {
            var window = GetWindow<MassAISpawnerWindow>();
            window.titleContent = new GUIContent("AI Spawner");
            window.Show();
        }

        private void OnGUI() {
            if (m_SpawnAmount > 100) {
                EditorGUILayout.HelpBox("Spawn Amount cannot pass 100", MessageType.Error);
                m_SpawnAmount = 100;
            }

            EditorGUI.BeginDisabledGroup(m_SpawnAmount > 100);
            {
                m_SpawnAmount = EditorGUILayout.IntField(m_SpawnAmount);
            }

            EditorGUI.BeginDisabledGroup(m_SpawnAmount < 1);
            {
                if (GUILayout.Button("Spawn AI")) {
                    GameObject obj = new GameObject("Static AI Spawner");

                    //Spawn in the amount of AI 

                    for (int i = 0; i < m_SpawnAmount; i++) {
                        GameObject emptyAI = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/AI Tool/Prefabs/StaticAI.prefab", typeof(GameObject)) as GameObject, obj.transform) as GameObject;
                        emptyAI.transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5)); //Set a random position
                    }
                }
            }
        }
    }
}


