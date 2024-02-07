using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class DoOnceNode : SingleNode
    {
        private bool bHasStarted = false;

        public override void OnNodeUpdate()
        {
            if (!bHasStarted)
            {
                childNode.OnNodeUpdate();
                bHasStarted = true;
            }
        }

        public override void OnNodeExit()
        {
            bHasStarted = false;
        }
    }
}


