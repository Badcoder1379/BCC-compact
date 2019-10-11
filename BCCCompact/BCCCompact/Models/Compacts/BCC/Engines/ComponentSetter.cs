using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class ComponentSetter
    {

        public void Set(HashSet<Component> components)
        {
            var largest = GetLargestComponent(components);
            double sumOfSizes = GetSumOfSizes(components);
            sumOfSizes -= largest.LargestNode.externallRadius;

            double angleCounter = 0;
            foreach (var child in components)
            {
                if (child == largest) continue;
                double angle = Math.PI * 2 * (child.LargestNode.externallRadius / sumOfSizes);
                angle = Math.Min(angle, Math.PI / 2);
                angle /= 2;
                angleCounter += angle;
                double lenght = child.LargestNode.externallRadius / Math.Sin(angle / 2);
                lenght = Math.Max(lenght, largest.LargestNode.externallRadius + child.LargestNode.externallRadius);
                child.LargestNode.XCenter = lenght * Math.Sin(angleCounter);
                child.LargestNode.YCenter = lenght * Math.Cos(angleCounter);
                angleCounter += angle;
            }
        }

        private double GetSumOfSizes(HashSet<Component> components)
        {
            double sumOfSizes = 0;
            foreach (var component in components)
            {
                sumOfSizes += component.LargestNode.externallRadius;
            }
            return sumOfSizes;
        }

        private Component GetLargestComponent(HashSet<Component> components)
        {
            var largest = new Component();
            largest.LargestNode = new Node();
            foreach (var component in components)
            {
                if (component.LargestNode.externallRadius > largest.LargestNode.externallRadius)
                {
                    largest = component;
                }
            }
            return largest;
        }
    }
}