using BCCCompact.Models.Compacts;
using BCCCompact.Models.Elemans.Star;

namespace BCCCompact.Models
{
    public class BCC
    {

        private readonly Graph graph;

        public BCC(Graph graph)
        {
            this.graph = graph;
        }

        public BCC(StarGraph starGraph)
        {
            graph = new Graph(starGraph.Edges, starGraph.Nodes);
        }



        /// <summary>
        /// this method give the gragh and set the vertices locations
        /// </summary>
        /// <param name="graph"></param>
        public void Process()
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
            graph.ConstructResults();
        }
    }
}
