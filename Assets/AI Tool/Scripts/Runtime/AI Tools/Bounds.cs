using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AINodeTool
{
    [ExecuteInEditMode, AddComponentMenu("AI Node Tool / Bounds"), DefaultExecutionOrder(-1)]
    public class Bounds : MonoBehaviour
    {
        public float Raduis { get { return m_raduis; } }
        public Vector3 BoundSize { get { return boundsSize; } }

        [SerializeField] private Vector3 boundsSize;
        [SerializeField] private LayerMask aiMask = 8;
        [SerializeField] private bool debug = true;

        private Collider[] m_aiInBounds;
        private float m_raduis = 5.0f;

        private void Start()
        {
            m_raduis = boundsSize.magnitude;     
        }

        private void Update()
        {
            m_aiInBounds = Physics.OverlapBox(transform.position, boundsSize, Quaternion.identity, aiMask);

            foreach (Collider col in m_aiInBounds)
            {
                Vector3 position = col.gameObject.transform.localPosition;

                if (position.x > boundsSize.x)
                {
                    Debug.Log("Called position.x > boundsSize.x");
                    position = new Vector3(boundsSize.x, position.y, position.z);
                }
                else if (position.x < -boundsSize.x)
                {
                    Debug.Log("Called position.x < -boundsSize.x");
                    position = new Vector3(-boundsSize.x, position.y, position.z);
                }
                else if (position.z > boundsSize.z)
                {
                    Debug.Log("Called position.z > boundsSize.z");
                    position = new Vector3(position.z, position.y, boundsSize.z);
                }
                else if (position.z < -boundsSize.z)
                {
                    Debug.Log(-boundsSize.z);
                    position = new Vector3(position.x, position.y, -boundsSize.z);
                }

                col.gameObject.transform.position = position;
            }
        }

        private void OnDrawGizmos()
        {
            if (debug)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position, boundsSize * 2);
            }
        }
    }
}


