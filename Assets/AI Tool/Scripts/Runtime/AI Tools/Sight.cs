using System;
using UnityEngine;

namespace AINodeTool {
    [AddComponentMenu("AI Node Tool / Sight"), DefaultExecutionOrder(-1)]
    public sealed class Sight : MonoBehaviour {
        public bool IsInSight { get; private set; }
        public float Raduis { get { return raduis; } }
        public float Angle { get { return sightAngle; } }

        public delegate void TargetSpotted(GameObject other);
        public event TargetSpotted OnTargetSpotted;
        public delegate void TargetLost();
        public event TargetLost OnTargetLost;

        [SerializeField] private float raduis;
        [SerializeField, Range(0, 360)] private float sightAngle = 90f;
        [SerializeField, Tooltip("This can be kept null. But Represent the transform which the AI will look from")] private Transform lookPoint;
        [SerializeField, Tooltip("The time between the AI checks for objects \n " +
            "The smaller the value the quicker will check \n " +
            "The Larger the value the longer will check")]
        private float updateTime = 0.25f;
        [SerializeField] private LayerMask interactionMask = 9;
        private float m_Angle;
        private float m_CurrentTime;
        private GameObject m_TargetedObject;
        private int m_TargetCount;
        private Collider[] colliders = new Collider[10];

        [Obsolete("Use Agent Behaviour script or the Events")]
        public GameObject GetTargetedObject() {
            if (!m_TargetedObject)
                return m_TargetedObject;

            return null;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update() {
            m_CurrentTime += Time.deltaTime;

            if (m_CurrentTime >= updateTime) {
                m_CurrentTime = 0;

                m_TargetCount = Physics.OverlapSphereNonAlloc(transform.position, 10.0f, colliders, interactionMask, QueryTriggerInteraction.Collide);

                if (m_TargetCount > 0) {
                    for (int i = 0; i < m_TargetCount; i++) {
                        //Check if something is int sight
                        m_TargetedObject = colliders[i].gameObject;
                        if (m_TargetedObject) {
                            IsInSight = IsInSightAngle(m_TargetedObject);
                            if (IsInSight) {
                                if (OnTargetSpotted != null) OnTargetSpotted(m_TargetedObject);
                            } else if (!IsInSight) {
                                if (OnTargetLost != null) OnTargetLost();
                            }
                        }
                    }
                } else {
                    // If was in sight but no longer is found than call the OnTargetLost event and set IsInSight to false
                    if (IsInSight)
                        if (OnTargetLost != null) OnTargetLost();
                    IsInSight = false;
                }
            }
        }

        private bool IsInSightAngle(GameObject obj) {
            if (obj != null) {
                if (lookPoint) {
                    //Gets the angle using the Dot Product
                    float cosAngle = Vector3.Dot((obj.transform.position - lookPoint.position).normalized, transform.forward);
                    m_Angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
                } else {
                    float cosAngle = Vector3.Dot((obj.transform.position - transform.position).normalized, transform.forward);
                    m_Angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
                }

                return m_Angle < sightAngle; //Returns the size of the angle compared to the sightAngle
            } else {
                return false;
            }
        }

        public Vector3 DirectionFromAngle(float angle) {
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
        }
    }
}


