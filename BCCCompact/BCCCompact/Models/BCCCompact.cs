using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class BccCompact
    {
        public void Process(Graph graph)
        {
            ComponentMaker componentMaker = new ComponentMaker();
            componentMaker.Process(graph);
            HashSet<Component> components = componentMaker.MakeComponents();

            NodeMaker nodeMaker = new NodeMaker();
            SizeCalcuter sizeCalcuter = new SizeCalcuter();
            NodeTreeMaker nodeTreeMaker = new NodeTreeMaker();

            foreach(Component component in components)
            {
                nodeMaker.Process(component);
                nodeTreeMaker.process(component);
                sizeCalcuter.Process(component);
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