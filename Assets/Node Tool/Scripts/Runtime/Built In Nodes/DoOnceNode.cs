using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class DoOnceNode : SingleNode
    {
        public bool bHasStarted = false;

        public override object OnNodeUpdate()
        {
            if(!bHasStarted)
            {
                childNode.OnNodeUpdate();
                bHasStarted = true;
            }

            return null;
        }

        public override void OnNodeExit()
        {
            bHasStarted = false;
        }
    }
}


