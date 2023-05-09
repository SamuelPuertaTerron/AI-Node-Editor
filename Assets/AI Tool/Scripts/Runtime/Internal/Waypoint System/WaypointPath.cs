using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeToolInternal
{
    //Will be place when a path is finished.
    public class WaypointPath : MonoBehaviour
    {
        [SerializeField] public List<Waypoint> Waypoints;
        [SerializeField] public bool hasCompletedPath = false;
    }
}


