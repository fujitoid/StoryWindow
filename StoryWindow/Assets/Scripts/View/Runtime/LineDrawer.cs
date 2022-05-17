using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nekonata.SituationCreator.StoryWindow.View.Runtime
{
    public class LineDrawer : VisualElement
    {
        private Vector3 _startPosition, _endPostion;
        private float _width;

        public LineDrawer(Vector3 startPosotion, Vector3 endPosition, float width)
        {
            _startPosition = startPosotion;
            _endPostion = endPosition;
            _width = width;

            generateVisualContent += OnGenerateVisualContent;
        }

        private void OnGenerateVisualContent(MeshGenerationContext ctx)
        {
            var angleDeg = Vector3.Angle(_startPosition, _endPostion);

            MeshWriteData mesh = ctx.Allocate(4, 6);
            Vertex[] vertices = new Vertex[4];
            vertices[0].position = _startPosition - new Vector3(0, _width / 2, 0); //bottom left
            vertices[1].position = _startPosition + new Vector3(0, _width / 2, 0); //top left
            vertices[2].position = _endPostion + new Vector3(0, _width / 2, 0); //top right
            vertices[3].position = _endPostion - new Vector3(0, _width / 2, 0); //bottom right

            for (var index = 0; index < vertices.Length; index++)
            {
                vertices[index].position += Vector3.forward * Vertex.nearZ;
                vertices[index].tint = Color.white;
            }

            mesh.SetAllVertices(vertices);
            mesh.SetAllIndices(new ushort[] { 0, 1, 3, 1, 2, 3 });
        }
    } 
}
