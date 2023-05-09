using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool
{
    [ExecuteInEditMode, AddComponentMenu("AI Node Tool / Sight"), DefaultExecutionOrder(-1)]
    public class Sight : MonoBehaviour
    {
        public bool IsInSight { get; private set; }

        public delegate void TargetSpotted();
        public event TargetSpotted OnTargetSpotted;

        [SerializeField] private float sightAngle = 80f;
        [SerializeField, Tooltip("This can be kept null")] private Transform lookPoint;
        [SerializeField, Tooltip("The time between the AI checks for objects \n " +
            "The smaller the value the quicker will check \n " +
            "The Larger the value the longer will check")] private float updateTime = 0.25f;
        private float m_angle;
        private Vector3 m_difference;
        private float m_currentTime;
        private GameObject m_TargetedObject;

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            m_currentTime += Time.deltaTime;

            if (m_currentTime >= updateTime)
            {
                m_currentTime = 0;
                if (IsInSightAngle())
                {
                    if (OnTargetSpotted != null)
                    {
                        IsInSight = true;
                        Debug.Log("Spotted");
                        OnTargetSpotted();
                    }
                }
                else
                {
                    IsInSight = false;
                }
            }
        }

        private bool IsInSightAngle()
        {
            RaycastHit rayhit;

            if (lookPoint)
            {
                if (Physics.Raycast(lookPoint.position, lookPoint.forward * 10.0f, out rayhit))
                {
                    if (rayhit.collider.CompareTag("Player"))
                    {
                        m_TargetedObject = rayhit.collider.gameObject;

                        m_difference = rayhit.collider.transform.position - lookPoint.position;
                        m_angle = Vector3.Angle(transform.position, transform.forward);
                        return m_angle < sightAngle;
                    }
                    // m_difference = target.transform.position - new Vector3(transform.position.x, 0.50f, transform.position.z);
                    // m_angle = Vector3.Angle(transform.position, transform.forward);
                    // return m_angle < sightAngle;
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, transform.forward * 10.0f, out rayhit))
                {
                    if (rayhit.collider.CompareTag("Player"))
                    {
                        m_TargetedObject = rayhit.collider.gameObject;

                        m_difference = rayhit.collider.transform.position - new Vector3(transform.position.x, 0.80f, transform.position.z);
                        m_angle = Vector3.Angle(transform.position, transform.forward);
                        return m_angle < sightAngle;
                    }

                    Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.red);
                }
            }

            return false;
        }

        private GameObject GetTargetedObject()
        {
            if(!m_TargetedObject)
                return m_TargetedObject;

            return null;
        }
    }
}


