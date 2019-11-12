using System;
using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    class SizeCalculater
    {
        private readonly Cluster largestCluster;
        private readonly double firstInternallRadius = 30;

        public SizeCalculater(Component component)
        {
            this.largestCluster = component.LargestCluster;
        }

        /// <summary>
        /// calcutes internul and externul radius of each cluster and will be approves speciallAngle of each cluster(angle that each cluster will used from his parent)
        /// and calcute lenght of edge between clusters and their parent center
        /// now clusters are ready to sort and pick around circle
        /// </summary>
        /// <param name="component"></param>
        public void Process()
        {
            Calcute(largestCluster);
        }

        private void Calcute(Cluster cluster)
        {
            SetSizes(cluster);
            double sumOfAllChildsSizes = SumOfChildrenSizes(cluster);

            if (sumOfAllChildsSizes == 0)
            {
                cluster.ExternallRadius = cluster.InternallRadius;
                return;
            }

            var angleShareOfEachVertex = new Dictionary<BccVertex, double>();
            double maxExternallRadius = cluster.InternallRadius;

            foreach (var vertex in cluster.Adjacenty.Keys)
            {
                angleShareOfEachVertex[vertex] = GetSumOfChildrenSizes(vertex, cluster);
            }

            double allUsedAngle = 0;

            foreach (var vertex in angleShareOfEachVertex.Keys)
            {
                double allVertexAngel = (angleShareOfEachVertex[vertex] / sumOfAllChildsSizes) * Math.PI * 2;
                allVertexAngel = Math.Min(Math.PI, allVertexAngel);
                allUsedAngle += allVertexAngel;

                foreach (var child in cluster.Adjacenty[vertex].Where(x => x != cluster.Parent))
                {
                    double angel = (child.ExternallRadius / angleShareOfEachVertex[vertex]) * allVertexAngel;

                    if (angel != 0)
                    {
                        child.AngleShareFromParentCenter = angel;
                        angel /= 2;
                        double minDistanceToCenter = Math.Abs(child.ExternallRadius / Math.Sin(angel));

                        if (minDistanceToCenter < child.ExternallRadius + cluster.InternallRadius)
                        {
                            minDistanceToCenter = child.ExternallRadius + cluster.InternallRadius;
                        }

                        if (maxExternallRadius < minDistanceToCenter + child.ExternallRadius)
                        {
                            maxExternallRadius = minDistanceToCenter + child.ExternallRadius;
                        }

                        child.EdgeToParentLenght = minDistanceToCenter;
                    }
                }
            }

            cluster.FreeAngleAround = (Math.PI * 2) - allUsedAngle;
            cluster.ExternallRadius = maxExternallRadius;

            if (cluster.Vertices.Count == 1 && cluster.Children.Count == 1)
            {
                cluster.ExternallRadius /= 2;
            }
        }

        /// <summary>
        /// returns sum of size of all children of cluster that connect to this cluster with this vertex
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="cluster"></param>
        /// <returns></returns>
        public double GetSumOfChildrenSizes(BccVertex vertex, Cluster cluster)
        {
            double sumOfSize = 0;

            foreach (var child in cluster.Adjacenty[vertex].Where(x => x != cluster.Parent))
            {
                sumOfSize += child.ExternallRadius;
            }

            return sumOfSize;
        }

        private void SetSizes(Cluster cluster)
        {
            SetInternallRadius(cluster);

            foreach (var child in cluster.Children)
            {
                Calcute(child);
            }
        }

        public double SumOfChildrenSizes(Cluster cluster)
        {
            double sum = 0;

            foreach (var child in cluster.Children)
            {
                if (child.ExternallRadius == 0)
                {
                    Calcute(child);
                }

                sum += child.ExternallRadius;
            }

            return sum;
        }

        private void SetInternallRadius(Cluster cluster)
        {
            if (cluster.Vertices.Count == 1)
            {
                cluster.InternallRadius = firstInternallRadius;
            }
            else
            {
                cluster.InternallRadius = cluster.Vertices.Count * firstInternallRadius;
            }

            cluster.ExternallRadius = cluster.InternallRadius;
        }
    }
}
