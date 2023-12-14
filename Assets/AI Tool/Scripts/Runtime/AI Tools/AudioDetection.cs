using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool {
    using UnityEngine.Audio;

    [AddComponentMenu("AI Node Tool/ Audio Detection")]
    public class AudioDetection : MonoBehaviour {
        public delegate void HeardAudio(Sound sound);
        public event HeardAudio OnHeardAudio;

        public void RespondToSound(Sound sound) {
            if (OnHeardAudio != null) OnHeardAudio(sound);
        }
    }

}

