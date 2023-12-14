using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AINodeToolInternal {
    /* 
        Optimazation Improvements:
            Multithreading
     */
    [DefaultExecutionOrder(-2)]
    public class PathFinding : MonoBehaviour {
        private Grid m_Grid;

        private void Awake() {
            m_Grid = FindObjectOfType<Grid>();
            if (!m_Grid) {
                m_Grid = gameObject.AddComponent<Grid>();
                Debug.LogFormat($"Cannot find instance of Grid within the Scene. Has cretaed a temporary Grid component  on this {gameObject.name} object. This will destroyed when out of play mode."); //Cannot use LogWarning for some reason
            }
        }

        public List<Node> RequestPath(Vector3 start, Vector3 dest) {
            return FindPath(start, dest);
        }

        #region Pseudo Code
        /* Found here https://en.wikipedia.org/wiki/A*_search_algorithm#Pseudocode
        // A* finds a path from start to goal.
        // h is the heuristic function. h(n) estimates the cost to reach goal from node n.
        function A_Star(start, goal, h)
        // The set of discovered nodes that may need to be (re-)expanded.
        // Initially, only the start node is known.
        // This is usually implemented as a min-heap or priority queue rather than a hash-set.
        openSet := {start}

        // For node n, cameFrom[n] is the node immediately preceding it on the cheapest path from the start
        // to n currently known.
        cameFrom := an empty map

        // For node n, gScore[n] is the cost of the cheapest path from start to n currently known.
        gScore := map with default value of Infinity
        gScore[start] := 0

        // For node n, fScore[n] := gScore[n] + h(n). fScore[n] represents our current best guess as to
        // how cheap a path could be from start to finish if it goes through n.
        fScore := map with default value of Infinity
        fScore[start] := h(start)

        while openSet is not empty
            // This operation can occur in O(Log(N)) time if openSet is a min-heap or a priority queue
            current := the node in openSet having the lowest fScore[] value
            if current = goal
                return reconstruct_path(cameFrom, current)

            openSet.Remove(current)
            for each neighbor of current
                // d(current,neighbor) is the weight of the edge from current to neighbor
                // tentative_gScore is the distance from start to the neighbor through current
                tentative_gScore := gScore[current] + d(current, neighbor)
                if tentative_gScore < gScore[neighbor]
                    // This path to neighbor is better than any previous one. Record it!
                    cameFrom[neighbor] := current
                    gScore[neighbor] := tentative_gScore
                    fScore[neighbor] := tentative_gScore + h(neighbor)
                    if neighbor not in openSet
                        openSet.add(neighbor)

        // Open set is empty but goal was never reached
        return failure
        */
        #endregion

        /// <summary>
        /// Core Algorithm Loop
        /// </summary>
        private List<Node> FindPath(Vector3 startPos, Vector3 endPos) {
            Node startNode = m_Grid.GetNodeFromWorldPoint(startPos);
            Node endNode = m_Grid.GetNodeFromWorldPoint(endPos);

            if (startNode.Walkable && endNode.Walkable) {
                List<Node> openSet = new List<Node>(); //Improve performance by using a custom data structure like a minHeap currenly takes a while to find a path
                List<Node> closedSet = new List<Node>();

                openSet.Add(startNode);

                while (openSet.Count > 0) {
                    Node currentNode = openSet[0]; //Set the current node to the start node

                    for (int i = 1; i < openSet.Count; i++) {
                        //Check if the next in the open set has a lower fcost to the current node and set the current node to n
                        if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost) {
                            currentNode = openSet[i];
                        }
                    }

                    if (currentNode == endNode) {
                        return RetracePath(startNode, endNode); ;
                    }

                    openSet.Remove(currentNode);
                    closedSet.Add(currentNode);

                    foreach (Node node in m_Grid.GetNeighbours(currentNode).Where(n => n.Walkable && !closedSet.Contains(n))) {
                        //Find the new g cost and check if it is lower than the current gcost
                        int newGCost = currentNode.GCost + Distance(currentNode, node);
                        if (newGCost < node.GCost || !openSet.Contains(node)) {
                            //Set the current node to the new node with the lower gcost
                            node.GCost = newGCost;
                            node.HCost = Distance(currentNode, node);
                            node.Parent = currentNode;

                            if (!openSet.Contains(node)) {
                                openSet.Add(node);
                            }
                        }
                    }
                }
            }

            return null;
        }

        private List<Node> RetracePath(Node startNode, Node endNode) {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            //Loops through the parent nodes until it is equal to the start node
            while (currentNode != startNode) {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            m_Grid.path = path;

            path.Reverse(); // Reverse the order of the path to get it from start to end
            return path;
        }

        private int Distance(Node current, Node nieghbour) {
            //Estimates the distance from the current node to the neighbour
            int distance = Mathf.Abs((int)(current.GridX - nieghbour.GridX)) + Mathf.Abs((int)(current.GridY - nieghbour.GridY));
            return distance;
        }
    }
}


