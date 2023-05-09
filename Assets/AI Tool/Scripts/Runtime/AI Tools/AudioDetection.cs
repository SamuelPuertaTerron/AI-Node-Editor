using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool
{
    [AddComponentMenu("AI Node Tool/ Audio Detection")]
    public class AudioDetection : MonoBehaviour
    {
        public float Raduis { get { return raduis; } }
        [SerializeField] private float raduis;

        private void Update() {
            
        }
    }

}

