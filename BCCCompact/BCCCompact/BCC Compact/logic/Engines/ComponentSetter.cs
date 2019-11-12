using System;
using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    public class ComponentSetter
    {
        private readonly HashSet<Component> components;

        public ComponentSetter(HashSet<Component> components)
        {
            this.components = components;
        }

        /// <summary>
        /// picks all component around of a circle
        /// </summary>
        /// <param name="components"></param>
        public void SetComponents()
        {
            var largest = GetLargestComponent(components);
            double sumOfSizes = GetSumOfSizes(components);
            sumOfSizes -= largest.LargestCluster.ExternallRadius;

            double angleCounter = 0;

            foreach (var child in components.Where(x => x != largest))
            {
                double angle = Math.PI * 2 * (child.LargestCluster.ExternallRadius / sumOfSizes);
                angle = Math.Min(angle, Math.PI / 2);
                angle /= 2;
                angleCounter += angle;
                double lenght = child.LargestCluster.ExternallRadius / Math.Sin(angle / 2);
                lenght = Math.Max(lenght, largest.LargestCluster.ExternallRadius + child.LargestCluster.ExternallRadius);
                child.LargestCluster.XCenter = lenght * Math.Sin(angleCounter);
                child.LargestCluster.YCenter = lenght * Math.Cos(angleCounter);
                angleCounter += angle;
            }
        }

        private double GetSumOfSizes(HashSet<Component> components)
        {
            double sumOfSizes = 0;

            foreach (var component in components)
            {
                sumOfSizes += component.LargestCluster.ExternallRadius;
            }

            return sumOfSizes;
        }

        private Component GetLargestComponent(HashSet<Component> components)
        {
            var largest = new Component
            {
                LargestCluster = new Cluster()
            };

            foreach (var component in components.Where(x => x.LargestCluster.ExternallRadius > largest.LargestCluster.ExternallRadius))
            {
                largest = component;
            }

            return largest;
        }
    }
}
