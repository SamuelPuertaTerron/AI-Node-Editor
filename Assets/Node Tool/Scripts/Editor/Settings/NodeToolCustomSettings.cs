using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace NodeToolEditorInternal {
    public class NodeToolCustomSettings : ScriptableObject {
        public const string settingsPath = "Assets/Node Tool/Node Tool Internal/Editor/Settings/NodeToolSettings.asset";

        [SerializeField] private int m_Number;
        [SerializeField] private string m_SomeString;

        [SerializeField] private NodeColourGroup[] colourGroups;

        internal static NodeToolCustomSettings GetOrCreaetSettings() {
            var settings = AssetDatabase.LoadAssetAtPath<NodeToolCustomSettings>(settingsPath);
            if (settings == null) {
                settings = ScriptableObject.CreateInstance<NodeToolCustomSettings>();
                AssetDatabase.CreateAsset(settings, settingsPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        internal static SerializedObject GetSerializedObject() {
            return new SerializedObject(GetOrCreaetSettings());
        }

        [System.Serializable]
        internal class NodeColourGroup {
            [field: SerializeField] public Color NodeColour { get; set; } = Color.gray;
            [field: SerializeField] public Color TextColour { get; set; } = Color.white;
        }
    }

    static class NodeToolCustomSettingsUIElementsRegister {
        [SettingsProvider]
        public static SettingsProvider CreateCustomSettings() {
            // First parameter is the path in the Settings window.
            // Second parameter is the scope of this setting: it only appears in the Project Settings window.
            var provider = new SettingsProvider("Project/NodeToolSettingsProvider", SettingsScope.Project) {
                // By default the last token of the path is used as display name if no label is provided.
                label = "Node Tool Settings",
                // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
                guiHandler = (searchContext) => {
                    var settings = NodeToolCustomSettings.GetSerializedObject();
                    EditorGUILayout.PropertyField(settings.FindProperty("m_Number"), new GUIContent("My Number"));
                    EditorGUILayout.PropertyField(settings.FindProperty("m_SomeString"), new GUIContent("My String"));
                    EditorGUILayout.PropertyField(settings.FindProperty("colourGroups"), new GUIContent("Node Colour Groups"));
                    settings.ApplyModifiedPropertiesWithoutUndo();
                },

                // Populate the search keywords to enable smart search filtering and label highlighting:
                keywords = new HashSet<string>(new[] { "Number", "Some String" })
            };

            return provider;
        }
    }
}


