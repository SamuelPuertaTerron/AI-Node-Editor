using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;

namespace NodeToolEditor
{
    using NodeTool;

    public class BaseNodeView : Node
    {
        //TODO: Move to get set
        public BaseNode node;
        public Port input;
        public Port output;

        public Action<BaseNodeView> OnNodeSelected;

        public BaseNodeView(BaseNode p_node)
        {
            NodeData(p_node);
            Draw();
        }
        
        protected virtual void DrawNodeElements(VisualElement dataContainer)
        {
            
        }
        
        private void NodeData(BaseNode p_node)
        {
            node = p_node;
            title = p_node.name;
            viewDataKey = p_node.m_guid;

            style.left = node.m_position.x;
            style.top = node.m_position.y;
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

        public override void OnSelected()
        {
            base.OnSelected();
            if(OnNodeSelected != null)
                OnNodeSelected.Invoke(this);
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);
            var types = TypeCache.GetTypesDerivedFrom<BaseNode>();
            foreach (var type in types)
            {
                if (type.Name == node.GetType().Name && type.Namespace != "NodeTool") //Remove internal nodes from being able to be opened
                {
                    evt.menu.AppendAction($"[Open Script] {type.Name}", action => MenuOpenScript(type));
                    evt.menu.AppendSeparator();
                }
            }
        }

        private void MenuOpenScript(Type script)
        {
            string lPath = script.Name + ".cs";
            foreach (var lAssetPath in AssetDatabase.GetAllAssetPaths())
            {
                if (lAssetPath.EndsWith(lPath))
                {
                    var lScript = (MonoScript)AssetDatabase.LoadAssetAtPath(lAssetPath, typeof(MonoScript));
                    if (lScript != null)
                    {
                        UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(lAssetPath, 1);
                        break;
                    }
                    else
                    {
                        Debug.LogError("Cannot find Asset");
                    }
                }
            }
        }

        public override bool IsCopiable()
        {
            return base.IsCopiable();
        }
    }
}


