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
        public BaseNode childNode;

        public override object OnNodeUpdate()
        {
            return childNode.OnNodeUpdate();
        }
    }
}


