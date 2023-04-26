using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    /// <summary>
    /// The Start Node in the graph
    /// </summary>
    public class RootNode : BaseNode
    {
        [HideInInspector] public BaseNode childNode;

        public override void OnNodeStart()
        {
            if(childNode != null)
                childNode.OnNodeStart();
            else 
                Debug.Log("The graph failed to save correctly!");
        }

        public override void OnNodeUpdate()
        {
            if(childNode != null)
                childNode.OnNodeUpdate();
            else 
                Debug.Log("The graph failed to save correctly!");
        }
    }
}


