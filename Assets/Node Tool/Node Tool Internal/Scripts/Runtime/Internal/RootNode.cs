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
            childNode.OnNodeStart();
        }

        public override void OnNodeUpdate()
        {
            childNode.OnNodeUpdate();
        }
    }
}


