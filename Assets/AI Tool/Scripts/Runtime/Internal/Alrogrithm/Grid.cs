using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeToolInternal
{
    [RequireComponent(typeof(PathFinding))]
    [AddComponentMenu("AI Node Tool/ Nav Mesh Grid")]
    [DefaultExecutionOrder(-1)]
    public class Grid : MonoBehaviour
    {
        [field: SerializeField] public bool CanSeeNavmesh { get; set; }
        public Node[,] WorldGrid { get { return m_Grid; } }

        [SerializeField] private Vector2 worldSize = new Vector2(100.0f, 100.0f);
        [SerializeField] private float m_NodeRaduis = 1.0f;
        [SerializeField] private LayerMask walkableLayerMask;
        private Node[,] m_Grid;
        private float m_NodeDiameter;
        private int m_GridSizeX, m_GridSizeY;

        private void Awake()
        {
            Grid grid = FindObjectOfType<Grid>();
            if(grid != this)
            {
                Debug.LogError("Found more than one instance of Gird in the scene. Can only have one instance of grid within this Scene.");
            }

            m_NodeDiameter = m_NodeRaduis * 2;
            m_GridSizeX = Mathf.RoundToInt(worldSize.x / m_NodeDiameter);
            m_GridSizeY = Mathf.RoundToInt(worldSize.y / m_NodeDiameter);
            CreateGrid();
        }

        private void CreateGrid()
        {
            m_Grid = new Node[m_GridSizeX, m_GridSizeY];

            Vector3 worldBottomLeft = new Vector3(transform.position.x, transform.position.y, transform.position.z) - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2;

            for (int x = 0; x < m_GridSizeX; x++)
            {
                for (int y = 0; y < m_GridSizeY; y++)
                {   
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * m_NodeDiameter + m_NodeRaduis) + Vector3.forward * (y * m_NodeDiameter + m_NodeRaduis);
                    bool walkable = !(Physics.CheckSphere(worldPoint, m_NodeRaduis, walkableLayerMask));
                    m_Grid[x,y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        public Node GetNodeFromWorldPoint(Vector3 worldPoint)
        {
            float percentX = (worldPoint.x + worldSize.x / 2) / worldSize.x;
            float percentY = (worldPoint.z + worldSize.y / 2) / worldSize.y;

            Mathf.Clamp01(percentX);
            Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((m_GridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((m_GridSizeY - 1) * percentY);

            return m_Grid[x, y];
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> nodes = new List<Node>();

            for(int x = -1; x <= 1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    if(x == 0 && y == 0)
                        continue;
                    
                    int checkX = node.GridX + x;
                    int checkY = node.GridY + y;

                    if(checkX >= 0 && checkX < m_GridSizeX && 
                    checkY >= 0 && checkY < m_GridSizeY)
                    {
                        nodes.Add(m_Grid[checkX, checkY]);
                    }
                }
            }

            return nodes;
        }

        public List<Node> path;

        //TODO: Display nav mesh area using own mesh
        private void OnDrawGizmos()
        {
            if(m_Grid != null && CanSeeNavmesh)
            {
                Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1, worldSize.y));
                foreach(Node node in m_Grid)
                {
                    Gizmos.color = (node.Walkable) ? Color.green : Color.red;
                    if(path != null) if(path.Contains(node)) Gizmos.color = Color.black;
                    Gizmos.DrawCube(node.WorldPosition, Vector3.one * (m_NodeDiameter - 0.1f));
                    
                }
            }
        }
    }
}


