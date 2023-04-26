using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool
{
    [ExecuteInEditMode, AddComponentMenu("AI Node Tool / Sight"), DefaultExecutionOrder(-1)]
    public class Sight : MonoBehaviour
    {
        public delegate void TargetSpotted(GameObject spottedTarget);
        public event TargetSpotted OnTargetSpotted; 

        [SerializeField] private float sightAngle = 80f;
        [SerializeField, Tooltip("This can be kept null")] private Transform lookPoint;
        [SerializeField] private GameObject target;
        [SerializeField, Tooltip("The time between the AI checks for objects \n " +
            "The smaller the value the quicker will check \n " +
            "The Larger the value the longer will check")] private float updateTime = 0.25f;
        private float m_angle;
        private Vector3 m_difference;
        private float m_currentTime;

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            m_currentTime += Time.deltaTime;

            if (m_currentTime >= updateTime)
            {
                m_currentTime = 0;
                if (IsInSightAngle(target))
                {
                    if(OnTargetSpotted != null) OnTargetSpotted(target);
                }
            }
        }

        private bool IsInSightAngle(GameObject target)
        {
            if (!target) return false;

            if (lookPoint) {

                m_difference = target.transform.position - lookPoint.position;
                m_angle = Vector3.Angle(transform.position, transform.forward);
                return m_angle < sightAngle;
            } else {
                m_difference = target.transform.position - new Vector3(transform.position.x, 0.50f, transform.position.z);
                m_angle = Vector3.Angle(transform.position, transform.forward);
                return m_angle < sightAngle;
            }
        }
    }
}


