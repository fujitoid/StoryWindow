using Nekonata.SituationCreator.Model.Implementations;
using Nekonata.SituationCreator.StoryWindow.Model;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Nekonata.SituationCreator.StoryWindow.View.Editor
{
    public class NodeView : Node
    {
        [SerializeField] private Port _inputPort;
        [SerializeField] private Port _outputPort;

        private BaseNode _node;
        private Action<BaseNode> _onNodeSelected;

        public BaseNode Node => _node;

        public Port InputPort => _inputPort;
        public Port OutputPort => _outputPort;

        public NodeView(BaseNode node)
        {
            this._node = node;
            this.title = node.Name;

            this.viewDataKey = node.Guid;

            style.left = node.Position.x;
            style.top = node.Position.y;

            CreateInputPorts();
            CreateOutputPorts();
        }

        internal void SetContext(Action<BaseNode> onNodeSelected)
        {
            _onNodeSelected = onNodeSelected;
        }

        private void CreateInputPorts()
        {
            if (Node is SplitNode)
                _inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));

            if (_inputPort != null)
            {
                _inputPort.portName = string.Empty;
                inputContainer.Add(_inputPort);
            }
        }

        private void CreateOutputPorts()
        {
            if (Node is BaseNode)
                _outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

            if (_outputPort != null)
            {
                _outputPort.portName = string.Empty;
                outputContainer.Add(_outputPort);
            }
        }

        public override void SetPosition(Rect newPosition)
        {
            base.SetPosition(newPosition);
            Vector2 positionToSave = new Vector2(newPosition.xMin, newPosition.yMin);
            _node.SetPosition(positionToSave);
        }

        public override void OnSelected()
        {
            base.OnSelected();
            _onNodeSelected?.Invoke(_node);
        }
    } 
}