using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NodeToolEditor {
    public class BaseWindow : EditorWindow {
        public VisualTreeAsset VisualTreeAsset { get { return m_VisualTreeAsset; } }

        public VisualElement Root { get { return m_root; } }

        public static string Title { get; set; }

        protected virtual void Init() {

        }

        protected virtual void DrawWindowElements() {

        }

        protected virtual void CloseWindow() {

        }

        //----------------------- Private -------------------------//

        [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;
        private VisualElement m_root;

        private void CreateGUI() {
            m_root = rootVisualElement;
            m_VisualTreeAsset.CloneTree(m_root);

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Node Tool/Editor/Graph Editor/GraphEditorWindow.uss");
            m_root.styleSheets.Add(styleSheet);

            Init();
        }

        private void OnGUI() {
            DrawWindowElements();
        }

        private void OnDestroy() {
            CloseWindow();
        }
    }
}


