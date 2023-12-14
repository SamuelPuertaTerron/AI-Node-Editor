using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool {
    public class Sound {
        public enum SoundType { Defualt = -1, Danger, Intresting };
        public Vector3 position;
        public float raduis;

        public Sound(Vector3 _position, float _raduis) {
            this.position = _position;
            this.raduis = _raduis;
        }
    }
}


