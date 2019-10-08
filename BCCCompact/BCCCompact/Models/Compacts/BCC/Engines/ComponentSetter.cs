using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class ComponentSetter
    {

        public void Set(HashSet<Component> components)
        {
            var largest = GetLargestComponent(components);
            double sumOfSizes = GetSumOfSizes(components);
            sumOfSizes -= largest.LasrgestNode.externallRadius;

            double angleCounter = 0;
            foreach (var child in components)
            {
                if (child == largest) continue;
                double angle = Math.PI * 2 * (child.LasrgestNode.externallRadius / sumOfSizes);
                angle = Math.Min(angle, Math.PI/2);
                angle /= 2;
                angleCounter += angle;
                double lenght = child.LasrgestNode.externallRadius / Math.Sin(angle / 2);
                lenght = Math.Max(lenght, largest.LasrgestNode.externallRadius + child.LasrgestNode.externallRadius);
                child.LasrgestNode.XCenter = lenght * Math.Sin(angleCounter);
                child.LasrgestNode.YCenter = lenght * Math.Cos(angleCounter);
                angleCounter += angle;
            }
        }

        private double GetSumOfSizes(HashSet<Component> components)
        {
            double sumOfSizes = 0;
            foreach (var component in components)
            {
                sumOfSizes += component.LasrgestNode.externallRadius;
            }
            return sumOfSizes;
        }

        private Component GetLargestComponent(HashSet<Component> components)
        {
            var largest = new Component();
            largest.LasrgestNode = new Node();
            foreach (var component in components)
            {
                if (component.LasrgestNode.externallRadius > largest.LasrgestNode.externallRadius)
                {
                    largest = component;
                }
            }
            return largest;
        }
    }
}