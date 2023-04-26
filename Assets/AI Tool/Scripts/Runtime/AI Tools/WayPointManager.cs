using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool
{
    using AINodeToolInternal;

    //TODO: Make Multiple Paths of waypoint paths

    [AddComponentMenu("AI Node Tool/ Way Point Manager")]
    [ExecuteInEditMode]
    public class WayPointManager : MonoBehaviour {
        [field: SerializeField] public GameObject WaypointObject { get; private set;  }
        public List<GameObject> WaypointPath { get; set; } = new List<GameObject>(); //Will be added inside the Waypoint Window

        private void Start() {
            if(FindObjectOfType<WayPointManager>() != this) {
                Debug.LogErrorFormat("Cannot have more than one waypoint manager");
            }
        }

        private void Update() {
            if(WaypointPath.Count > 0) {
                Debug.DrawLine(WaypointPath[0].transform.position, WaypointPath[1].transform.position, Color.red);
            }
        }
    }
}

 
