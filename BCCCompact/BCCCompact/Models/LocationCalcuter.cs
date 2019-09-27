using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    class LocationCalcuter
    {
        private readonly double bounderMLT = 0.7;

        public void CalcuteNodeLocations(Component component)
        {
            Node fatherNode = component.lasrgestNode;
            CalcuteNodes(fatherNode);
        }

        private void CalcuteNodes(Node currentNode)
        {
            foreach (Node child in currentNode.children)
            {
                child.XCenter = currentNode.XCenter + child.connectionLenght * Math.Sin(child.angleToConnectToParent);
                child.YCenter = currentNode.YCenter + child.connectionLenght * Math.Cos(child.angleToConnectToParent);
                CalcuteNodes(child);
            }
        }

        public void CalcuteVerticseLocation(Component component)
        {
            CalcuteVertices(component.lasrgestNode);
        }
        private void CalcuteVertices(Node currentNode)
        {
            Dictionary<Vertex, double> vertex_angle = currentNode.innerVertices_angle;

            if (vertex_angle.Count == 1)
            {
                SetOwnVertexLocation(currentNode);
            }
            else
            {
                foreach (Vertex vertex in vertex_angle.Keys)
                {
                    double angle = vertex_angle[vertex];
                    double x = currentNode.XCenter + currentNode.internallRadius * Math.Sin(angle) * bounderMLT;
                    double y = currentNode.YCenter + currentNode.internallRadius * Math.Cos(angle) * bounderMLT;
                    vertex.setLocation(x, y);
                }
            }
            foreach (Node child in currentNode.children)
            {
                CalcuteVertices(child);
            }
        }

        private void SetOwnVertexLocation(Node currentNode)
        {
            Vertex vertex = currentNode.innerVertices_angle.Keys.ToList().First();
            double x = currentNode.XCenter;
            double y = currentNode.YCenter;
            vertex.setLocation(x, y);
        }
    }
}