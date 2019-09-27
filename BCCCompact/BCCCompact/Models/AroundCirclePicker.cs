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
            foreach (Node child in currentNode.children)
            {
                Pick(child);
            }
        }

        private LinkedList<Vertex> GetAdjacentyVerticesList(Node currentNode)
        {
            Dictionary<Vertex, HashSet<Node>> adjacenty = currentNode.adjacenty_vertex_nodes;
            LinkedList<Vertex> VerticesList = new LinkedList<Vertex>(adjacenty.Keys);
            if (currentNode.parent != null)
            {
                VerticesList.Remove(currentNode.connectedToParent);
                VerticesList.AddFirst(currentNode.connectedToParent);
            }
            return VerticesList;
        }

        public void PickAroundCircle(Node currentNode)
        {
            HashSet<Vertex> otherVertices = new HashSet<Vertex>(currentNode.vertices);
            Dictionary<Vertex, HashSet<Node>> adjacenty = currentNode.adjacenty_vertex_nodes;
            LinkedList<Vertex> VerticesList = GetAdjacentyVerticesList(currentNode);

            double angleCounter = 0;
            foreach (Vertex vertex in VerticesList)
            {
                HashSet<Node> nodes = adjacenty[vertex];
                double firstAngle = angleCounter;
                foreach (Node child in nodes)
                {
                    if (child == currentNode.parent)
                    {
                        continue;
                    }
                    double shareAngle = child.parentAngleShare;
                    angleCounter += shareAngle / 2;
                    child.angleToConnectToParent = angleCounter;
                    angleCounter += shareAngle / 2;
                }
                currentNode.PickVertexByAngle(vertex, (firstAngle + angleCounter) / 2);
                if (vertex == currentNode.connectedToParent)
                {
                    currentNode.bestAngleForRotate = firstAngle;
                }
                otherVertices.Remove(vertex);
            }
            SetSomeVerticesAroundANode(currentNode, otherVertices.ToList());
        }

        private void Arrange(Node currentNode)
        {
            List<Vertex> verticesList = new List<Vertex>(currentNode.innerVertices_angle.Keys);
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