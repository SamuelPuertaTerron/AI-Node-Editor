using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace NodeToolEditor
{
    using NodeTool;
    using Node = UnityEditor.Experimental.GraphView.Node;

    public class BaseNodeEditor : Node
    {
        public Action<BaseNodeEditor> OnNodeSelected;
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

            CreateInputPorts();
            CreateOutputPorts();
        }

        private void CreateInputPorts()
        {
            if(node is not RootNode) input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));

            if(input != null){
                input.portName = "";
                inputContainer.Add(input);
            }
        }

        private void CreateOutputPorts()
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

            if(output != null)
            {
                output.portName = "";
                outputContainer.Add(output);
            }
        }
    
        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.Position = new Vector2(newPos.x, newPos.y);
        }

        public override void OnSelected()
        {
            base.OnSelected();
            if(OnNodeSelected != null) OnNodeSelected.Invoke(this);
        }
    }
}


