using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class RepeatNode : SingleNode
    {
        [SerializeField] private float duration = 1;
        private float currentStartTime = 0;

        public override void OnNodeStart()
        {
            childNode.OnNodeStart();
        }

        public override void OnNodeUpdate()
        {
            currentStartTime += Time.deltaTime;
            if(currentStartTime >= duration){
                currentStartTime = 0.0f;
                childNode.OnNodeUpdate();
            }
        }
    }
}


