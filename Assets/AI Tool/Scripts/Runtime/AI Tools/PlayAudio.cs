using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AINodeTool;

namespace AINodeTool {
    [AddComponentMenu("AI Node Tool/ Play Audio")]
    public class PlayAudio : MonoBehaviour {
        [SerializeField] private float raduis = 10;
        [SerializeField] private bool debug = true;
        private AudioSource m_Source = null;

        private void Start() {
            m_Source = GetComponent<AudioSource>();
            Play();
        }

        public void Play() {
            if (m_Source.isPlaying)
                return;

            m_Source.Play();

            var sound = new Sound(transform.position, raduis);

            SoundListener.ListenForSound(sound);
        }

        private void OnDrawGizmos() {
            if (debug) {
                Gizmos.DrawWireSphere(transform.position, raduis);
            }
        }
    }
}


