using Nekonata.SituationCreator.StoryWindow.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nekonata.SituationCreator.StoryWindow.View.Runtime
{
    public class NodeView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<NodeView, VisualElement.UxmlTraits> { }

        private RadioButton _leftInput;
        private RadioButton _rightInput;

        private BaseNode _baseNode;

        public NodeView()
        {
            this.style.position = Position.Absolute;

            this.style.backgroundColor = new Color(0.3411765f, 0.3411765f, 0.3411765f, 1);
        }

        public void Construct(BaseNode baseNode)
        {
            _baseNode = baseNode;
        }
    }
}
