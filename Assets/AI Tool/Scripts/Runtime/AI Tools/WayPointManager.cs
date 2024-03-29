using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool {
    //TODO: Make Multiple Paths of waypoint paths

    [AddComponentMenu("AI Node Tool/ Way Point Manager")]
    [ExecuteInEditMode]
    public sealed class WayPointManager : MonoBehaviour {
        [field: SerializeField] public GameObject WaypointObject { get; set; }
        [field: SerializeField] public AINodeToolInternal.WaypointPath WaypointPathObject { get; set; }
        [SerializeField] public List<AINodeToolInternal.WaypointPath> WaypointPath;

        private void Start() {
            if (FindObjectOfType<WayPointManager>() != this) {
                Debug.LogErrorFormat("Cannot have more than one waypoint manager");
                Debug.Break();
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.black;
            if (WaypointPath != null) {
                for (int i = 0; i < WaypointPath.Count; i++) {
                    for (int j = 0; j < WaypointPath[i].Waypoints.Count - 1; j++) {
                        Gizmos.DrawLine(WaypointPath[i].Waypoints[j].transform.position, WaypointPath[i].Waypoints[j + 1].transform.position);

                        int finalIndex = WaypointPath[i].Waypoints.Count - 1;
                        Gizmos.DrawLine(WaypointPath[i].Waypoints[finalIndex].transform.position, WaypointPath[i].Waypoints[0].transform.position);
                    }
                }
            }
        }
    }
}


