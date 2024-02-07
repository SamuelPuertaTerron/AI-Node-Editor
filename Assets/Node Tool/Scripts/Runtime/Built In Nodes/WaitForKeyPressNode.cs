using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeTool;

namespace NodeTool
{
    public class WaitForKeyPressNode : SingleNode
    {
#if ENABLE_LEGACY_INPUT_MANAGER
                [SerializeField] private KeyCode keyCode;
#endif
        // This is called on the first frame when in play mode
        public override void OnNodeStart()
        {
        }

        // This is called every frame when in play mode
        public override void OnNodeUpdate()
        {
#if ENABLE_INPUT_SYSTEM
            if (UnityEngine.InputSystem.Keyboard.current.anyKey.wasPressedThisFrame)
            {
                childNode.OnNodeUpdate();
            }
#elif ENABLE_LEGACY_INPUT_MANAGER
            if(Input.GetKeyDown(keyCode))
            {
                childNode.OnNodeUpdate();
            }
#endif
        }
    }
}

