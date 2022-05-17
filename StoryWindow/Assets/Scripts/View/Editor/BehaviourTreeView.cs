using Nekonata.SituationCreator.StoryWindow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Nekonata.SituationCreator.StoryWindow.View.Editor
{
    public class BehaviourTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }

        private BehaviourTree _tree;

        public BehaviourTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/View/Editor/BehaviourTreeEditor.uss");
            styleSheets.Add(styleSheet);
        }

        internal void PopulateView(BehaviourTree tree)
        {
            this._tree = tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            foreach (var node in tree.Nodes)
            {
                CreateNodeView(node);
            }

            foreach (var treeNode in tree.Nodes)
            {
                var children = tree.GetChildren(treeNode);
                children.ForEach(x =>
                {
                    NodeView parentView = GetNodeByGuid(treeNode.Guid) as NodeView;
                    NodeView childView = GetNodeByGuid(x.Guid) as NodeView;

                    Edge edge = parentView.OutputPort.ConnectTo(childView.InputPort);
                    AddElement(edge);
                });
            }
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports
                .Where(endPort =>
                    endPort.direction != startPort.direction
                    && endPort.node != startPort.node)
                .ToList();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(x =>
                {
                    NodeView nodeView = x as NodeView;
                    if (nodeView != null)
                    {
                        _tree.DeleteNode(nodeView.Node);
                    }

                    Edge edge = x as Edge;
                    if (edge != null)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        _tree.RemoveChild(parentView.Node, childView.Node);
                    }
                });
            }

            if (graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(x =>
                {
                    NodeView parentView = x.output.node as NodeView;
                    NodeView childView = x.input.node as NodeView;
                    _tree.AddChild(parentView.Node, childView.Node);
                });
            }

            return graphViewChange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            //base.BuildContextualMenu(evt);
            var types = TypeCache.GetTypesDerivedFrom<BaseNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", x => CreateNode(type));
            }
        }

        private void CreateNode(Type type)
        {
            BaseNode node = _tree.CreateNode(type);
            CreateNodeView(node);
        }

        private void CreateNodeView(BaseNode node)
        {
            NodeView nodeView = new NodeView(node);
            nodeView.SetContext(x => _tree.SetCurrentSelectedNode(x));
            AddElement(nodeView);
        }
    } 
}
