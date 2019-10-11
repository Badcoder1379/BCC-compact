using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models.Compacts.MMD.Engines
{
    public class VerticesPicker
    {
        public void Process(Component component)
        {
            Calcute(component.LargestTriangle);
        }

        public void Calcute(Triangle currentTriangle)
        {
            int orbitalCounter = 0;
            foreach (var orbital in currentTriangle.Orbitals)
            {
                double eachVertexAngle = currentTriangle.CenteralAngle / orbital.Count;
                double angleCounter = currentTriangle.BeginAngle;
                foreach (var vertex in orbital)
                {
                    currentTriangle.AddVertexLocation(vertex, orbitalCounter, angleCounter +eachVertexAngle/2);
                    angleCounter += eachVertexAngle;
                }
                orbitalCounter++;
            }
            foreach(var child in currentTriangle.Children)
            {
                Calcute(child);
            }
        }
    }
}