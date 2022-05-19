using Nekonata.SituationCreator.Model.Implementations;
using Nekonata.SituationCreator.StoryWindow.Model;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nekonata.SituationCreator.StoryWindow.View.Runtime
{
    public class NodeView : VisualElement
    {
        private TextField _textField;

        private BaseNode _node;
        private Action<BaseNode> _onNodeSelected;
        private Action _onRedrawTree;

        private VisualElement _dradAndDropHolder;

        private bool _isMouseHolded = false;

        public BaseNode Node => _node;

        public NodeView(BaseNode baseNode)
        {
            this.style.position = Position.Absolute;
            this.style.width = 135;
            this.style.height = 75;
            this.style.backgroundColor = new Color(0.3411765f, 0.3411765f, 0.3411765f, 1);

            _textField = new TextField(string.Empty);
            _textField.value = "NodeName";
            _textField.name = "text-field";
            this.Add(_textField);

            var visualElement = new VisualElement();
            visualElement.style.flexDirection = FlexDirection.Row;
            this.Add(visualElement);

            _dradAndDropHolder = new VisualElement();
            _dradAndDropHolder.style.position = Position.Absolute;
            _dradAndDropHolder.style.left = 36;
            _dradAndDropHolder.style.top = 45;
            _dradAndDropHolder.style.width = 65;
            _dradAndDropHolder.style.height = 30;
            _dradAndDropHolder.RegisterCallback<MouseDownEvent>(OnMouseDown);
            _dradAndDropHolder.RegisterCallback<MouseUpEvent>(OnMouseUp);
            _dradAndDropHolder.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            this.Add(_dradAndDropHolder);

            this._node = baseNode;
            _textField.value = _node.Name;

            this.viewDataKey = _node.Guid;
            this.name = _node.Guid;

            this.style.left = _node.Position.x;
            this.style.top = _node.Position.y;
        }

        internal void SetContext(Action<BaseNode> onNodeSelected, Action onRedrawTree)
        {
            _onNodeSelected = onNodeSelected;
            _onRedrawTree = onRedrawTree;
        }

        public void SetPostion(Rect newPosition)
        {
            this.SetPostion(newPosition);
            Vector2 positionToSave = new Vector2(newPosition.xMin, newPosition.yMin);
            _node.SetPosition(positionToSave);
        }
        
        public void SetPostion(Vector2 newPosition)
        {
            this.style.top = newPosition.y - this.layout.height / 2;
            this.style.left = newPosition.x - this.layout.width / 2;
            _node.SetPosition(newPosition);
        }

        private void OnMouseDown(MouseDownEvent downEvent)
        {
            _isMouseHolded = true;

            _dradAndDropHolder.style.left = 0;
            _dradAndDropHolder.style.top = 0;
            _dradAndDropHolder.style.width = 135;
            _dradAndDropHolder.style.height = 75;
        }

        private void OnMouseUp(MouseUpEvent mouseUp)
        {
            _isMouseHolded = false;

            _dradAndDropHolder.style.left = 36;
            _dradAndDropHolder.style.top = 45;
            _dradAndDropHolder.style.width = 65;
            _dradAndDropHolder.style.height = 30;

            _onRedrawTree.Invoke();
        }

        private void OnMouseMove(MouseMoveEvent moveEvent)
        {
            if (_isMouseHolded == false)
                return;

            SetPostion(moveEvent.mousePosition);
            _onRedrawTree.Invoke();
        }
    }
}
