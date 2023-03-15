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
            viewDataKey = p_node.m_guid;

            style.left = node.m_position.x;
            style.top = node.m_position.y;

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
                if (node is PureNode)
                {
                    input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(BaseNode));
                    inputContainer.Add(input);
                }

                if (node is SingleNode)
                {
                    input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(BaseNode));
                    inputContainer.Add(input);
                }

                if (node is MultiNode)
                {
                    input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(BaseNode));
                    inputContainer.Add(input);
                }
            }
        }

        private void DrawPortOutput()
        {
            if (node is not PureNode)
            {
                if (node is RootNode)
                {
                    output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(BaseNode));

                    outputContainer.Add(output);
                }

                if (node is SingleNode)
                {
                    output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(BaseNode));
                    outputContainer.Add(output);
                }

                if (node is MultiNode)
                {
                    output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(BaseNode));
                    outputContainer.Add(output);
                }
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.m_position.x = newPos.x;
            node.m_position.y = newPos.y;
        }
    }
}


