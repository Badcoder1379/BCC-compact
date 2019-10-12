using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models.Compacts.MMD.Engines
{
    public class LocationCalculater
    {
        private static double EdgeLength = 100;
        public void Process(Component component)
        {
            Calcute(component.LargestTriangle);
        }


        private void Calcute(Triangle currentTriangle)
        {
            foreach(var vertex in currentTriangle.VertexToLocation.Keys)
            {
                double length = currentTriangle.VertexToLocation[vertex].Item1 * EdgeLength;
                double angle = currentTriangle.VertexToLocation[vertex].Item2;
                double x = currentTriangle.XCenter + length * Math.Sin(angle);
                double y = currentTriangle.YCenter + length * Math.Cos(angle);
                vertex.SetLocation(x, y);
            }
            foreach(var child in currentTriangle.Children)
            {
                child.EdgeToParentLenght = EdgeLength * child.EdgeToParentLenght;
                double x = currentTriangle.XCenter + child.EdgeToParentLenght * Math.Sin(child.AngleToConnectToParent);
                double y = currentTriangle.XCenter + child.EdgeToParentLenght * Math.Cos(child.AngleToConnectToParent);
                Calcute(child);
            }
        }
    }
}
