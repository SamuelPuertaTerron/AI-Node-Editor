using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeTool;

namespace AINodeTool
{
    public class ChaseNode : PureNode
    {
        public override void OnNodeUpdate()
        {
            if(ParentObject.GetComponent<Sight>())
            {
                if(ParentObject.GetComponent<Sight>().IsInSight)
                {
                    Debug.Log("has Started chasing the player!");
                }
            }
        }
    }
}

