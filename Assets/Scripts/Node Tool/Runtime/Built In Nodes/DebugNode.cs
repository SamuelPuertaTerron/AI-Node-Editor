using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class DebugNode : Node
    {
        [HideInInspector] public Node child;

        [SerializeField] private object message;

        public override object OnGetValue()
        {
            Debug.Log("This is a debug node");
            return null;
        }
    }
}


