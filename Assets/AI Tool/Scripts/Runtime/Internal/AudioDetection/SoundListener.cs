using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool {
    public static class SoundListener {
        public static void ListenForSound(Sound sound) {
            Collider[] col = Physics.OverlapSphere(sound.position, sound.raduis);

            for (int i = 0; i < col.Length; i++) {
                if (col[i].TryGetComponent(out AudioDetection audio)) {
                    audio.RespondToSound(sound);
                }
            }
        }
    }
}


