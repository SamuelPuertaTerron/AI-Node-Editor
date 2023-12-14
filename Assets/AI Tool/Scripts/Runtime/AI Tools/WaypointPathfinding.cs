using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AINodeTool {
    [AddComponentMenu("AI Node Tool/ Way Point Pathfinding")]
    public class WaypointPathfinding : MonoBehaviour {
        //Pauses the agents AI
        public bool IsPaused { get; set; } 
        public PathfindingAgent Agent { get; private set; }


        [SerializeField] private AINodeToolInternal.WaypointPath path;
        [SerializeField] private bool reversePath;


        private NavMeshAgent agent;

        private int m_PathIndex = 0;
        private void Start() {
            agent = GetComponent<NavMeshAgent>();
            if (!path) {
                Debug.LogError($"The path vairable on game object {gameObject.name} is null. Please assign it in the inspector.");
                Debug.Break();
            }

            if (reversePath) {
                path.Waypoints.Reverse();
            }
        }
        private void Update() {

            //Loops through the waypoints and updates the agents position to the next waypoint index. 
            if (path && !IsPaused) {
                agent.SetDestination(path.Waypoints[m_PathIndex].transform.position);

                if (Vector3.Distance(path.Waypoints[m_PathIndex].transform.position, agent.transform.position) < 1.5f) {
                    if (path.Waypoints[m_PathIndex].actions != null) path.Waypoints[m_PathIndex].actions.Invoke(); //If the waypoint has actions than invoke them

                    m_PathIndex++;

                    if (m_PathIndex >= path.Waypoints.Count) {
                        m_PathIndex = 0;
                    }
                }
            }
        }
    }
}


