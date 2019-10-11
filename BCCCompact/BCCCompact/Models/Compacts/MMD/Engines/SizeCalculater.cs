using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models.Compacts.MMD
{
    public class SizeCalculater
    {
        private static double FirstSize = 1;
        public void Process(Component component)
        {
            CalcuteSize(component.LargestTriangle);
        }

        private void CalcuteSize(Triangle triangle)
        {
            if (triangle.Children.Count == 0) {
                triangle.Size = FirstSize;
                return;
            }
            double sum = 0;
            foreach(var child in triangle.Children)
            {
                CalcuteSize(child);
                sum += child.Size;
            }
            triangle.Size = sum;
        }

        private void CalcuteCenteralAngles(Triangle currentTriangle)
        {
            double sumOfChildrenSizes = currentTriangle.Size;
            if(currentTriangle.Size == 0)
            {
                return;
            }
            var adjacenty = currentTriangle.AdjacentNodesWithConnectiongThisVertex;
            double angleCounter = currentTriangle.BeginAngle;
            foreach (Vertex vertex in adjacenty.Keys)
            {
                double sumOfSizes = 0;
                foreach (Triangle child in adjacenty[vertex])
                {
                    if (child != currentTriangle.Parent)
                        sumOfSizes += child.Size;
                }
                double allVertexChildrenAngle = (sumOfSizes / sumOfChildrenSizes) * currentTriangle.CenteralAngle;
                allVertexChildrenAngle = Math.Min(allVertexChildrenAngle, Math.PI / 2);
                foreach(Triangle child in adjacenty[vertex])
                {
                    double angle = (child.Size / sumOfSizes) * allVertexChildrenAngle;
                    child.SetAngles(angleCounter, angleCounter + angle);
                }
            }
        }
    }
}