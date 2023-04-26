using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class SingleNode : BaseNode
    {
        [HideInInspector] public BaseNode childNode;

        public override BaseNode OnCloneNode()
        {
            SingleNode node = Instantiate(this);
            if(node.childNode == null)
            {
                Debug.LogError("Single Node must require a child node");
                return null;
            }
            node.childNode = childNode.OnCloneNode();
            return node;
        }
    }
}


