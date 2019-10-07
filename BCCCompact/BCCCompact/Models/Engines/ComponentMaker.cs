using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class ComponentMaker
    {
        readonly Stack<Tuple<Vertex, Dictionary<Vertex, bool>, Component>> VerticesToUtil = new Stack<Tuple<Vertex, Dictionary<Vertex, bool>,Component>>();
        HashSet<Component> Components = new HashSet<Component>();
        Dictionary<Vertex, bool> VisitedVertices;


        private void SetGraph(Graph graph)
        {
            VisitedVertices = new Dictionary<Vertex, bool>();
            foreach (Vertex vertex in graph.Vertices)
            {
                VisitedVertices[vertex] = false;
            }
        }

        public void Process(Graph graph)
        {
            SetGraph(graph);
            foreach (Vertex vertex in graph.Vertices)
            {
                if (!VisitedVertices[vertex])
                {
                    var component = new Component();
                    Components.Add(component);
                    var firstData = new Tuple<Vertex, Dictionary<Vertex, bool>, Component>(vertex, VisitedVertices, component);
                    VerticesToUtil.Push(firstData);
                    while (VerticesToUtil.Count > 0)
                    {
                        IterateOnVertices(VerticesToUtil.Pop());
                    }
                }
            }
        }

        

        public HashSet<Component> MakeComponents()
        {
            return Components;
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
                    VerticesToUtil.Push(newData);
                }
            }
        }
    }
}