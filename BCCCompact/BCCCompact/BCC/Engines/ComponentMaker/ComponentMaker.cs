using BCCCompact.Models.Compacts.BCC.Engines.ComponentMaker;
using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class ComponentMaker
    {
        private readonly Stack<IteratorPack> verticesToUtil = new Stack<IteratorPack>();
        private readonly Dictionary<Vertex, bool> visitedVertices = new Dictionary<Vertex, bool>();
        public readonly HashSet<Component> Components = new HashSet<Component>();
        private readonly Graph graph;

        public ComponentMaker(Graph graph)
        {
            this.graph = graph;
            foreach (Vertex vertex in graph.Vertices)
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
            foreach (Vertex vertex in graph.Vertices)
            {
                if (!visitedVertices[vertex])
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
        }


        private void IterateOnVertices(IteratorPack currentPack)
        {
            var currentVertex = currentPack.vertex;
            var component = currentPack.component;
            visitedVertices[currentVertex] = true;
            component.Vertices.Add(currentVertex);

            foreach (Vertex adjacent in currentVertex.Adjacents)
            {
                if (!visitedVertices[adjacent])
                {
                    var newPack = new IteratorPack(adjacent, component);
                    verticesToUtil.Push(newPack);
                }
            }
        }
    }
}