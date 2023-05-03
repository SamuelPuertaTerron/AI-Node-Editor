using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NodeToolEditor {
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomNodeColourAttribute : Attribute {

        public Color32 Colour;
        
        public CustomNodeColourAttribute(Color32 c) {
            Colour = c;
        }
    }
}


