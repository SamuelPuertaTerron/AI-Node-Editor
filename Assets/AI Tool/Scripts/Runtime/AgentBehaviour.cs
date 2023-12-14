using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool {
    using UnityEngine.AI;

    /// <summary>
    /// Base class for all agents to inherit from 
    /// This class contains callbacks for all other components
    /// </summary>

    [AddComponentMenu("AI Node Tool / Agent Behaviour")]
    [DefaultExecutionOrder(-1)]
    public class AgentBehaviour : MonoBehaviour {
        protected NavMeshAgent UnityAgent {
            get {
                if (m_UnityAgent)
                    return m_UnityAgent;
                else
                    return null;
            }
        }
        protected PathfindingAgent AIToolAgent {
            get {
                if (m_AIToolAgent)
                    return m_AIToolAgent;
                else
                    return null;
            }
        }

        protected Sight Sight {
            get {
                if (m_Sight)
                    return m_Sight;
                else
                    return null;
            }
        }

        protected AudioDetection AudioDetection {
            get {
                if (m_AudioDetection)
                    return m_AudioDetection;
                else
                    return null;
            }
        }
        protected WaypointPathfinding WaypointPathfinding {
            get {
                if (m_WaypointPathfinding)
                    return m_WaypointPathfinding;
                else
                    return null;
            }
        }

        protected HealthManager HealthManager { get { return m_HealthManager; } }

        /// <summary>
        /// Called whenever the AI has seen an GameObject with the Sight Layer
        /// </summary>
        /// <param name="other">The GameObject this AI has spotted</param>
        public virtual void OnTargetSpotted(GameObject other) { }
        /// <summary>
        /// Called When the AI has lost the spotted GameObject
        /// </summary>
        public virtual void OnTargetLost() { }
        /// <summary>
        /// Called when the AI has heard a sound from a select sounds.
        /// </summary>
        public virtual void OnAudioHeard(Sound sound) { }
        /// <summary>
        /// Called when the AI has received damage
        /// </summary>
        public virtual void OnTakeDamage() { }
        /// <summary>
        /// Called when the AI has been killed
        /// </summary>
        public virtual void OnDeath() { }

        // ---------------------- Private ----------------------

        private NavMeshAgent m_UnityAgent;
        private PathfindingAgent m_AIToolAgent;
        private Sight m_Sight;
        private AudioDetection m_AudioDetection;
        private WaypointPathfinding m_WaypointPathfinding;
        private HealthManager m_HealthManager;

        private void Start() {
            m_Sight = GetComponent<Sight>();
            m_AudioDetection = GetComponent<AudioDetection>();
            m_AIToolAgent = GetComponent<PathfindingAgent>();
            m_UnityAgent = GetComponent<NavMeshAgent>();
            m_WaypointPathfinding = GetComponent<WaypointPathfinding>();
            m_HealthManager = GetComponent<HealthManager>();

            if (m_Sight != null) {
                m_Sight.OnTargetSpotted += TargetSpotted;
                m_Sight.OnTargetLost += TargetLost;
            }
            if (m_HealthManager != null) {
                m_HealthManager.OnTakeDamage += TakeDamage;
                m_HealthManager.OnDeath += Death;
            }
            if (m_AudioDetection) {
                m_AudioDetection.OnHeardAudio += AudioHeard;
            }
        }

        private void OnDisable() {
            if (m_Sight != null) {
                m_Sight.OnTargetSpotted -= TargetSpotted;
                m_Sight.OnTargetLost -= TargetLost;
            }
            if (m_HealthManager != null) {
                m_HealthManager.OnTakeDamage -= TakeDamage;
                m_HealthManager.OnDeath -= Death;
            }
            if (m_AudioDetection) {
                m_AudioDetection.OnHeardAudio -= AudioHeard;
            }
        }

        private void TargetSpotted(GameObject other) {
            OnTargetSpotted(other);
        }
        public void TargetLost() {
            OnTargetLost();
        }

        private void AudioHeard(Sound sound) {
            OnAudioHeard(sound);
        }

        private void TakeDamage() {
            OnTakeDamage();
        }

        private void Death() {
            OnDeath();
        }
    }
}


