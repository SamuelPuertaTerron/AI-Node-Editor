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
            
            return children;
        }

        public void AddChild(BaseNode parent, BaseNode child)
        {
            
        }        

        public void RemoveChild(BaseNode parent)
        {
           
        }  

        public void DeleteNode(BaseNode node){
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
    }
}


