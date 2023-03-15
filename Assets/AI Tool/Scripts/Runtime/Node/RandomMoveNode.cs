using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeTool;
using UnityEngine.AI;

namespace AINodeTool
{
    public class RandomMoveNode : PureNode
    {
        [SerializeField] private float walkRaduis = 10.0f;

        public override object OnNodeUpdate()
        {
            ParentObject.GetComponent<NavMeshAgent>().SetDestination(AIMovement.RandomMovement(walkRaduis));
            return null;
        }
    }

    public static class AIMovement
    {
        public static Vector3 RandomMovement(float raduis)
        {
            return new Vector3(
                Random.Range(-raduis, raduis),
                Random.Range(-raduis, raduis),
                Random.Range(-raduis, raduis)
            );
        }
    }
}


