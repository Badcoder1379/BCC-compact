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
            Component largest = GetLargestComponent(components);
            double sumOfSizes = GetSumOfSizes(components);
            sumOfSizes -= largest.LasrgestNode.externallRadius;

            double angleCounter = 0;
            foreach (Component child in components)
            {
                if (child == largest) continue;
                double angle = Math.PI * 2 * (child.LasrgestNode.externallRadius / sumOfSizes);
                angle = Math.Max(angle, Math.PI);
                angle /= 2;
                angleCounter += angle;
                double lenght = child.LasrgestNode.externallRadius / Math.Sin(angle / 2);
                lenght = Math.Max(lenght, largest.LasrgestNode.externallRadius + child.LasrgestNode.externallRadius);
                child.LasrgestNode.XCenter = lenght * Math.Sin(angleCounter);
                child.LasrgestNode.YCenter = lenght * Math.Cos(angleCounter);
                angleCounter += angle / 2;
            }
        }

        private double GetSumOfSizes(HashSet<Component> components)
        {
            double sumOfSizes = 0;
            foreach (Component component in components)
            {
                sumOfSizes += component.LasrgestNode.externallRadius;
            }
            return sumOfSizes;
        }

        private Component GetLargestComponent(HashSet<Component> components)
        {
            Component largest = new Component();
            largest.LasrgestNode = new Node();
            foreach (Component component in components)
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