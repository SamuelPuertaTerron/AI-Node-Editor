using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeToolInternal {
    internal struct Path {
        public Vector3 Start { get; private set; }
        public Vector3 Dest { get; private set; }

        public Path(Vector3 start, Vector3 dest) {
            Start = start;
            Dest = dest;
        }
    }

    [DefaultExecutionOrder(-2)]
    public class PathRequest : NodeTool.Singleton<PathRequest> {
        private PathFinding m_PathFinding;

        private Queue<Path> m_PathRequestQueue = new Queue<Path>();

        private bool m_HasRequestedPath = false;

        private void OnEnable() {
            m_PathFinding = GetComponent<PathFinding>();
        }

        /// <summary>
        /// Finds the next path using A* with the start and dest
        /// </summary>
        /// <returns>The path as a list</returns>
        public List<Node> RequestPath(Vector3 start, Vector3 dest) {
            Path newPathRequest = new Path(start, dest);
            m_PathRequestQueue.Enqueue(newPathRequest);
            List<Node> path = TryNextProcess();
            m_HasRequestedPath = false;
            return path;
        }
        
        private List<Node> TryNextProcess() {
            List<Node> currentPath = new List<Node>();

            if (!m_HasRequestedPath && m_PathRequestQueue.Count > 0) {
                m_HasRequestedPath = true;
                Path currentPathRequest = m_PathRequestQueue.Dequeue();
                if (m_PathFinding != null)
                    currentPath = m_PathFinding.RequestPath(currentPathRequest.Start, currentPathRequest.Dest);
                else
                    Debug.LogError("Cannot find pathfinding script. Assign it to an empty game object");
            }

            return currentPath;
        }
    }
}


