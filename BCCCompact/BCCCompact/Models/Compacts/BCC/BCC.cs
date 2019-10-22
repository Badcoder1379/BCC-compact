using BCCCompact.Models.Compacts;

namespace BCCCompact.Models
{
    public class BCC : Compact
    {
        /// <summary>
        /// this method give the gragh and set the vertices locations
        /// </summary>
        /// <param name="graph"></param>
        public override void Process(Graph graph)
        {
            var componentMaker = new ComponentMaker();
            componentMaker.Process(graph);
            var components = componentMaker.Components;
            var classerMaker = new ClasserMaker();
            var sizeCalcuter = new SizeCalculater();
            var classerTreeMaker = new ClasserTreeMaker();
            var picker = new AroundCirclePicker();

            foreach (var component in components)
            {
                classerMaker.Process(component);
                classerTreeMaker.Process(component);
                sizeCalcuter.Process(component);
                picker.PickClassers(component.LargestClasser);
            }

            var componentSetter = new ComponentSetter();
            componentSetter.Set(components);

            var locationCalcuter = new LocationCalculater();

            foreach (var component in components)
            {
                locationCalcuter.CalcuteClasserLocations(component);
                locationCalcuter.CalcuteVerticseLocation(component);
            }
        }


    }
}
