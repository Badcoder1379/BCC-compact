using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            PickAroundCircle(currentNode);
            Arrange(currentNode);
            foreach (Node child in currentNode.Children)
            {
                Pick(child);
            }
        }

        private LinkedList<Vertex> GetAdjacentyVerticesList(Node currentNode)
        {
            Dictionary<Vertex, HashSet<Node>> adjacenty = currentNode.AdjacentNodesWithConnectiongThisVertex;
            LinkedList<Vertex> VerticesList = new LinkedList<Vertex>(adjacenty.Keys);
            if (currentNode.Parent != null)
            {
                VerticesList.Remove(currentNode.VertexConnectorToParent);
                VerticesList.AddFirst(currentNode.VertexConnectorToParent);
            }
            return VerticesList;
        }

        public void PickAroundCircle(Node currentNode)
        {
            HashSet<Vertex> otherVertices = new HashSet<Vertex>(currentNode.Vertices);
            Dictionary<Vertex, HashSet<Node>> adjacenty = currentNode.AdjacentNodesWithConnectiongThisVertex;
            LinkedList<Vertex> VerticesList = GetAdjacentyVerticesList(currentNode);

            double angleCounter = 0;
            foreach (Vertex vertex in VerticesList)
            {
                HashSet<Node> nodes = adjacenty[vertex];
                double firstAngle = angleCounter;
                foreach (Node child in nodes)
                {
                    if (child == currentNode.Parent)
                    {
                        continue;
                    }
                    double shareAngle = child.AngleShareFromParentCenter;
                    angleCounter += shareAngle / 2;
                    child.AngleToConnectToParent = angleCounter;
                    angleCounter += shareAngle / 2;
                }
                currentNode.PickVertexByAngle(vertex, (firstAngle + angleCounter) / 2);
                otherVertices.Remove(vertex);
            }
            SetSomeVerticesAroundANode(currentNode, otherVertices.ToList());
        }

        private void Arrange(Node currentNode)
        {
            List<Vertex> verticesList = new List<Vertex>(currentNode.AnglesOfInnerVertices.Keys);
            verticesList.Sort();
            Dictionary<Vertex, double> dic = new Dictionary<Vertex, double>();
            
            SetSomeVerticesAroundANode(currentNode, verticesList);
        }

        public void SetSomeVerticesAroundANode(Node node, List<Vertex> vertices)
        {
            int count = vertices.Count;
            int i = 0;
            foreach (Vertex vertex in vertices)
            {
                node.PickVertexByAngle(vertex, Math.PI * 2 * i / count);
                i++;
            }
        }
    }
}