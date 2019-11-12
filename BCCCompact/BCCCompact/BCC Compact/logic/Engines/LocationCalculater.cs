using System;
using System.Linq;

namespace BCCCompact.Models
{
    class LocationCalculater
    {
        private readonly double bounderMLT = 0.7;
        private readonly Component component;

        public LocationCalculater(Component component)
        {
            this.component = component;
        }

        public void Calcute()
        {
            CalcuteClusters(component.LargestCluster);
            CalcuteVertices(component.LargestCluster);
        }

        /// <summary>
        /// now we have angles and lengths and its easy to calcute all locations with sin and cos
        /// </summary>
        /// <param name="component"></param>
        private void CalcuteClusters(Cluster currentCluster)
        {
            foreach (var child in currentCluster.Children)
            {
                child.XCenter = currentCluster.XCenter + child.EdgeToParentLenght * Math.Sin(child.AngleToConnectToParent);
                child.YCenter = currentCluster.YCenter + child.EdgeToParentLenght * Math.Cos(child.AngleToConnectToParent);
                CalcuteClusters(child);
            }
        }

        /// <summary>
        /// now we have angles and lengths and its easy to calcute location with sin and cos
        /// </summary>
        /// <param name="component"></param>
        private void CalcuteVertices(Cluster currentCluster)
        {
            var vertex_angle = currentCluster.AnglesOfInnerVertices;

            if (vertex_angle.Count == 1)
            {
                SetOwnVertexLocation(currentCluster);
            }
            else
            {
                foreach (var vertex in vertex_angle.Keys)
                {
                    double angle = vertex_angle[vertex];
                    double x = currentCluster.XCenter + currentCluster.InternallRadius * Math.Sin(angle) * bounderMLT;
                    double y = currentCluster.YCenter + currentCluster.InternallRadius * Math.Cos(angle) * bounderMLT;
                    vertex.SetLocation(x, y);
                }
            }

            foreach (var child in currentCluster.Children)
            {
                CalcuteVertices(child);
            }
        }

        private void SetOwnVertexLocation(Cluster currentCluster)
        {
            var vertex = currentCluster.AnglesOfInnerVertices.Keys.First();
            double x = currentCluster.XCenter;
            double y = currentCluster.YCenter;
            vertex.SetLocation(x, y);
        }
    }
}
