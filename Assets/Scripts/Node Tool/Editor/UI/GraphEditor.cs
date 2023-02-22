using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using NodeTool;

namespace NodeToolEditor.UI
{
    public class GraphEditor : GraphView
    {
        public new class UxmlFactory : UxmlFactory<GraphEditor, GraphView.UxmlTraits> { }

        private BaseGraph m_graph;

        public GraphEditor()
        {
            AddGridBackground();
            AddManipulators();

            var sytleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Node Tool/GraphEditorWindow.uss");
            styleSheets.Add(sytleSheet);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort => endPort.direction != startPort.direction &&
            endPort.node != startPort.node).ToList();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            var nodes = TypeCache.GetTypesDerivedFrom<BaseNode>();

            foreach (var node in nodes)
            {
                if (node.BaseType.Name == "Node") evt.menu.AppendAction($"[{node.BaseType.Name}] {node.Name}", (a) => CreateNode(node));
            }
        }

        public void PopulateGraph(BaseGraph graph)
        {
            m_graph = graph;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            if (m_graph.rootNode == null)
            {
                m_graph.rootNode = m_graph.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(m_graph);
                AssetDatabase.SaveAssets();
            }
            foreach (BaseNode node in m_graph.nodes)
            {
                CreateNodeEditor(node);
            }

            foreach (var node in m_graph.nodes)
            {
                var children = m_graph.GetChildren(node);
                foreach (var child in children)
                {
                    BaseNodeEditor parentView = FindNode(node);
                    BaseNodeEditor childView = FindNode(child);

                    Edge edge = parentView.output.ConnectTo(childView.input);
                    AddElement(edge);
                }
            }
        }

        private BaseNodeEditor FindNode(BaseNode node)
        {
            return GetNodeByGuid(node.Guid) as BaseNodeEditor;
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }

        private void AddManipulators()
        {
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                foreach (var view in graphViewChange.elementsToRemove)
                {
                    BaseNodeEditor nodeView = view as BaseNodeEditor;
                    if (nodeView != null)
                    {
                        m_graph.DeleteNode(nodeView.node);
                    }

                    Edge edge = view as Edge;
                    if (edge != null)
                    {
                        BaseNodeEditor parentView = edge.output.node as BaseNodeEditor;
                        m_graph.RemoveChild(parentView.node);
                    }
                }
            }

            if (graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    BaseNodeEditor parentView = edge.output.node as BaseNodeEditor;
                    BaseNodeEditor childView = edge.input.node as BaseNodeEditor;
                    m_graph.AddChild(parentView.node, childView.node);
                }
            }

            return graphViewChange;
        }

        private void CreateNode(System.Type type)
        {
            BaseNode node = m_graph.CreateNode(type);
            node.OnCreateNode();
            CreateNodeEditor(node);
        }

        private void CreateNodeEditor(BaseNode node)
        {
            BaseNodeEditor nodeEditor = new BaseNodeEditor(node);
            AddElement(nodeEditor);
        }
    }
}