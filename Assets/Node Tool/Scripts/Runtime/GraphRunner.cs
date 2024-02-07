using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    [AddComponentMenu("Node Tool / Graph Runner")]
    public sealed class GraphRunner : MonoBehaviour
    {
        [SerializeField] private BaseGraph graph;

        public void RunOnce()
        {
            graph = graph.Clone();

            foreach (BaseNode node in graph.nodes)
            {
                node.ParentObject = this.gameObject;
            }

            graph.UpdateGraph();
            graph.StartGraph();
        }

        private void Start()
        {
            if (graph)
            {
                graph = graph.Clone();

                foreach (BaseNode node in graph.nodes)
                {
                    node.ParentObject = this.gameObject;
                }

                graph.StartGraph();
            }
            else
            {
                Debug.LogError($"The graph vairable on game object {gameObject.name} is null. Please assign it in the inspector.");
                Debug.Break();
            }
        }

        private void Update()
        {
            graph.UpdateGraph();
        }
    }
}


