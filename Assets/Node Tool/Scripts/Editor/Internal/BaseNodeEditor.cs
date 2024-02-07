using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeToolEditor
{
    public class NodeEditor : Editor
    {
        public bool DrawDefualtInspector { get; set; }

        /// <summary>
        /// Draws a custom inspector when a node is selected
        /// </summary>
        public virtual void DrawInspectorGUI()
        {

        }


        //Private --------------------------------------------- //

        public override void OnInspectorGUI()
        {
            if (DrawDefualtInspector)
                base.OnInspectorGUI();

            DrawInspectorGUI();
        }
    }
}


