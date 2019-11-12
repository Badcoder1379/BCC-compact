using System;
using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    public class AroundCirclePicker
    {
        private readonly Cluster largestCluster;

        public AroundCirclePicker(Component component) => this.largestCluster = component.LargestCluster;

        /// <summary>
        /// now for each cluster we know size and radius and used angle and length and ... 
        /// this method will pick children of each parent cluster around his and vertices of each cluster too
        /// </summary>
        /// <param name="component"></param>
        public void PickClusters() => Pick(largestCluster);

        private void Pick(Cluster currentCluster)
        {
            PickVerticesAroundCircle(currentCluster);

            foreach (Cluster child in currentCluster.Children)
            {
                Pick(child);
            }
        }

        private LinkedList<BccVertex> GetAdjacentyVerticesList(Cluster currentCluster)
        {
            var adjacenty = currentCluster.Adjacenty;
            var VerticesList = new LinkedList<BccVertex>(adjacenty.Keys);

            if (currentCluster.Parent != null)
            {
                VerticesList.Remove(currentCluster.VertexConnectorToParent);
                VerticesList.AddFirst(currentCluster.VertexConnectorToParent);
            }

            return VerticesList;
        }

        private void PickVerticesAroundCircle(Cluster currentCluster)
        {
            var otherVertices = new HashSet<BccVertex>(currentCluster.Vertices);
            var adjacenty = currentCluster.Adjacenty;
            var VerticesList = GetAdjacentyVerticesList(currentCluster);

            if (currentCluster.Children.Count == 1 && currentCluster.Vertices.Count == 1)
            {
                var child = currentCluster.Children.First();
                child.AngleToConnectToParent = currentCluster.AngleToConnectToParent;
            }
            else
            {
                double angleCounter = Math.PI + currentCluster.AngleToConnectToParent;

                foreach (var vertex in VerticesList)
                {
                    var clusters = adjacenty[vertex];
                    angleCounter += currentCluster.FreeAngleAround / (VerticesList.Count + 1);
                    double firstAngle = angleCounter;

                    foreach (var child in clusters.Where(x => x != currentCluster.Parent))
                    {
                        double shareAngle = child.AngleShareFromParentCenter;
                        angleCounter += shareAngle / 2;
                        child.AngleToConnectToParent = angleCounter;
                        angleCounter += shareAngle / 2;
                    }

                    currentCluster.PickVertexByAngle(vertex, (firstAngle + angleCounter) / 2);
                    otherVertices.Remove(vertex);
                }
            }

            SetSomeVerticesAroundACluster(currentCluster, otherVertices.ToList());
            Arrange(currentCluster);
        }

        private void Arrange(Cluster currentCluster)
        {
            var verticesList = new List<BccVertex>(currentCluster.AnglesOfInnerVertices.Keys);
            verticesList.Sort();

            SetSomeVerticesAroundACluster(currentCluster, verticesList);
        }

        private void SetSomeVerticesAroundACluster(Cluster cluster, List<BccVertex> vertices)
        {
            var count = vertices.Count;
            var i = 0;

            foreach (var vertex in vertices)
            {
                cluster.PickVertexByAngle(vertex, Math.PI * 2 * i / count);
                i++;
            }
        }
    }
}
