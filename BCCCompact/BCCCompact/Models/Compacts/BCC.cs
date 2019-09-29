using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class BCC
    {
        public void Process(Graph graph)
        {
            ComponentMaker componentMaker = new ComponentMaker();
            componentMaker.Process(graph);
            HashSet<Component> components = componentMaker.MakeComponents();

            NodeMaker nodeMaker = new NodeMaker();
            SizeCalcuter sizeCalcuter = new SizeCalcuter();
            NodeTreeMaker nodeTreeMaker = new NodeTreeMaker();
            AroundCirclePicker picker = new AroundCirclePicker();

            foreach (Component component in components)
            {
                nodeMaker.Process(component);
                nodeTreeMaker.Process(component);
                sizeCalcuter.Process(component);
                picker.PickNodes(component.LasrgestNode);
                picker.PickAroundCircle(component.LasrgestNode);
            }

            ComponentSetter componentSetter = new ComponentSetter();
            componentSetter.Set(components);

            LocationCalcuter locationCalcuter = new LocationCalcuter();

            foreach(Component component in components)
            {
                locationCalcuter.CalcuteNodeLocations(component);
                locationCalcuter.CalcuteVerticseLocation(component);
            }
        }
    }
}