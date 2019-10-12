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
            if (triangle.Children.Count == 0)
            {
                triangle.Size = FirstSize;
                return;
            }
            double sum = 0;
            foreach (var child in triangle.Children)
            {
                CalcuteSize(child);
                sum += child.Size;
            }
            triangle.Size = sum;
        }
    }
}
