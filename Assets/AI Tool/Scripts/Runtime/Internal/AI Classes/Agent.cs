using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool
{
    using AINodeToolInternal;

    /*
        AI Order of executation:
        
        Call SetDestination inside a script to set a NPC to move somewhere.
        Calls The pathfinding algorithm.
        Once completed calls the OnPathFound event which has a parameter of the Path which than is set inside this script.
    */
    [AddComponentMenu("AI Node Tool/Agent")]    
    public class Agent : MonoBehaviour
    {
        public bool HasReachedPath { get; private set; }
        public bool HasFoundPath { get; private set; }

        [SerializeField] private float speed = 3.5f;
        
        private List<AINodeToolInternal.Node> m_Path; //The path to follow
        private int m_PathIndex = 0;

        /// <summary>
        /// Set the Destination for this agent to go to
        /// </summary>
        /// <param name="destination"> The desitnation as a Vector3</param>
        public void SetDestination(Vector3 destination)
        {
            if(PathFinding.Instance == null)
            {
                Debug.LogError("Pathfinding Instance is Null: Place a grid component on a GameObject");
            }

            PathFinding.Instance.RequestPath(this.transform.position, destination); //Calls the Algorithm
        }

        private void Awake()
        {
            if(PathFinding.Instance == null)
            {
                Debug.LogError("Pathfinding Instance is Null: Place a grid component on a GameObject");
            }
            PathFinding.Instance.OnPathFound += PathFound;
        }

        private void PathFound(List<Node> path)
        {
            //This gets called each timne a new path has been found
            HasFoundPath = true;
            HasReachedPath = false;
            m_Path = path;
            m_PathIndex = 0;
            HasFoundPath = false;
        }

        private void Update()
        {
            if (m_Path == null || m_Path.Count == 0) return;

            Node currentPathNode = m_Path[m_PathIndex];
            Vector3 currentPathPosition = currentPathNode.WorldPosition;

            transform.position = Vector3.MoveTowards(transform.position, currentPathPosition, speed * Time.deltaTime);

            //Increments the path index each time the AI has reached the path position
            if (transform.position == currentPathPosition)
            {
                m_PathIndex++;
                if (m_PathIndex >= m_Path.Count)
                {
                    HasReachedPath = true;
                    m_Path = null;
                }
            }
        }
    }
}


