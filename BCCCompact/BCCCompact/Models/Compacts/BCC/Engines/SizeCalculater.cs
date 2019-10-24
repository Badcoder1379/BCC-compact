using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    class SizeCalculater
    {
        private readonly Classer largestClasser;
        private readonly double firstInternallRadius = 30;

        public SizeCalculater(Component component)
        {
            this.largestClasser = component.LargestClasser;
        }



        /// <summary>
        /// calcutes internul and externul radius of each classer and will be approves speciallAngle of each classer(angle that each classer will used from his parent)
        /// and calcute lenght of edge between classers and their parent center
        /// now classers are ready to sort and pick around circle
        /// </summary>
        /// <param name="component"></param>
        public void Process()
        {
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
            foreach (var vertex in classer.Adjacenty.Keys)
            {
                angleShareOfEachVertex[vertex] = GetSumOfChildrenSizes(vertex, classer);
            }
            double allUsedAngle = 0;
            foreach (var vertex in angleShareOfEachVertex.Keys)
            {
                double allVertexAngel = (angleShareOfEachVertex[vertex] / sumOfAllChildsSizes) * Math.PI * 2;
                allVertexAngel = Math.Min(Math.PI, allVertexAngel);
                allUsedAngle += allVertexAngel;
                foreach (var child in classer.Adjacenty[vertex])
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

        /// <summary>
        /// returns sum of size of all children of classer that connect to this classer with this vertex
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="classer"></param>
        /// <returns></returns>
        public double GetSumOfChildrenSizes(Vertex vertex, Classer classer)
        {
            double sumOfSize = 0;

            foreach (var child in classer.Adjacenty[vertex])
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