using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class RepeatNode : SingleNode
    {
        public float duration = 1;
        public float currentStartTime = 0;

        public override object OnNodeStart()
        {
            return null; 
        }

        public override object OnNodeUpdate()
        {
            currentStartTime += Time.deltaTime;
            if(currentStartTime >= duration){
                currentStartTime = 0.0f;
                return childNode.OnNodeUpdate();
            }

            return null;
        }
    }
}


