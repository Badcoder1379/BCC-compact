using System;
using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    public class AroundCirclePicker
    {
        public void PickNodes(Node fatherNode)
        {
            Pick(fatherNode);
        }

        private void Pick(Node currentNode)
        {
            PickVerticesAroundCircle(currentNode);


            foreach (Node child in currentNode.Children)
            {
                Pick(child);
            }


        }

        private LinkedList<Vertex> GetAdjacentyVerticesList(Node currentNode)
        {
            var adjacenty = currentNode.AdjacentNodesWithConnectiongThisVertex;
            var VerticesList = new LinkedList<Vertex>(adjacenty.Keys);

            if (currentNode.Parent != null)
            {
                VerticesList.Remove(currentNode.VertexConnectorToParent);
                VerticesList.AddFirst(currentNode.VertexConnectorToParent);
            }

            return VerticesList;
        }

        public void PickVerticesAroundCircle(Node currentNode)
        {
            var otherVertices = new HashSet<Vertex>(currentNode.Vertices);
            var adjacenty = currentNode.AdjacentNodesWithConnectiongThisVertex;
            var VerticesList = GetAdjacentyVerticesList(currentNode);

            double angleCounter = 0;
            foreach (Vertex vertex in VerticesList)
            {
                var nodes = adjacenty[vertex];
                angleCounter += currentNode.FreeAngleAround / (VerticesList.Count + 1);
                double firstAngle = angleCounter;
                foreach (Node child in nodes.Where(x => x != currentNode.Parent))
                {
                    double shareAngle = child.AngleShareFromParentCenter;
                    angleCounter += shareAngle / 2;
                    child.AngleToConnectToParent = angleCounter;
                    angleCounter += shareAngle / 2;
                }
                currentNode.PickVertexByAngle(vertex, (firstAngle + angleCounter) / 2);
                otherVertices.Remove(vertex);
            }
            SetSomeVerticesAroundANode(currentNode, otherVertices.ToList());
            Arrange(currentNode);
        }

        private void Arrange(Node currentNode)
        {
            var verticesList = new List<Vertex>(currentNode.AnglesOfInnerVertices.Keys);
            verticesList.Sort();

            SetSomeVerticesAroundANode(currentNode, verticesList);
        }

        private void SetSomeVerticesAroundANode(Node node, List<Vertex> vertices)
        {
            var count = vertices.Count;
            var i = 0;
            foreach (Vertex vertex in vertices)
            {
                node.PickVertexByAngle(vertex, Math.PI * 2 * i / count);
                i++;
            }
        }
    }
}