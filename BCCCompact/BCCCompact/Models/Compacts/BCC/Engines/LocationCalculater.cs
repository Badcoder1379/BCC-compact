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
            CalcuteClassers(component.LargestClasser);
            CalcuteVertices(component.LargestClasser);
        }


        /// <summary>
        /// now we have angles and lengths and its easy to calcute all locations with sin and cos
        /// </summary>
        /// <param name="component"></param>


        private void CalcuteClassers(Classer currentClasser)
        {
            foreach (var child in currentClasser.Children)
            {
                child.XCenter = currentClasser.XCenter + child.EdgeToParentLenght * Math.Sin(child.AngleToConnectToParent);
                child.YCenter = currentClasser.YCenter + child.EdgeToParentLenght * Math.Cos(child.AngleToConnectToParent);
                CalcuteClassers(child);
            }
        }

        /// <summary>
        /// now we have angles and lengths and its easy to calcute location with sin and cos
        /// </summary>
        /// <param name="component"></param>
        private void CalcuteVertices(Classer currentClasser)
        {
            var vertex_angle = currentClasser.AnglesOfInnerVertices;

            if (vertex_angle.Count == 1)
            {
                SetOwnVertexLocation(currentClasser);
            }
            else
            {
                foreach (var vertex in vertex_angle.Keys)
                {
                    double angle = vertex_angle[vertex];
                    double x = currentClasser.XCenter + currentClasser.InternallRadius * Math.Sin(angle) * bounderMLT;
                    double y = currentClasser.YCenter + currentClasser.InternallRadius * Math.Cos(angle) * bounderMLT;
                    vertex.SetLocation(x, y);
                }
            }
            foreach (var child in currentClasser.Children)
            {
                CalcuteVertices(child);
            }
        }

        private void SetOwnVertexLocation(Classer currentClasser)
        {
            var vertex = currentClasser.AnglesOfInnerVertices.Keys.ToList().First();
            double x = currentClasser.XCenter;
            double y = currentClasser.YCenter;
            vertex.SetLocation(x, y);
        }
    }
}