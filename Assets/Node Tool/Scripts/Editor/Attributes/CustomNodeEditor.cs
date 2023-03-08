using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeToolEditor
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomNodeEditor : Attribute
    {
        private Type m_inspectedType;

        public CustomNodeEditor(Type inspectedType)
        {
            this.m_inspectedType = inspectedType;
        }

        public Type GetInspectedType() { return m_inspectedType; }
    }
}


