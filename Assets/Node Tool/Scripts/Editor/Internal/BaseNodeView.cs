using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace NodeToolEditor
{
    using NodeTool;
    using NodeToolEditor.Utils;
    public class BaseNodeView : Node
    {
        //TODO: Move to get set
        public BaseNode BaseNode { get; set; }
        public Port input;
        public Port output;

        public UI.GraphEditor GraphEditor { get; set; }

        public Action<BaseNodeView> OnNodeSelected;
        public Action<BaseNodeView> OnNodeUnselected;

        private delegate void ChangeNodeProperties(Type node);
        private event ChangeNodeProperties OnNodePropertiesChange;

        private VisualElement m_Panel;
        private Label m_NodeName;
        private ColorField m_NodeColour;
        private ColorField m_TextColour;
        private Button m_CloseButton;

        private Color m_NodeSaveColour = Color.gray;
        private Color m_TextSaveColour = Color.white;

        public BaseNodeView(BaseNode p_node)
        {
            NodeData(p_node);
            OnNodePropertiesChange += NodePropertiesChanged;
            Draw();
        }

        protected virtual void DrawNodeElements(VisualElement dataContainer)
        {

        }

        private void SetUpElements()
        {
            m_Panel = GraphEditor.Q<VisualElement>("CustomNodeEditor");
            m_NodeName = GraphEditor.Q<Label>("SelectedNodeName");
            m_NodeColour = GraphEditor.Q<ColorField>("NodeColourPicker");
            m_TextColour = GraphEditor.Q<ColorField>("TextColourPicker");
            m_CloseButton = GraphEditor.Q<Button>("CloseButton");
        }

        private void NodeData(BaseNode p_node)
        {
            BaseNode = p_node;
            title = p_node.name;
            viewDataKey = p_node.m_guid;

            style.left = BaseNode.m_position.x;
            style.top = BaseNode.m_position.y;

            //Load colours from the player prefs and set them accordingly if the namespace is not NodeTool

            if (p_node.GetType().Namespace != "NodeTool")
            {
                if (ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString(BaseNode.Guid + " Node Colour"), out m_NodeSaveColour))
                {
                    titleContainer.style.backgroundColor = m_NodeSaveColour;
                }
                else
                {
                    titleContainer.style.backgroundColor = Color.gray;
                }

                if (ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString(BaseNode.Guid + " Text Colour"), out m_TextSaveColour))
                {
                    titleContainer.Q<Label>().style.color = m_TextSaveColour;
                }
                else
                {
                    titleContainer.Q<Label>().style.color = Color.white;
                }
            }
            else
            {
                titleContainer.style.backgroundColor = Color.gray;
                titleContainer.Q<Label>().style.color = Color.white;
            }
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
            if (BaseNode is not RootNode)
            {
                if (BaseNode is PureNode)
                {
                    input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(BaseNode));
                    inputContainer.Add(input);
                }

                if (BaseNode is SingleNode)
                {
                    input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(BaseNode));
                    inputContainer.Add(input);
                }

                if (BaseNode is MultiNode)
                {
                    input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(BaseNode));
                    inputContainer.Add(input);
                }
            }
        }

        private void DrawPortOutput()
        {
            if (BaseNode is not PureNode)
            {
                if (BaseNode is RootNode)
                {
                    output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(BaseNode));

                    outputContainer.Add(output);
                }

                if (BaseNode is SingleNode)
                {
                    output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(BaseNode));
                    outputContainer.Add(output);
                }

                if (BaseNode is MultiNode)
                {
                    output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(BaseNode));
                    outputContainer.Add(output);
                }
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            BaseNode.m_position.x = newPos.x;
            BaseNode.m_position.y = newPos.y;
        }

        public override void OnSelected()
        {
            base.OnSelected();

            BaseNode.IsSelected = true;

            if (OnNodeSelected != null)
            {
                OnNodeSelected.Invoke(this);
            }
        }

        public override void OnUnselected()
        {
            BaseNode.IsSelected = false;

            if (OnNodeUnselected != null)
                OnNodeUnselected.Invoke(this);
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);
            var types = TypeCache.GetTypesDerivedFrom<BaseNode>();
            foreach (var type in types)
            {
                if (type == BaseNode.GetType())
                {
                    evt.menu.AppendAction($"[Editor Node Properties] {type.Name}", action => EditNodeProperties(type));

                    if (type.Namespace != "NodeTool")
                    {
                        evt.menu.AppendAction($"[Open Script] {type.Name}", action => MenuOpenScript(type));
                        evt.menu.AppendSeparator();
                    }
                }
            }
        }

        public void EditNodeProperties(Type node)
        {
            SetUpElements();

            if (m_Panel != null)
            {
                m_Panel.visible = true;
                m_NodeName.text = "Selected Node: " + node.Name;
                m_NodeColour.value = m_NodeSaveColour;
                titleContainer.style.backgroundColor = m_NodeColour.value;
                m_TextColour.value = m_TextSaveColour;
                titleContainer.Q<Label>().style.color = m_TextColour.value;
                m_CloseButton.clicked += CloseNodeProperties;
            }
        }

        private void NodePropertiesChanged(Type node)
        {
            if (m_Panel != null && m_NodeColour != null && m_NodeName != null)
            {
                if (m_Panel.visible)
                {
                    titleContainer.style.backgroundColor = m_NodeColour.value;
                    titleContainer.Q<Label>().style.color = m_TextColour.value;
                    m_NodeSaveColour = m_NodeColour.value;
                    m_TextSaveColour = m_TextColour.value;

                    NodeToolUtilities.SaveFile(NodeToolUtilities.NodeToolSettingsPath + @"\" + BaseNode.name + " " + BaseNode.Guid.ToString(), ColorUtility.ToHtmlStringRGB(m_NodeSaveColour));

                    PlayerPrefs.SetString(BaseNode.Guid + " Node Colour", ColorUtility.ToHtmlStringRGB(m_NodeSaveColour));
                    PlayerPrefs.SetString(BaseNode.Guid + " Text Colour", ColorUtility.ToHtmlStringRGB(m_TextSaveColour));
                }
            }
        }

        private void CloseNodeProperties()
        {
            if (OnNodePropertiesChange != null) OnNodePropertiesChange(BaseNode.GetType());

            if (m_Panel != null)
            {
                m_Panel.visible = false;
            }
        }

        public void MenuOpenScript(Type script)
        {
            string path = script.Name + ".cs";
            foreach (var assetPath in AssetDatabase.GetAllAssetPaths())
            {
                if (assetPath.EndsWith(path))
                {
                    var mono = (MonoScript)AssetDatabase.LoadAssetAtPath(assetPath, typeof(MonoScript));
                    if (mono != null)
                    {
                        UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(assetPath, 1);
                        break;
                    }
                    else
                    {
                        Debug.LogError("Cannot find Asset");
                    }
                }
            }
        }
    }
}