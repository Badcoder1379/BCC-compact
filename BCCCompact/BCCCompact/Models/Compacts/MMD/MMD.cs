

using BCCCompact.Models.Compacts.MMD.Engines;

namespace BCCCompact.Models.Compacts.MMD
{
    public class MMD : Compact
    {
        public override void Process(Graph graph)
        {
            var componentMaker = new ComponentMaker();
            componentMaker.Process(graph);
            var components = componentMaker.MakeComponents();
            var nodeMaker = new NodeMaker();
            var sizeCalcuter = new Models.SizeCalculater();
            var nodeTreeMaker = new NodeTreeMaker();
            var picker = new AroundCirclePicker();
            var converter = new TriangleMaker();

            foreach (var component in components)
            {
                nodeMaker.Process(component);
                nodeTreeMaker.Process(component);
                converter.Process(component);


                sizeCalcuter.Process(component);
                picker.PickNodes(component.LargestNode);
                picker.PickVerticesAroundCircle(component.LargestNode);
            }

            var componentSetter = new ComponentSetter();
            componentSetter.Set(components);

            var locationCalcuter = new LocationCalculater();

            foreach (var component in components)
            {
                locationCalcuter.CalcuteNodeLocations(component);
                locationCalcuter.CalcuteVerticseLocation(component);
            }
        }
    }
}