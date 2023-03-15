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

        public GameObject ParentObject { get { return m_parentObject; } set { m_parentObject = value; } }
        
        /// <summary>
        /// This is called on the first frame when in play mode
        /// </summary>
        public virtual object OnNodeStart()
        {
            return null;
        }

        /// <summary>
        /// This is called every frame when in play mode
        /// </summary>
        public virtual object OnNodeUpdate()
        {
            return null;
        }

        /// <summary>
        /// This is called when the node is created in the Graph Window
        /// </summary>
        public virtual void OnCreateNode() {  }

        /// <summary>
        /// Called when the node has exited play mode
        /// </summary>
        public virtual void OnNodeExit() { }
        
        /// <summary>
        /// This is called when the node gets cloned
        /// </summary>
        /// <returns></returns>
        public virtual BaseNode OnCloneNode()
        {
            return Instantiate(this);
        }

        public void GenerateGUID()
        {
            m_guid = GUID.Generate().ToString();
        }

        //--------------- Private -----------------------//

        private GameObject m_parentObject;
        public string m_guid;
        public Vector2 m_position;

        private void OnDisable()
        {
            OnNodeExit();
        }
   }
}


