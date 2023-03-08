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

        private BaseGraph m_graph;

        public GraphEditor()
        {
            CreateManipulators();
            AddStyleSheet();
            DeleteSelected();
        }

        public void PopulateGraph(BaseGraph graph)
        {
            m_graph = graph;
        }


        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            var nodeTypes = TypeCache.GetTypesDerivedFrom<BaseNode>();
            foreach(var node in nodeTypes)
            {
                evt.menu.AppendAction($"[{node.BaseType.Name}] {node.Name}", action => CreateNode(node, action.eventInfo.mousePosition));
            }
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatilePorts = new List<Port>();

            ports.ForEach( port => 
            {
                if(startPort == port) return;
                if(startPort.node == port.node) return;
                if(startPort.direction == port.direction) return;

                compatilePorts.Add(port);
            });

            return compatilePorts;
        }

        private void CreateNode(Type nodeType, Vector2 position)
        {
            BaseNode node = m_graph.CreateNode(nodeType);
            BaseNodeEditor nodeEditor = new BaseNodeEditor(node);
            nodeEditor.SetPosition(new Rect(position, Vector2.zero));
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
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Node Tool/Editor/Node Tool/GraphEditorWindow.uss");
            this.styleSheets.Add(styleSheet); 
        }

        private void DeleteSelected()
        {
            deleteSelection = (operationName, askUser) =>
            {
                List<BaseNodeEditor> nodesToDelete = new List<BaseNodeEditor>();

                foreach(GraphElement element in selection)
                {
                    if(element is BaseNodeEditor nodeEditor)
                    {
                        nodesToDelete.Add(nodeEditor);
                    }
                }

                //Have to do this to prevent an Error inside Unity
                foreach(BaseNodeEditor nodeEditor in nodesToDelete)
                {
                    m_graph.DeleteNode(nodeEditor.node);

                    RemoveElement(nodeEditor);
                }
            };
        }
    }
}