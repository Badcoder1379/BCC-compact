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
            ComponentSetter componentSetter = new ComponentSetter();
            NodeMaker nodeMaker = new NodeMaker();
            NodeTreeMaker treeMaker = new NodeTreeMaker();
            SizeCalcuter sizeCalcuter = new SizeCalcuter();
            AroundCirclePicker picker = new AroundCirclePicker();
            LocationCalcuter locationCalcuter = new LocationCalcuter();
            foreach (Component component in components)
            {
                nodeMaker.Process(component);
                HashSet<Node> nodes = nodeMaker.GetNodes();
                Node largestNode = nodeMaker.GetLargestNode();
                treeMaker.process(largestNode);
                sizeCalcuter.Process(largestNode);
                picker.PickNodes(largestNode);
            }
            componentSetter.Set(components);
            foreach (Component component in components)
            {
                Node largestNode = component.lasrgestNode;
                locationCalcuter.CalcuteNodeLocations(largestNode);
                locationCalcuter.CalcuteVerticseLocation(largestNode);
            }
        }
    }
}