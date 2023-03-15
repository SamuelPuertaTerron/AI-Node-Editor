using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeToolEditor
{
    public class NodeEditor 
    {
        public virtual void DrawNodeEditor()
        {

        }

        private void OnGUI()
        {
            DrawNodeEditor();
        }
    }
}


