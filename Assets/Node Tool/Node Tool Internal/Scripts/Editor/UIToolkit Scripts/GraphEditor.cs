using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using NodeTool;

namespace NodeToolEditor.UI
{
    public class GraphEditor : GraphView
    {
        public new class UxmlFactory : UxmlFactory<GraphEditor, GraphView.UxmlTraits> { }

        public Action<BaseNodeView> OnNodeSelected;

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

            if (graph.rootNode == null)
            {
                graph.rootNode = m_graph.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(graph);
                AssetDatabase.SaveAssets();
            }
            foreach (BaseNode node in graph.nodes)
            {
                CreateNodeEditor(node);
            }

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
            //Make sure a node is not selected

            var nodeTypes = TypeCache.GetTypesDerivedFrom<BaseNode>();
            foreach (var node in nodeTypes)
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
            if (graphViewChange.elementsToRemove != null)
            {
                foreach (var view in graphViewChange.elementsToRemove)
                {
                    BaseNodeView nodeView = view as BaseNodeView;
                    if (nodeView != null)
                    {
                        m_graph.DeleteNode(nodeView.node);
                    }

                    Edge edge = view as Edge;
                    if (edge != null)
                    {
                        BaseNodeView parentView = edge.output.node as BaseNodeView;
                        BaseNodeView childView = edge.input.node as BaseNodeView;
                        m_graph.RemoveChild(parentView.node, childView.node);
                    }
                }
            }

            if (graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    BaseNodeView parentView = edge.output.node as BaseNodeView;
                    BaseNodeView childView = edge.input.node as BaseNodeView;
                    m_graph.AddChild(parentView.node, childView.node);
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
            BaseNodeView nodeEditor = new BaseNodeView(node);
            //nodeEditor.SetPosition(new Rect(position, Vector2.zero));
            nodeEditor.OnNodeSelected = OnNodeSelected;
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
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Node Tool/Node Tool Internal/Editor/Graph Editor/GraphEditorWindow.uss");
            this.styleSheets.Add(styleSheet);
        }
    }
}