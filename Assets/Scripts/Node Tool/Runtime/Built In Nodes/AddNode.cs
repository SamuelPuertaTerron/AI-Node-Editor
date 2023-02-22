using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class AddNode : Node
    {
        public override object OnGetValue()
        {
            float val1 = 4;
            float val2 = 6;
            Debug.Log("Adding 4 + 6");
            return val1 + val2; 
        }
    }
}

