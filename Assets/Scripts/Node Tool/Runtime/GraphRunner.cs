using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class GraphRunner : MonoBehaviour
    {
        [SerializeField] private BaseGraph graph;

        private void Start()
        {
            if(graph)
            {    
                graph.Start();
            }
            else 
            {
                Debug.LogError("Graph is null: Please assign it in the inspector");
		return;
            }
        }
    }
}


