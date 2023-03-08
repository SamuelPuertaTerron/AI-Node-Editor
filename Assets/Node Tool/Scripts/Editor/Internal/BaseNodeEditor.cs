using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine.LowLevel;

namespace NodeToolEditor
{
    using NodeTool;

    public class BaseNodeEditor : Node
    {
        public BaseNode node;
        public Port input;
        public Port output;

        public BaseNodeEditor(BaseNode p_node)
        {
            node = p_node;
            title = p_node.name;
            viewDataKey = p_node.Guid;

            style.left = node.Position.x;
            style.top = node.Position.y;

            Draw();
        }

        protected virtual void DrawNodeElements(VisualElement dataContainer)
        {

        }

        private void Draw()
        {
            VisualElement customDataContainer = new VisualElement();
            DrawPortInput();
            DrawPortOutput();

            DrawNodeElements(customDataContainer);

            extensionContainer.Add(customDataContainer);
            RefreshExpandedState();
        }

        private void DrawPortInput()
        {
            if (node is not RootNode)
            {
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(BaseNode));
                inputContainer.Add(input);
            }
        }

        private void DrawPortOutput()
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(BaseNode));

            outputContainer.Add(output);
        }
    }
}


