using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodeTool
{
    public abstract class BaseNode : ScriptableObject
    {
        public string Guid { get { return m_guid; } }
        public Vector2 Position { get { return m_position; } set { m_position = value; } }

        /// <summary>
        /// This is called when the node is created in the Graph Window
        /// </summary>
<<<<<<< HEAD:Assets/Node Tool/Scripts/Runtime/Internal/BaseNode.cs
        public virtual void OnCreateNode() {  }
=======
        public virtual void OnCreateNode() {}
>>>>>>> be552c9bf66ad2f1ddea14e2d779fb9504161242:Assets/Scripts/Node Tool/Runtime/Internal/BaseNode.cs
        
        /// <summary>
        /// This is called when the node is called in the Editor
        /// </summary>
        public virtual object OnNodeUpdate()
        {
            return null;
        }

        public void GenerateGUID()
        {
            m_guid = GUID.Generate().ToString();
        }

        //--------------- Private -----------------------//

        private string m_guid;
        private Vector2 m_position;
   }
}


