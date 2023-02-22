using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class RootNode : Node
    {
        [HideInInspector] public Node child;

        public override object OnGetValue()
        {
            return child.OnGetValue();
        }
    }
}


