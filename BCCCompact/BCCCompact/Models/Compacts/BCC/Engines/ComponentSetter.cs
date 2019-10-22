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
            sumOfSizes -= largest.LargestClasser.ExternallRadius;

            double angleCounter = 0;
            foreach (var child in components)
            {
                if (child == largest) continue;
                double angle = Math.PI * 2 * (child.LargestClasser.ExternallRadius / sumOfSizes);
                angle = Math.Min(angle, Math.PI / 2);
                angle /= 2;
                angleCounter += angle;
                double lenght = child.LargestClasser.ExternallRadius / Math.Sin(angle / 2);
                lenght = Math.Max(lenght, largest.LargestClasser.ExternallRadius + child.LargestClasser.ExternallRadius);
                child.LargestClasser.XCenter = lenght * Math.Sin(angleCounter);
                child.LargestClasser.YCenter = lenght * Math.Cos(angleCounter);
                angleCounter += angle;
            }
        }

        private double GetSumOfSizes(HashSet<Component> components)
        {
            double sumOfSizes = 0;
            foreach (var component in components)
            {
                sumOfSizes += component.LargestClasser.ExternallRadius;
            }
            return sumOfSizes;
        }

        private Component GetLargestComponent(HashSet<Component> components)
        {
            var largest = new Component();
            largest.LargestClasser = new Classer();
            foreach (var component in components)
            {
                if (component.LargestClasser.ExternallRadius > largest.LargestClasser.ExternallRadius)
                {
                    largest = component;
                }
            }
            return largest;
        }
    }
}