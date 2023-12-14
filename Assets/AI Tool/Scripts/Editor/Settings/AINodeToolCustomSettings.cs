using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace NodeToolEditorInternal {
    public class AINodeToolCustomSettings : ScriptableObject {
        public const string settingsPath = "Assets/AI Tool/Editor/Settings/AINodeToolSettings.asset";

        [SerializeField] private int m_Number;
        [SerializeField] private string m_SomeString;

        internal static AINodeToolCustomSettings GetOrCreaetSettings() {
            var settings = AssetDatabase.LoadAssetAtPath<AINodeToolCustomSettings>(settingsPath);
            if (settings == null) {
                settings = ScriptableObject.CreateInstance<AINodeToolCustomSettings>();
                AssetDatabase.CreateAsset(settings, settingsPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        internal static SerializedObject GetSerializedObject() {
            return new SerializedObject(GetOrCreaetSettings());
        }
    }

    static class AINodeToolCustomSettingsUIElementsRegister {
        [SettingsProvider]
        public static SettingsProvider CreateCustomSettings() {
            // First parameter is the path in the Settings window.
            // Second parameter is the scope of this setting: it only appears in the Project Settings window.
            var provider = new SettingsProvider("Project/AISettings", SettingsScope.Project) {
                // By default the last token of the path is used as display name if no label is provided.
                label = "AI Node Tool Settings",
                // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
                guiHandler = (searchContext) => {
                    var settings = AINodeToolCustomSettings.GetSerializedObject();
                    EditorGUILayout.PropertyField(settings.FindProperty("m_Number"), new GUIContent("My Number"));
                    EditorGUILayout.PropertyField(settings.FindProperty("m_SomeString"), new GUIContent("My String"));
                    settings.ApplyModifiedPropertiesWithoutUndo();
                },

                // Populate the search keywords to enable smart search filtering and label highlighting:
                keywords = new HashSet<string>(new[] { "Number", "Some String" })
            };

            return provider;
        }
    }
}


