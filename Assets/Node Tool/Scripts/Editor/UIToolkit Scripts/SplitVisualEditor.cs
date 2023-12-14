using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace NodeToolEditor.UI {
    public class SplitVisualEditor : TwoPaneSplitView {
        public new class UxmlFactory : UxmlFactory<SplitVisualEditor, TwoPaneSplitView.UxmlTraits> { }
    }
}


