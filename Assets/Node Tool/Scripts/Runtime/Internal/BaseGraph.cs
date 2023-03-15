using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Collections;

namespace NodeTool
{
    [CreateAssetMenu(fileName = "Node Graph", menuName = "Node Tool/Base Node Graph")]
    public class BaseGraph : ScriptableObject
    {
        public BaseNode rootNode;
        [ReadOnly] public List<BaseNode> nodes = new List<BaseNode>();
        public bool isActive = false;
        public string graphName = "Node Graph";
        
        private int m_nodeValue = 1;

        public void StartGraph()
        {
            rootNode.OnNodeStart();
        }

        public void UpdateGraph()
        {
            rootNode.OnNodeUpdate();
        }

        public BaseNode CreateNode(System.Type type)
        {
            BaseNode node = ScriptableObject.CreateInstance(type) as BaseNode;
            node.GenerateGUID();
            node.name = type.Name;
            node.OnCreateNode();
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public List<BaseNode> GetChildren(BaseNode parent)
        {
            List<BaseNode> children = new List<BaseNode>();

            RootNode root = parent as RootNode;
            if(root && root.childNode != null){
                children.Add(root.childNode);
            }

            SingleNode singleNode = parent as SingleNode;
            if(singleNode && singleNode.childNode != null){
                children.Add(singleNode.childNode);
            }

            MultiNode multiNode = parent as MultiNode;
            if(multiNode && multiNode.children != null){
                return multiNode.children;
            }

            return children;
        }

        public void AddChild(BaseNode parent, BaseNode child)
        {
            RootNode root = parent as RootNode;
            if(root){
                root.childNode = child;
            }

            SingleNode singleNode = parent as SingleNode;
            if(singleNode){
                singleNode.childNode = child;
            }

            MultiNode multiNode = parent as MultiNode;
            if(multiNode){
                multiNode.children.Add(child);
            }
        }        

        public void RemoveChild(BaseNode parent)
        {
           RootNode root = parent as RootNode;
            if(root){
                root.childNode = null;
            }

            SingleNode singleNode = parent as SingleNode;
            if(singleNode){
                singleNode.childNode = null;
            }
        }  

        public void DeleteNode(BaseNode node){
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public BaseGraph Clone()
        {
            BaseGraph graph = Instantiate(this);

            foreach(BaseNode node in nodes)
            {
                node.OnCloneNode();
            }

            return graph;
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        private void OnDisable()
        {
            if(Application.isPlaying)
            {
                foreach(BaseNode node in nodes)
                {
                    node.OnNodeExit();
                }
            }
        }
    }
}


