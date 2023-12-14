using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Collections;

namespace NodeTool {
    [CreateAssetMenu(fileName = "Node Graph", menuName = "Node Tool/Base Node Graph", order = 99)]
    public class BaseGraph : ScriptableObject {
        public RootNode rootNode;
        [ReadOnly] public List<BaseNode> nodes = new List<BaseNode>();
        public bool isActive = false;
        public string graphName = "Node Graph";

        public void StartGraph() {
            if (rootNode)
                rootNode.OnNodeStart();
        }

        public void UpdateGraph() {
            if (rootNode)
                rootNode.OnNodeUpdate();
        }

        public BaseNode CreateNode(System.Type type) {
            BaseNode node = ScriptableObject.CreateInstance(type) as BaseNode;
#if UNITY_EDITOR
            node.GenerateGUID();
#endif
            node.name = type.Name;
            node.OnCreateNode();
            nodes.Add(node);
#if UNITY_EDITOR
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
#endif
            return node;
        }

        public List<BaseNode> GetChildren(BaseNode parent) {
            List<BaseNode> children = new List<BaseNode>();

            RootNode root = parent as RootNode;
            if (root && root.childNode != null) {
                children.Add(root.childNode);
            }

            SingleNode singleNode = parent as SingleNode;
            if (singleNode && singleNode.childNode != null) {
                children.Add(singleNode.childNode);
            }

            MultiNode multiNode = parent as MultiNode;
            if (multiNode && multiNode.children != null) {
                return multiNode.children;
            }

            return children;
        }

        public void AddChild(BaseNode parent, BaseNode child) {
            RootNode root = parent as RootNode;
            if (root) {
                root.childNode = child;
            }

            SingleNode singleNode = parent as SingleNode;
            if (singleNode) {
                singleNode.childNode = child;
            }

            MultiNode multiNode = parent as MultiNode;
            if (multiNode) {
                if (child || parent)
                    multiNode.children.Add(child);
                else
                    Debug.LogError("Child is null");
            }
        }

        public void RemoveChild(BaseNode parent, BaseNode child) {
            RootNode root = parent as RootNode;
            if (root) {
                root.childNode = null;
            }

            SingleNode singleNode = parent as SingleNode;
            if (singleNode) {
                singleNode.childNode = null;
            }

            MultiNode multiNode = parent as MultiNode;
            if (multiNode) {
                multiNode.children.Remove(child);
            }
        }

        public void DeleteNode(BaseNode node) {
            nodes.Remove(node);
#if UNITY_EDITOR
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
#endif
        }

        public BaseGraph Clone() {
            BaseGraph graph = Instantiate(this);

            foreach (BaseNode node in nodes) {
                node.OnCloneNode();
            }

            return graph;
        }

        private void OnDisable() {
            if (Application.isPlaying) {
                foreach (BaseNode node in nodes) {
                    node.OnNodeExit();
                }
            }
        }
    }
}


