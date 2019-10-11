using System;
using System.Linq;

namespace BCCCompact.Models
{
    class LocationCalculater
    {
        private readonly double bounderMLT = 0.7;

        public void CalcuteNodeLocations(Component component)
        {
            var fatherNode = component.LargestNode;
            CalcuteNodes(fatherNode);
        }

        private void CalcuteNodes(Node currentNode)
        {
            foreach (var child in currentNode.Children)
            {
                child.XCenter = currentNode.XCenter + child.EdgeToParentLenght * Math.Sin(child.AngleToConnectToParent);
                child.YCenter = currentNode.YCenter + child.EdgeToParentLenght * Math.Cos(child.AngleToConnectToParent);
                CalcuteNodes(child);
            }
        }

        public void CalcuteVerticseLocation(Component component)
        {
            CalcuteVertices(component.LargestNode);
        }
        private void CalcuteVertices(Node currentNode)
        {
            var vertex_angle = currentNode.AnglesOfInnerVertices;

            if (vertex_angle.Count == 1)
            {
                SetOwnVertexLocation(currentNode);
            }
            else
            {
                foreach (var vertex in vertex_angle.Keys)
                {
                    double angle = vertex_angle[vertex];
                    double x = currentNode.XCenter + currentNode.internallRadius * Math.Sin(angle) * bounderMLT;
                    double y = currentNode.YCenter + currentNode.internallRadius * Math.Cos(angle) * bounderMLT;
                    vertex.SetLocation(x, y);
                }
            }
            foreach (var child in currentNode.Children)
            {
                CalcuteVertices(child);
            }
        }

        private void SetOwnVertexLocation(Node currentNode)
        {
            var vertex = currentNode.AnglesOfInnerVertices.Keys.ToList().First();
            double x = currentNode.XCenter;
            double y = currentNode.YCenter;
            vertex.SetLocation(x, y);
        }
    }
}