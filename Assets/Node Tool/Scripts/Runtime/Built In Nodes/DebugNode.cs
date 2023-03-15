using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class DebugNode : PureNode
    {
        public override object OnNodeStart()
        {
            Debug.Log("Hello World!");
            return null;
        }
    }
}


