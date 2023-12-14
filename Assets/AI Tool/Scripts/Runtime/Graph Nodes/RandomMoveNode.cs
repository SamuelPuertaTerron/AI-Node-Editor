using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeTool;
using UnityEngine.AI;
namespace AINodeTool {
    public enum EChosenAI {
        UnityAI,
        AIToolAI,
    }

    public class RandomMoveNode : PureNode {
        [SerializeField] private float walkRaduis = 100.0f;
        [SerializeField] private EChosenAI chosenAI = EChosenAI.UnityAI;
        private Bounds m_bounds;

        public override void OnNodeStart() {
            //TODO: Find Closet bounds
            m_bounds = FindObjectOfType<Bounds>();
            MoveAI();
        }

        public override void OnNodeUpdate() {
            MoveAI();
        }

        private void MoveAI() {
            switch (chosenAI) {
                case EChosenAI.UnityAI:
                    if (ParentObject.GetComponent<NavMeshAgent>() != null) {
                        if (m_bounds) {

                            //bounds size should always have 0 on Y if in 3D mode or 0 on Z in 2D mode
                            ParentObject.GetComponent<NavMeshAgent>().SetDestination(m_bounds.BoundSize);
                        } else {
                            ParentObject.GetComponent<NavMeshAgent>().SetDestination(new Vector3(Random.Range(-walkRaduis, walkRaduis), Random.Range(-walkRaduis, walkRaduis), Random.Range(-walkRaduis, walkRaduis)));
                        }
                    } else {
                        Debug.LogErrorFormat($"This {ParentObject.name} does not have a Unity NavMeshAgent component. Did you mean AIToolAI?");
                    }
                    break;
                case EChosenAI.AIToolAI:
                    if (ParentObject.GetComponent<AINodeTool.PathfindingAgent>() != null) {
                        if (m_bounds) {
                            //bounds size should always have 0 on Y if in 3D mode or 0 on Z in 2D mode
                            ParentObject.GetComponent<AINodeTool.PathfindingAgent>().SetDestination(m_bounds.BoundSize);
                        } else {
                            ParentObject.GetComponent<AINodeTool.PathfindingAgent>().SetDestination(new Vector3(Random.Range(-walkRaduis, walkRaduis), Random.Range(-walkRaduis, walkRaduis), Random.Range(-walkRaduis, walkRaduis)));
                        }
                    } else {
                        Debug.LogErrorFormat($"This {ParentObject.name} does not have a AI Node Tool PathfindingAgent component. Did you mean UnityAI?");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}


