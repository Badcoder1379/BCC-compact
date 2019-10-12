using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    class SizeCalculater
    {
        private Node largestNode;
        private readonly double firstInternallRadius = 30;

        public void Process(Component component)
        {
            largestNode = component.LargestNode;
            Calcute(largestNode);
        }


        private void Calcute(Node node)
        {
            SetSizes(node);
            double sumOfAllChildsSizes = SumOfChildrenSizes(node);
            if (sumOfAllChildsSizes == 0)
            {
                node.externallRadius = node.internallRadius;
                return;
            }
            var angleShareOfEachVertex = new Dictionary<Vertex, double>();
            double maxExternallRadius = node.internallRadius;
            foreach (var vertex in node.AdjacentNodesWithConnectiongThisVertex.Keys)
            {
                angleShareOfEachVertex[vertex] = GetSumOfChildrenSizes(vertex, node);
            }
            double allUsedAngle = 0;
            foreach (var vertex in angleShareOfEachVertex.Keys)
            {
                double allVertexAngel = (angleShareOfEachVertex[vertex] / sumOfAllChildsSizes) * Math.PI * 2;
                allVertexAngel = Math.Min(Math.PI, allVertexAngel);
                allUsedAngle += allVertexAngel;
                foreach (var child in node.AdjacentNodesWithConnectiongThisVertex[vertex])
                {
                    if (child == node.Parent)
                    {
                        continue;
                    }
                    double angel = (child.externallRadius / angleShareOfEachVertex[vertex]) * allVertexAngel;
                    if (angel != 0)
                    {
                        child.AngleShareFromParentCenter = angel;
                        angel /= 2;
                        double minDistanceToCenter = Math.Abs(child.externallRadius / Math.Sin(angel));
                        if (minDistanceToCenter < child.externallRadius + node.internallRadius)
                        {
                            minDistanceToCenter = child.externallRadius + node.internallRadius;
                        }
                        if (maxExternallRadius < minDistanceToCenter + child.externallRadius)
                        {
                            maxExternallRadius = minDistanceToCenter + child.externallRadius;
                        }
                        child.EdgeToParentLenght = minDistanceToCenter;
                    }
                }
            }
            node.FreeAngleAround = (Math.PI * 2) - allUsedAngle;
            node.externallRadius = maxExternallRadius;
            if (node.Vertices.Count == 1 && node.Children.Count == 1)
            {
                node.externallRadius /= 2;
            }
        }


        public double GetSumOfChildrenSizes(Vertex vertex, Node node)
        {
            double sumOfSize = 0;

            foreach (var child in node.AdjacentNodesWithConnectiongThisVertex[vertex])
            {
                if (child == node.Parent)
                {
                    continue;
                }
                sumOfSize += child.externallRadius;
            }
            return sumOfSize;
        }


        private void SetSizes(Node node)
        {
            SetInternallRadius(node);
            foreach (var child in node.Children)
            {
                Calcute(child);
            }
        }

        public double SumOfChildrenSizes(Node node)
        {
            double sum = 0;

            foreach (var child in node.Children)
            {
                if (child.externallRadius == 0)
                {
                    Calcute(child);
                }
                sum += child.externallRadius;
            }
            return sum;
        }

        private void SetInternallRadius(Node node)
        {
            if (node.Vertices.Count == 1)
            {
                node.internallRadius = firstInternallRadius;
            }
            else
            {
                node.internallRadius = node.Vertices.Count * firstInternallRadius;
            }
            node.externallRadius = node.internallRadius;
        }

    }
}