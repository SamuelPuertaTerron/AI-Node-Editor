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

                //bounds size should always have 0 on Y if in 3D mode or 0 on Z in 2D mode
                ParentObject.GetComponent<AINodeTool.Agent>().SetDestination(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)));
            }
            else
            {
                ParentObject.GetComponent<AINodeTool.Agent>().SetDestination(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)));
            }
        }

        public override void OnNodeUpdate()
        {
            if (m_bounds)
            {
                //bounds size should always have 0 on Y if in 3D mode or 0 on Z in 2D mode
                ParentObject.GetComponent<AINodeTool.Agent>().SetDestination(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)));
            }
            else
            {
                ParentObject.GetComponent<AINodeTool.Agent>().SetDestination(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)));
            }
        }
    }
}


