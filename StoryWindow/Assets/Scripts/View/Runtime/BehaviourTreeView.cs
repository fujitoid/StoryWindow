using Nekonata.SituationCreator.StoryWindow.Controllers.TreeAsset.Context;
using Nekonata.SituationCreator.StoryWindow.Model;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nekonata.SituationCreator.StoryWindow.View.Runtime
{
    public class BehaviourTreeView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, BehaviourTreeView.UxmlTraits> { }

        private BehaviourTree _tree;
        private IBehaviourTreeSaver _treeSaver;
        private Background _background;

        private System.Collections.Generic.List<LineDrawer> _lines = new System.Collections.Generic.List<LineDrawer>();

        public BehaviourTreeView()
        {
            if (_tree == null)
                _tree = new BehaviourTree();

            this.style.flexGrow = 1;

            var visualElement = new VisualElement();
            visualElement.style.flexDirection = FlexDirection.Row;
            visualElement.style.height = 40;
            visualElement.name = "commands-container";
            visualElement.style.backgroundColor = new Color(0.3411765f, 0.3411765f, 0.3411765f, 1);
            Add(visualElement);

            var saveButton = new Button(OnSaveClicked);
            saveButton.text = "Save";
            saveButton.name = "save-button";
            visualElement.Add(saveButton);

            var nodesTypesNames = TypeCache.GetTypesDerivedFrom<BaseNode>().Select(x => x.Name).ToList();
            var dropDown = new DropdownField(string.Empty, nodesTypesNames, 0);
            dropDown.name = "dropdown-nodes";
            visualElement.Add(dropDown);

            var createButton = new Button(OnCreateClicked);
            createButton.text = "Create";
            createButton.name = "create-button";
            visualElement.Add(createButton);

            _background = new Background();
            _background.style.flexGrow = 1;
            Add(_background);
        }

        internal void PopulateView(BehaviourTree tree)
        {
            this._tree = tree;

            foreach (var node in _tree.Nodes)
            {
                var chidlren = _tree.GetChildren(node);
                chidlren.ForEach(x =>
                {
                    var parentPos = new Vector3(node.Position.x + 135 / 2, node.Position.y + 75 / 2);
                    var childrenPos = new Vector3(x.Position.x + 135 / 2, x.Position.y + 75 / 2);
                    var lineDrawer = new LineDrawer(parentPos, childrenPos, 3);
                    this.Add(lineDrawer);
                    _lines.Add(lineDrawer);
                });
            }

            foreach (var node in _tree.Nodes)
            {
                CreateNodeView(node);
            }
        }

        private void OnRedrawTree()
        {
            foreach(var line in _lines)
            {
                Remove(line);
            }

            _lines.Clear();

            foreach (var node in _tree.Nodes)
            {
                var chidlren = _tree.GetChildren(node);
                chidlren.ForEach(x =>
                {
                    var parentPos = new Vector3(node.Position.x + 135 / 2, node.Position.y + 75 / 2);
                    var childrenPos = new Vector3(x.Position.x + 135 / 2, x.Position.y + 75 / 2);
                    var lineDrawer = new LineDrawer(parentPos, childrenPos, 3);
                    this.Add(lineDrawer);
                    _lines.Add(lineDrawer);
                });
            }
        }

        private void OnSaveClicked()
        {
            _treeSaver?.Save();
        }

        private void OnCreateClicked()
        {
            var dropdown = this.Q<VisualElement>("commands-container").Q<DropdownField>("dropdown-nodes");
            var currentChoiseName = dropdown.choices[dropdown.index];

            var nodeType = TypeCache.GetTypesDerivedFrom<BaseNode>().FirstOrDefault(x => x.Name == currentChoiseName);

            if (nodeType == default)
                return;

            CreateNode(nodeType);
        }

        private void CreateNode(Type type)
        {
            BaseNode node = _tree.CreateNode(type);
            CreateNodeView(node);
        }

        private void CreateNodeView(BaseNode node)
        {
            NodeView nodeView = new NodeView(node);
            nodeView.SetContext(x => _tree.SetCurrentSelectedNode(x), OnRedrawTree);
            this.Add(nodeView);
        }
    }
}
