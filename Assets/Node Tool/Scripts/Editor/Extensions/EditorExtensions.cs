using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeToolEditor.Extensions {
    public static class EditorExtensions {
        public static bool HasExitedPlayMode() {
            return EditorApplication.isPlayingOrWillChangePlaymode && Application.isPlaying;
        }

        public static bool HasStartedPlayMode() {
            return EditorApplication.isPlayingOrWillChangePlaymode && !Application.isPlaying;
        }
    }
}


