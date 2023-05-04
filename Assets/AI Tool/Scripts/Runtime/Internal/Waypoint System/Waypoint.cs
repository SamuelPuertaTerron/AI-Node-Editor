using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeToolInternal {

    //A Waypoint which can be created using the Waypoint window

    public sealed class Waypoint : MonoBehaviour {
        private void OnDrawGizmos() {
            Gizmos.color = Color.black;
            Gizmos.DrawCube(transform.position, new Vector3(1.0f, 1.0f, 1.0f));
        }
    }
}


