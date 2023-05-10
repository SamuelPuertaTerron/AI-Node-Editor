using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AINodeTool
{
    [AddComponentMenu("AI Node Tool/ Way Point Pathfinding")]
    public class WaypointPathfinding : MonoBehaviour
    {
        [SerializeField] private AINodeToolInternal.WaypointPath path;
        public Agent Agent { get; private set; }

        private NavMeshAgent agent;

        private int m_PathIndex = 0;
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (agent && path)
            {
                agent.SetDestination(path.Waypoints[m_PathIndex].transform.position);

                if (Vector3.Distance(path.Waypoints[m_PathIndex].transform.position, agent.transform.position) < 1.5f)
                {
                    if (path.Waypoints[m_PathIndex].actions != null) path.Waypoints[m_PathIndex].actions.Invoke();

                    m_PathIndex++;

                    if (m_PathIndex >= path.Waypoints.Count)
                    {
                        m_PathIndex = 0;
                    }
                }
            }
            else
            {
                if(!agent) Debug.LogError("Agent is null. Please add a NavMeshAgent component to this gameobject");
                if(!path) Debug.LogError("The path is null. Please add it in the inspector.");

                Debug.Break();
            }
        }
    }
}