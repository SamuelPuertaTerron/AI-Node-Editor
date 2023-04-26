using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeToolEditor
{
    [AttributeUsage(AttributeTargets.Class)]
    sealed  class CustomNodeEditorAttribute : Attribute
    {
        private Type m_inspectedType;

        public CustomNodeEditorAttribute(Type inspectedType)
        {
            this.m_inspectedType = inspectedType;
        }

        public Type GetInspectedType() { return m_inspectedType; }
    }
}


