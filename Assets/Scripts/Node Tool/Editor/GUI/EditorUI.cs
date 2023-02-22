using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeToolEditor
{
    public static class EditorUI
    {
        private static GUIStyle m_textSytle = new GUIStyle();

        public static void Header(string text, int fontSize = 35) 
        {
            m_textSytle.fontSize = fontSize;
            m_textSytle.normal.textColor = Color.white;
            GUILayout.Label(text, m_textSytle);
        }
    }
}


