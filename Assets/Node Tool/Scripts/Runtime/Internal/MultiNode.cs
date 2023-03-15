using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class MultiNode : BaseNode
    {
        public List<BaseNode> children;

        public override BaseNode OnCloneNode()
        {
            MultiNode node = Instantiate(this);
            node.children = children.ConvertAll(c => c.OnCloneNode());
            return node;
        }
    }
}


