using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeToolInternal
{
    public class Node
    {
        public int FCost { get { return m_GCost + m_HCost; } }
        public int GCost { get { return m_GCost; } set { value = m_GCost;} }
        public int HCost { get { return m_HCost; } set { value = m_HCost;}  }

        public Node Parent { get; set; }

        public Vector3 WorldPosition { get { return worldPosition; } }
        public bool Walkable { get { return walkable; } }
        
        public int GridX { get { return m_GridX; } }
        public int GridY { get { return m_GridY; } }

        private bool walkable;
        private Vector3 worldPosition;

        private int m_GridX;
        private int m_GridY;
        private int m_GCost;
        private int m_HCost;

        public Node(bool p_Walkable, Vector3 p_WorldPosition, int p_GridX, int p_GridY)
        {
            walkable = p_Walkable;
            worldPosition = p_WorldPosition;
            m_GridX = p_GridX;
            m_GridY = p_GridY;
        }
    }
}


