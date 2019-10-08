using BCCCompact.Models.Compacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class BCC : Compact
    {
        
        public override void Process(Graph graph)
        {
            var componentMaker = new ComponentMaker();
            componentMaker.Process(graph);
            var components = componentMaker.MakeComponents();
            var nodeMaker = new NodeMaker();
            var sizeCalcuter = new SizeCalcuter();
            var nodeTreeMaker = new NodeTreeMaker();
            var picker = new AroundCirclePicker();

            foreach (var component in components)
            {
                nodeMaker.Process(component);
                nodeTreeMaker.Process(component);
                sizeCalcuter.Process(component);
                picker.PickNodes(component.LasrgestNode);
                picker.PickVerticesAroundCircle(component.LasrgestNode);
            }

            var componentSetter = new ComponentSetter();
            componentSetter.Set(components);

            var locationCalcuter = new LocationCalcuter();

            foreach(var component in components)
            {
                locationCalcuter.CalcuteNodeLocations(component);
                locationCalcuter.CalcuteVerticseLocation(component);
            }
        }
    }
}
