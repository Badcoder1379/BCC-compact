using BCCCompact.Models.Compacts.BCC.Engines.ComponentMaker;
using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    public class ComponentMaker
    {
        private readonly Stack<IteratorPack> verticesToUtil = new Stack<IteratorPack>();
        private readonly Dictionary<BCCVertex, bool> visitedVertices = new Dictionary<BCCVertex, bool>();
        public readonly HashSet<Component> Components = new HashSet<Component>();
        private readonly BCCGraph graph;

        public ComponentMaker(BCCGraph graph)
        {
            this.graph = graph;
            foreach (var vertex in graph.Vertices)
            {
                visitedVertices[vertex] = false;

            }
        }

        /// <summary>
        /// constructs components from the graph
        /// </summary>
        /// <param name="graph"></param>
        public void Process()
        {
            foreach (var vertex in graph.Vertices.Where(x => !visitedVertices[x]))
            {
                var component = new Component();
                Components.Add(component);
                var firstPack = new IteratorPack(vertex, component);
                verticesToUtil.Push(firstPack);
                
                while (verticesToUtil.Count > 0)
                {
                    IterateOnVertices(verticesToUtil.Pop());
                }
            }
        }

        private void IterateOnVertices(IteratorPack currentPack)
        {
            var currentVertex = currentPack.vertex;
            var component = currentPack.component;
            visitedVertices[currentVertex] = true;
            component.Vertices.Add(currentVertex);

            foreach (var adjacent in currentVertex.Adjacents.Where(x => !visitedVertices[x]))
            {
                var newPack = new IteratorPack(adjacent, component);
                verticesToUtil.Push(newPack);
            }
        }
    }
}
