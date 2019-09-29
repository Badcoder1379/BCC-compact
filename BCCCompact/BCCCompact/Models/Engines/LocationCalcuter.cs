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
            Node fatherNode = component.LasrgestNode;
            CalcuteNodes(fatherNode);
        }

        private void CalcuteNodes(Node currentNode)
        {
            foreach (Node child in currentNode.Children)
            {
                child.XCenter = currentNode.XCenter + child.EdgeToParentLenght * Math.Sin(child.AngleToConnectToParent);
                child.YCenter = currentNode.YCenter + child.EdgeToParentLenght * Math.Cos(child.AngleToConnectToParent);
                CalcuteNodes(child);
            }
        }

        public void CalcuteVerticseLocation(Component component)
        {
            CalcuteVertices(component.LasrgestNode);
        }
        private void CalcuteVertices(Node currentNode)
        {
            Dictionary<Vertex, double> vertex_angle = currentNode.AnglesOfInnerVertices;

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
                    vertex.SetLocation(x, y);
                }
            }
            foreach (Node child in currentNode.Children)
            {
                CalcuteVertices(child);
            }
        }

        private void SetOwnVertexLocation(Node currentNode)
        {
            Vertex vertex = currentNode.AnglesOfInnerVertices.Keys.ToList().First();
            double x = currentNode.XCenter;
            double y = currentNode.YCenter;
            vertex.SetLocation(x, y);
        }
    }
}