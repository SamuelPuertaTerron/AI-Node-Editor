using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool {
    public class SequenceNode : MultiNode {
        private int m_currentNodeIndex = 0;

        public override void OnNodeStart() {
            if (m_currentNodeIndex <= children.Count) {
                BaseNode node = children[m_currentNodeIndex];
                node.OnNodeUpdate();
                m_currentNodeIndex++;
            }
        }

        public override void OnNodeUpdate() {
            if (m_currentNodeIndex <= children.Count - 1) {
                if (m_currentNodeIndex == children.Count) m_currentNodeIndex = 0;

                BaseNode node = children[m_currentNodeIndex];
                node.OnNodeUpdate();
                m_currentNodeIndex++;
            } else {
                m_currentNodeIndex = 0;
            }
        }

        public override void OnNodeExit() {
            m_currentNodeIndex = 0;
        }
    }
}

