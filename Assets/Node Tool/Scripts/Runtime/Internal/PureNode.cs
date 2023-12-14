using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool {
    public class PureNode : BaseNode {
        public override BaseNode OnCloneNode() {
            return Instantiate(this);
        }
    }
}


