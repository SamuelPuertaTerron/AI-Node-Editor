using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool
{
    public class TestAIBase : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<AINodeTool.Agent>().SetDestination(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)));
        }

        private void Update()
        {            
        }
    }
}


