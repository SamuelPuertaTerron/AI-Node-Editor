using UnityEngine;
using UnityEngine.Events;

namespace AINodeToolInternal {

    //A Waypoint which can be created using the Waypoint window

    public sealed class Waypoint : MonoBehaviour {

        public UnityEvent actions;

        //Add actions to perform when the agent is within range    

        private void OnDrawGizmos() {
            Gizmos.color = Color.black;
            Gizmos.DrawCube(transform.position, new Vector3(1.0f, 1.0f, 1.0f));
        }
    }
}


