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
            sumOfSizes -= largest.lasrgestNode.externallRadius;

            double angleCounter = 0;
            foreach (Component child in components)
            {
                if (child == largest) continue;
                double angle = Math.PI * 2 * (child.lasrgestNode.externallRadius / sumOfSizes);
                angle = Math.Max(angle, Math.PI);
                angle /= 2;
                angleCounter += angle;
                double lenght = child.lasrgestNode.externallRadius / Math.Sin(angle / 2);
                lenght = Math.Max(lenght, largest.lasrgestNode.externallRadius + child.lasrgestNode.externallRadius);
                child.lasrgestNode.XCenter = lenght * Math.Sin(angleCounter);
                child.lasrgestNode.YCenter = lenght * Math.Cos(angleCounter);
                angleCounter += angle / 2;
            }
        }

        private double GetSumOfSizes(HashSet<Component> components)
        {
            double sumOfSizes = 0;
            foreach (Component component in components)
            {
                sumOfSizes += component.lasrgestNode.externallRadius;
            }
            return sumOfSizes;
        }

        private Component GetLargestComponent(HashSet<Component> components)
        {
            Component largest = new Component();
            largest.lasrgestNode = new Node();
            foreach (Component component in components)
            {
                if (component.lasrgestNode.externallRadius > largest.lasrgestNode.externallRadius)
                {
                    largest = component;
                }
            }
            return largest;
        }
    }
}