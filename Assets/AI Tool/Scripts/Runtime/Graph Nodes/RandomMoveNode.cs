using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeTool;
using UnityEngine.AI;
namespace AINodeTool
{
    public class RandomMoveNode : PureNode
    {
        [SerializeField] private float walkRaduis = 100.0f;
        private Bounds m_bounds;

        public override void OnNodeStart()
        {
            //TODO: Find Closet bounds
            m_bounds = FindObjectOfType<Bounds>();
            if (m_bounds)
            {

                Vector3 pos = RandomVector.RandomMovement(m_bounds.BoundSize);
                //bounds size should always have 0 on Y if in 3D mode or 0 on Z in 2D mode
                ParentObject.GetComponent<Agent>().SetDestination(pos);

            }
            else
            {
                ParentObject.GetComponent<Agent>().SetDestination(RandomVector.RandomMovement(walkRaduis));
            }
        }

        public override void OnNodeUpdate()
        {
            if (m_bounds)
            {
                Vector3 pos = RandomVector.RandomMovement(m_bounds.BoundSize);
                //bounds size should always have 0 on Y if in 3D mode or 0 on Z in 2D mode
                ParentObject.GetComponent<Agent>().SetDestination(pos);
            }
            else
            {
                ParentObject.GetComponent<Agent>().SetDestination(RandomVector.RandomMovement(walkRaduis));
            }
        }
    }

    public static class RandomVector
    {
        public static Vector3 RandomMovement(float raduis)
        {
            return new Vector3(
                Random.Range(-raduis, raduis),
                Random.Range(-raduis, raduis),
                Random.Range(-raduis, raduis)
            );
        }

        public static Vector3 RandomMovement(Vector3 bounds)
        {
            return new Vector3(
                Random.Range(-bounds.x, bounds.x),
                Random.Range(-bounds.y, bounds.y),
                Random.Range(-bounds.z, bounds.z)
            );
        }
    }
}


