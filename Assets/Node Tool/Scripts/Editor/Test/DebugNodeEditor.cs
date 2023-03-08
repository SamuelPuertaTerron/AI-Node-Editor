using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeToolEditor
{
    [CustomNodeEditor(typeof(NodeTool.DebugNode))]
    public class DebugNodeEditor : NodeEditor
    {
        public override void DrawNodeEditor()
        {
            GUILayout.Label("Hello World");
        }
    }
}


