using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class SingleNode : BaseNode
    {
        public BaseNode childNode;

        public override BaseNode OnCloneNode()
        {
            SingleNode node = Instantiate(this);
            node.childNode = childNode.OnCloneNode();
            return node;
        }
    }
}


