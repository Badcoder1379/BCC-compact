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
            var componentMaker = new ComponentMaker(graph);
            componentMaker.Process();
            var components = componentMaker.Components;

            foreach (var component in components)
            {
                new ClasserMaker(component).Process();
                new ClasserTreeMaker(component).Process();
                new SizeCalculater(component).Process();
                new AroundCirclePicker(component).PickClassers();
            }

            new ComponentSetter(components).SetComponents();

            foreach (var component in components)
            {
                new LocationCalculater(component).Calcute();
            }
        }


    }
}
