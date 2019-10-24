using System;
using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    public class AroundCirclePicker
    {
        private readonly Classer largestClasser;


        public AroundCirclePicker(Component component) => this.largestClasser = component.LargestClasser;

        /// <summary>
        /// now for each classer we know size and radius and used angle and length and ... 
        /// this method will pick children of each parent classer around his and vertices of each classer too
        /// </summary>
        /// <param name="component"></param>
        public void PickClassers() => Pick(largestClasser);

        
        private void Pick(Classer currentClasser)
        {
            PickVerticesAroundCircle(currentClasser);

            foreach (Classer child in currentClasser.Children)
            {
                Pick(child);
            }
        }

        private LinkedList<Vertex> GetAdjacentyVerticesList(Classer currentClasser)
        {
            var adjacenty = currentClasser.Adjacenty;
            var VerticesList = new LinkedList<Vertex>(adjacenty.Keys);

            if (currentClasser.Parent != null)
            {
                VerticesList.Remove(currentClasser.VertexConnectorToParent);
                VerticesList.AddFirst(currentClasser.VertexConnectorToParent);
            }

            return VerticesList;
        }

        private void PickVerticesAroundCircle(Classer currentClasser)
        {
            var otherVertices = new HashSet<Vertex>(currentClasser.Vertices);
            var adjacenty = currentClasser.Adjacenty;
            var VerticesList = GetAdjacentyVerticesList(currentClasser);

            if (currentClasser.Children.Count == 1 && currentClasser.Vertices.Count == 1)
            {
                var child = currentClasser.Children.ToList().First();
                child.AngleToConnectToParent = currentClasser.AngleToConnectToParent;
            }
            else
            {
                double angleCounter = Math.PI + currentClasser.AngleToConnectToParent;
                foreach (Vertex vertex in VerticesList)
                {
                    var classers = adjacenty[vertex];
                    angleCounter += currentClasser.FreeAngleAround / (VerticesList.Count + 1);
                    double firstAngle = angleCounter;
                    foreach (Classer child in classers.Where(x => x != currentClasser.Parent))
                    {
                        double shareAngle = child.AngleShareFromParentCenter;
                        angleCounter += shareAngle / 2;
                        child.AngleToConnectToParent = angleCounter;
                        angleCounter += shareAngle / 2;
                    }
                    currentClasser.PickVertexByAngle(vertex, (firstAngle + angleCounter) / 2);
                    otherVertices.Remove(vertex);
                }
            }


            SetSomeVerticesAroundAClasser(currentClasser, otherVertices.ToList());
            Arrange(currentClasser);
        }

        private void Arrange(Classer currentClasser)
        {
            var verticesList = new List<Vertex>(currentClasser.AnglesOfInnerVertices.Keys);
            verticesList.Sort();

            SetSomeVerticesAroundAClasser(currentClasser, verticesList);
        }

        private void SetSomeVerticesAroundAClasser(Classer classer, List<Vertex> vertices)
        {
            var count = vertices.Count;
            var i = 0;
            foreach (Vertex vertex in vertices)
            {
                classer.PickVertexByAngle(vertex, Math.PI * 2 * i / count);
                i++;
            }
        }
    }
}