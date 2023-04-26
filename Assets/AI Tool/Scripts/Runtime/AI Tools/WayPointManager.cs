using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool
{
    using AINodeToolInternal;

    [AddComponentMenu("AI Node Tool/ Way Point Manager")]
    public class WayPointManager : MonoBehaviour
    {
        private List<Node> m_SplinePath; //Will be created inside an Editor Window
    }
}


