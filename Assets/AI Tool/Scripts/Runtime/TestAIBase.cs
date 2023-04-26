using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool
{
    public class TestAIBase : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Start()
        {
           
        }

        private void Update()
        {
            if(UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame){
                GetComponent<AINodeTool.Agent>().SetDestination(RandomVector.RandomMovement(10.0f));
            }
        }
    }
}


