using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using NodeTool;
using NodeToolEditor.Utils;

namespace NodeToolEditor.UI
{
    public class GraphEditor : GraphView
    {
        public new class UxmlFactory : UxmlFactory<GraphEditor, GraphView.UxmlTraits> { }

        public Action<BaseNodeView> OnNodeSelected;
        public Action<BaseNodeView> OnNodeUnselected;

        private BaseGraph m_graph;

        public GraphEditor()
        {
            CreateManipulators();
            AddStyleSheet();
        }

        public void PopulateGraph(BaseGraph graph)
        {
            m_graph = graph;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            //Creates a root node
            if (graph.rootNode == null)
            {
                graph.rootNode = m_graph.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(graph);
                AssetDatabase.SaveAssets();
            }
            //Create the node GUI when the graph editor window is opened
            foreach (BaseNode node in graph.nodes)
            {
                CreateNodeEditor(node);
            }
            //Connects the graph together when the graph editor window is opened
            foreach (BaseNode node in graph.nodes)
            {
                var children = graph.GetChildren(node);

                foreach (BaseNode child in children)
                {
                    BaseNodeView parentView = FindNodeView(node);
                    BaseNodeView childView = FindNodeView(child);

                    if (childView != null)
                    {
                        Edge edge = parentView.output.ConnectTo(childView.input);
                        AddElement(edge);
                    }
                }
            }
        }


        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            //Make sure a node is not selecteds
            foreach (BaseNode node in m_graph.nodes)
            {
                if (node.IsSelected)
                {
                    goto ExtraOptions;
                }
            }
            //Need to avoid this part
            var nodeTypes = TypeCache.GetTypesDerivedFrom<BaseNode>();
            foreach (Type node in nodeTypes)
            {
                //Remove Internal Nodes
                if (node.Name != "SingleNode" && node.Name != "MultiNode" && node.Name != "PureNode" && node.Name != "RootNode")
                {
                    if (node.Namespace != null)
                        evt.menu.AppendAction($"{node.Namespace} / [{node.BaseType.Name}] {node.Name}", action => CreateNode(node)); //Menu created based with the name of the namespace 
                    else
                        evt.menu.AppendAction($"Custom Created Nodes / [{node.BaseType.Name}] {node.Name}", action => CreateNode(node));
                }
            }

        ExtraOptions:
            evt.menu.AppendSeparator();
            evt.menu.AppendAction("Open Node Save Location", action => NodeToolUtilities.OpenDirectory(NodeToolUtilities.NodeToolSettingsPath));
            evt.menu.AppendAction("Settings", action => Debug.Log("Open Settings"));
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatilePorts = new List<Port>();

            ports.ForEach(port =>
            {
                if (startPort == port) return;
                if (startPort.node == port.node) return;
                if (startPort.direction == port.direction) return;

                compatilePorts.Add(port);
            });

            return compatilePorts;
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            //Deletes the nodes if the elements to remove is not null and remove them from the nodes list
            if (graphViewChange.elementsToRemove != null)
            {
                foreach (var view in graphViewChange.elementsToRemove)
                {
                    BaseNodeView nodeView = view as BaseNodeView;
                    if (nodeView != null)
                    {
                        m_graph.DeleteNode(nodeView.BaseNode);
                    }

                    //Deletes edges if not null
                    Edge edge = view as Edge;
                    if (edge != null)
                    {
                        BaseNodeView parentView = edge.output.node as BaseNodeView;
                        BaseNodeView childView = edge.input.node as BaseNodeView;
                        m_graph.RemoveChild(parentView.BaseNode, childView.BaseNode);
                    }
                }
            }

            //Creates edges if not null
            if (graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    BaseNodeView parentView = edge.output.node as BaseNodeView;
                    BaseNodeView childView = edge.input.node as BaseNodeView;
                    m_graph.AddChild(parentView.BaseNode, childView.BaseNode);
                }
            }

            return graphViewChange;
        }

        private BaseNodeView FindNodeView(BaseNode node)
        {
            return GetNodeByGuid(node.Guid) as BaseNodeView;
        }

        private void CreateNode(Type nodeType)
        {
            BaseNode node = m_graph.CreateNode(nodeType);
            CreateNodeEditor(node);
        }

        private void CreateNodeEditor(BaseNode node)
        {
            BaseNodeView nodeEditor = new BaseNodeView(node)
            {
                GraphEditor = this,
                //nodeEditor.SetPosition(new Rect(position, Vector2.zero));
                OnNodeSelected = OnNodeSelected,
                OnNodeUnselected = OnNodeUnselected
            };
            AddElement(nodeEditor);
        }

        private void CreateManipulators()
        {
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        private void AddStyleSheet()
        {
            AssetDatabase.FindAssets("GraphEditorWindow.uss");
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Node Tool/Editor/Graph Editor/GraphEditorWindow.uss");
            this.styleSheets.Add(styleSheet);
        }
    }
}