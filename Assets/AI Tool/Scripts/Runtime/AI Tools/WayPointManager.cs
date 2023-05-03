using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool
{
    //TODO: Make Multiple Paths of waypoint paths

    [AddComponentMenu("AI Node Tool/ Way Point Manager")]
    [ExecuteInEditMode]
    public class WayPointManager : MonoBehaviour {
        [field: SerializeField] public GameObject WaypointObject { get; private set;  }
        public List<GameObject> WaypointPath { get; set; }
        
        private void Start() {
            WaypointPath = new List<GameObject>();

            if(FindObjectOfType<WayPointManager>() != this) {
                Debug.LogErrorFormat("Cannot have more than one waypoint manager");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            if(WaypointPath.Count > 1)
            {
                for(int i = 0; i < WaypointPath.Count - 1; i++)
                {
                    Gizmos.DrawLine(WaypointPath[i].transform.position, WaypointPath[i + 1].transform.position);
                }
            }
        }
    }
}

 
