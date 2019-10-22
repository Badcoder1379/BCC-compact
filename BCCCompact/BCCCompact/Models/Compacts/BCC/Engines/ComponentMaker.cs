using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class ComponentMaker
    {
        private readonly Stack<Tuple<Vertex, Dictionary<Vertex, bool>, Component>> verticesToUtil = new Stack<Tuple<Vertex, Dictionary<Vertex, bool>, Component>>();
        private Dictionary<Vertex, bool> visitedVertices;
        public readonly HashSet<Component> Components = new HashSet<Component>();
        private void SetGraph(Graph graph)
        {
            visitedVertices = new Dictionary<Vertex, bool>();
            foreach (Vertex vertex in graph.Vertices)
            {
                visitedVertices[vertex] = false;
            }
        }

        /// <summary>
        /// constructs components from the graph
        /// </summary>
        /// <param name="graph"></param>
        public void Process(Graph graph)
        {
            SetGraph(graph);
            foreach (Vertex vertex in graph.Vertices)
            {
                if (!visitedVertices[vertex])
                {
                    var component = new Component();
                    Components.Add(component);
                    var firstData = new Tuple<Vertex, Dictionary<Vertex, bool>, Component>(vertex, visitedVertices, component);
                    verticesToUtil.Push(firstData);
                    while (verticesToUtil.Count > 0)
                    {
                        IterateOnVertices(verticesToUtil.Pop());
                    }
                }
            }
        }


        private void IterateOnVertices(Tuple<Vertex, Dictionary<Vertex, bool>, Component> functionData)
        {
            var currentVertex = functionData.Item1;
            var visited = functionData.Item2;
            var component = functionData.Item3;
            visited[currentVertex] = true;
            component.Vertices.Add(currentVertex);

            foreach (Vertex adjacent in currentVertex.adjacents)
            {
                if (!visited[adjacent])
                {
                    var newData = new Tuple<Vertex, Dictionary<Vertex, bool>, Component>(adjacent, visited, component);
                    verticesToUtil.Push(newData);
                }
            }
        }
    }
}