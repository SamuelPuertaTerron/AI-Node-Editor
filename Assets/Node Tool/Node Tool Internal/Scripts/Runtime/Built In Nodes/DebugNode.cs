using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class DebugNode : PureNode
    {
        public override void OnNodeStart()
        {
            Debug.Log("Hello World!");
        }
    }
}


