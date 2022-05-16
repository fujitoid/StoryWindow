using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nekonata.SituationCreator.StoryWindow.View.Runtime
{
    public class Background : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<Background, VisualElement.UxmlTraits> { }

        public Background()
        {
            this.style.flexGrow = 1;
            this.style.backgroundColor = new Color(0, 0, 0, 1);
        }
    }
}
