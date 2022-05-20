using Nekonata.SituationCreator.StoryWindow.Controllers;
using Nekonata.SituationCreator.StoryWindow.Controllers.TreeAsset.Context;
using Nekonata.SituationCreator.StoryWindow.Model;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private ICoroutineProvider _coroutineProvider;
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

            var createTransitionButton = new Button(OnCreateTransitionClicked);
            createTransitionButton.text = "new transition";
            createTransitionButton.name = "new-transition-button";
            visualElement.Add(createTransitionButton);
            
            var removeTransitionButton = new Button(OnRemoveTransitionClicked);
            removeTransitionButton.text = "remove transition";
            removeTransitionButton.name = "remove-transition-button";
            visualElement.Add(removeTransitionButton);

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

        internal void SetContext(IBehaviourTreeSaver treeSaver, ICoroutineProvider coroutineProvider)
        {
            _treeSaver = treeSaver;
            _coroutineProvider = coroutineProvider;
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
                var chidlrens = _tree.GetChildren(node);

                foreach(var children in chidlrens)
                {
                    var childrenNode = _tree.Nodes.FirstOrDefault(x => x.GUID == children.GUID);

                    var parentPos = new Vector3(node.Position.x, node.Position.y);
                    var childrenPos = new Vector3(childrenNode.Position.x, childrenNode.Position.y);
                    var lineDrawer = new LineDrawer(parentPos, childrenPos, 3);
                    this.Add(lineDrawer);
                    _lines.Add(lineDrawer);
                }

            }

            foreach (var node in _tree.Nodes)
            {
                var nodeView = this.Q<NodeView>(node.GUID);
                this.Add(nodeView);
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

        private void OnCreateTransitionClicked() => _coroutineProvider.StartCoroutine(CreateTransitionRoutine());

        private IEnumerator CreateTransitionRoutine()
        {
            var currentNode = _tree.CurrentSelectedNode;

            if(currentNode == null)
            {
                Debug.LogWarning("choose node");
                yield break;
            }

            yield return new WaitUntil(() => currentNode.GUID != _tree.CurrentSelectedNode.GUID);
            _tree.AddChild(currentNode, _tree.CurrentSelectedNode);
            OnRedrawTree();
        }

        private void OnRemoveTransitionClicked() => _coroutineProvider.StartCoroutine(RemoveTransitionRoutine());

        private IEnumerator RemoveTransitionRoutine()
        {
            var currentNode = _tree.CurrentSelectedNode;

            if (currentNode == null)
            {
                Debug.LogWarning("choose node");
                yield break;
            }

            yield return new WaitUntil(() => currentNode.GUID != _tree.CurrentSelectedNode.GUID);

            if (_tree.GetChildren(currentNode).FirstOrDefault(x => x.GUID == _tree.CurrentSelectedNode.GUID) == null)
            {
                Debug.LogWarning("Not a child of current Node");
                yield break;
            }

            _tree.RemoveChild(currentNode, _tree.CurrentSelectedNode);
            OnRedrawTree();
        }
    }
}
