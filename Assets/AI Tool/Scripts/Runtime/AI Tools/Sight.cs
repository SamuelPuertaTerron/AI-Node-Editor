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

        [SerializeField] private float raduis = 1.0f;
        [SerializeField] private float sightAngle = 80f;
        [SerializeField] private Transform lookPoint;
        [SerializeField] private GameObject target;
        [SerializeField] private float updateTime = 0.25f;
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

            m_difference = target.transform.position - lookPoint.position;
            m_angle = Vector3.Angle(transform.position, transform.forward);
            return m_angle < sightAngle;
        }
    }
}


