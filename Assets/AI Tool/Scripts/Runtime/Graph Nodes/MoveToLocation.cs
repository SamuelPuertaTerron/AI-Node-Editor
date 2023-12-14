using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeTool;
using UnityEngine.AI;

namespace AINodeTool {
    public class MoveToLocation : PureNode {
        [SerializeField] private Vector3 position;

        // This is called on the first frame when in play mode
        public override void OnNodeStart() {
            ParentObject.GetComponent<AINodeTool.PathfindingAgent>().SetDestination(position); 
        }
    }
}

