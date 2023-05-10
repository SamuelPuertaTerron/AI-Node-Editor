using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool
{
    [AddComponentMenu("AI Node Tool / Sight"), DefaultExecutionOrder(-1)]
    public class Sight : MonoBehaviour
    {
        public bool IsInSight { get; private set; }

        public delegate void TargetSpotted();
        public event TargetSpotted OnTargetSpotted;

        [SerializeField] private float sightAngle = 80f;
        [SerializeField, Tooltip("This can be kept null")] private Transform lookPoint;
        [SerializeField, Tooltip("The time between the AI checks for objects \n " +
            "The smaller the value the quicker will check \n " +
            "The Larger the value the longer will check")]
        private float updateTime = 0.25f;
        [SerializeField] private LayerMask interactionMask = 9;
        private float m_angle;
        private Vector3 m_difference;
        private float m_currentTime;
        private GameObject m_TargetedObject;
        private int m_TargetCount;
        private Collider[] colliders = new Collider[50];

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            m_currentTime += Time.deltaTime;

            if (m_currentTime >= updateTime)
            {
                m_currentTime = 0;

                m_TargetCount = Physics.OverlapSphereNonAlloc(transform.position, 10.0f, colliders, interactionMask, QueryTriggerInteraction.Collide);

                for (int i = 0; i < m_TargetCount; i++)
                {
                    //Check if something is int sight
                    if (colliders[i] != null)
                    {
                        if (IsInSightAngle(colliders[i].gameObject))
                        {
                            Debug.Log("Object in sight");
                        }
                    }
                }
            }
        }

        private bool IsInSightAngle(GameObject obj)
        {
            Vector3 origin = transform.position;
            Vector3 dest = obj.transform.position;
            Vector3 direction = origin - dest;
            m_difference = direction;
            if (direction.y < 0) return false;

            direction.y = 0;
            float angle = Vector3.Angle(direction, transform.forward);
            if(angle > m_angle)
            {
                return false;
            }

            return true;
        }

        private GameObject GetTargetedObject()
        {
            if (!m_TargetedObject)
                return m_TargetedObject;

            return null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 10.0f);

            Gizmos.DrawWireCube(transform.position, m_difference);
        }
    }
}


