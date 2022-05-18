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

        private RadioButton _leftInput;
        private RadioButton _rightOutput;

        private BaseNode _node;
        private Action<BaseNode> _onNodeSelected;

        private VisualElement _dradAndDropHolder;

        private bool _isMouseHolded = false;

        public BaseNode Node => _node;

        public RadioButton Input => _leftInput;
        public RadioButton Output => _rightOutput;

        public new class UxmlFactory : UxmlFactory<NodeView, NodeView.UxmlTraits> { }

        public NodeView()
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

            _leftInput = new RadioButton(string.Empty);
            _leftInput.style.width = 95;
            _leftInput.name = "left-input";
            visualElement.Add(_leftInput);

            _rightOutput = new RadioButton(string.Empty);
            _rightOutput.style.width = 25;
            _rightOutput.name = "right-output";
            visualElement.Add(_rightOutput);

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
        }

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

            _leftInput = new RadioButton(string.Empty);
            _leftInput.style.width = 95;
            _leftInput.name = "left-input";
            visualElement.Add(_leftInput);

            _rightOutput = new RadioButton(string.Empty);
            _rightOutput.style.width = 25;
            _rightOutput.name = "right-output";
            visualElement.Add(_rightOutput);

            this._node = baseNode;
            _textField.value = _node.Name;

            this.viewDataKey = _node.Guid;
            this.name = _node.Guid;

            this.style.left = _node.Position.x;
            this.style.top = _node.Position.y;

            CreateInputPorts();
            CreateOutputPorts();
        }

        internal void SetContext(Action<BaseNode> onNodeSelected)
        {
            _onNodeSelected = onNodeSelected;
        }

        internal void CreateInputPorts()
        {
            if (_node is SplitNode)
            {
                _leftInput.style.visibility = Visibility.Visible;
                return;
            }

            if (_node is BaseNode)
            {
                _leftInput.style.visibility = Visibility.Hidden;
                return;
            }
        }

        internal void CreateOutputPorts()
        {
            if (_node is BaseNode)
            {
                _leftInput.style.visibility = Visibility.Visible;
                return;
            }
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
        }

        private void OnMouseMove(MouseMoveEvent moveEvent)
        {
            if (_isMouseHolded == false)
                return;

            SetPostion(moveEvent.mousePosition);
        }
    }
}
