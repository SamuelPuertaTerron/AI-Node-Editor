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

        public void Start()
        {
            rootNode.OnGetValue();
        }

        public BaseNode CreateNode(System.Type type)
        {
            BaseNode node = ScriptableObject.CreateInstance(type) as BaseNode;
            node.name = type.Name;
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public List<BaseNode> GetChildren(BaseNode parent)
        {
            List<BaseNode> children = new List<BaseNode>();

            RootNode root = parent as RootNode;
            if(root && root.child != null) children.Add(root.child);
            
            return children;
        }

        public void AddChild(BaseNode parent, BaseNode child)
        {
            RootNode root = parent as RootNode;
            if(root) root.child = child as Node;
        }        

        public void RemoveChild(BaseNode parent)
        {
            RootNode root = parent as RootNode;
            if(root) root.child = null;
        }  

        public void DeleteNode(BaseNode node){
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
    }
}


