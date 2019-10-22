using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    class SizeCalculater
    {
        private Classer largestClasser;
        private readonly double firstInternallRadius = 30;

        public void Process(Component component)
        {
            largestClasser = component.LargestClasser;
            Calcute(largestClasser);
        }


        private void Calcute(Classer classer)
        {
            SetSizes(classer);
            double sumOfAllChildsSizes = SumOfChildrenSizes(classer);
            if (sumOfAllChildsSizes == 0)
            {
                classer.ExternallRadius = classer.InternallRadius;
                return;
            }
            var angleShareOfEachVertex = new Dictionary<Vertex, double>();
            double maxExternallRadius = classer.InternallRadius;
            foreach (var vertex in classer.AdjacentClassersWithConnectiongThisVertex.Keys)
            {
                angleShareOfEachVertex[vertex] = GetSumOfChildrenSizes(vertex, classer);
            }
            double allUsedAngle = 0;
            foreach (var vertex in angleShareOfEachVertex.Keys)
            {
                double allVertexAngel = (angleShareOfEachVertex[vertex] / sumOfAllChildsSizes) * Math.PI * 2;
                allVertexAngel = Math.Min(Math.PI, allVertexAngel);
                allUsedAngle += allVertexAngel;
                foreach (var child in classer.AdjacentClassersWithConnectiongThisVertex[vertex])
                {
                    if (child == classer.Parent)
                    {
                        continue;
                    }
                    double angel = (child.ExternallRadius / angleShareOfEachVertex[vertex]) * allVertexAngel;
                    if (angel != 0)
                    {
                        child.AngleShareFromParentCenter = angel;
                        angel /= 2;
                        double minDistanceToCenter = Math.Abs(child.ExternallRadius / Math.Sin(angel));
                        if (minDistanceToCenter < child.ExternallRadius + classer.InternallRadius)
                        {
                            minDistanceToCenter = child.ExternallRadius + classer.InternallRadius;
                        }
                        if (maxExternallRadius < minDistanceToCenter + child.ExternallRadius)
                        {
                            maxExternallRadius = minDistanceToCenter + child.ExternallRadius;
                        }
                        child.EdgeToParentLenght = minDistanceToCenter;
                    }
                }
            }
            classer.FreeAngleAround = (Math.PI * 2) - allUsedAngle;
            classer.ExternallRadius = maxExternallRadius;
            if (classer.Vertices.Count == 1 && classer.Children.Count == 1)
            {
                classer.ExternallRadius /= 2;
            }
        }


        public double GetSumOfChildrenSizes(Vertex vertex, Classer classer)
        {
            double sumOfSize = 0;

            foreach (var child in classer.AdjacentClassersWithConnectiongThisVertex[vertex])
            {
                if (child == classer.Parent)
                {
                    continue;
                }
                sumOfSize += child.ExternallRadius;
            }
            return sumOfSize;
        }


        private void SetSizes(Classer classer)
        {
            SetInternallRadius(classer);
            foreach (var child in classer.Children)
            {
                Calcute(child);
            }
        }

        public double SumOfChildrenSizes(Classer classer)
        {
            double sum = 0;

            foreach (var child in classer.Children)
            {
                if (child.ExternallRadius == 0)
                {
                    Calcute(child);
                }
                sum += child.ExternallRadius;
            }
            return sum;
        }

        private void SetInternallRadius(Classer classer)
        {
            if (classer.Vertices.Count == 1)
            {
                classer.InternallRadius = firstInternallRadius;
            }
            else
            {
                classer.InternallRadius = classer.Vertices.Count * firstInternallRadius;
            }
            classer.ExternallRadius = classer.InternallRadius;
        }

    }
}