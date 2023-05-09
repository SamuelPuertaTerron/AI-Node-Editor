using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeTool
{
    public class EventNode : SingleNode
    {
        public delegate void NodeEvent();
        public event NodeEvent OnEvent;

        public override void OnNodeStart()
        {
            OnEvent += CallEvent;
        }

        public virtual void CallEvent()
        {
        }
    }
}


