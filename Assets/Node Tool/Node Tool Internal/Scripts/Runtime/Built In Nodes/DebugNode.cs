using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class DebugNode : PureNode
    {
        [SerializeField] private string text;

        public override void OnNodeStart()
        {
            Debug.Log(text);
        }
    }
}


