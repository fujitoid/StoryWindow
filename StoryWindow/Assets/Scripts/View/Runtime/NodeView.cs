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

        public BaseNode Node => _node;

        public RadioButton Input => _leftInput;
        public RadioButton Output => _rightOutput;

        public NodeView(BaseNode baseNode)
        {
            this.AddManipulator(new Clickable(OnClick, 0, 0));

            this.style.position = Position.Absolute;
            this.style.width = 135;
            this.style.height = 75;
            this.style.backgroundColor = new Color(0.3411765f, 0.3411765f, 0.3411765f, 1);

            _textField = new TextField(string.Empty);
            _textField.value = "NodeName";
            _textField.name = "text-field";
            _textField.AddManipulator(new Clickable(OnClick, 0, 0));
            this.Add(_textField);

            var visualElement = new VisualElement();
            visualElement.style.flexDirection = FlexDirection.Row;
            visualElement.AddManipulator(new Clickable(OnClick, 0, 0));
            this.Add(visualElement);

            _leftInput = new RadioButton(string.Empty);
            _leftInput.style.width = 95;
            _leftInput.name = "left-input";
            _leftInput.AddManipulator(new Clickable(OnClick, 0, 0));
            visualElement.Add(_leftInput);

            _rightOutput = new RadioButton(string.Empty);
            _rightOutput.style.width = 25;
            _rightOutput.name = "right-output";
            _rightOutput.AddManipulator(new Clickable(OnClick, 0, 0));
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

        private void OnClick()
        {
            Debug.Log(_node.Name);
        }
    }
}
