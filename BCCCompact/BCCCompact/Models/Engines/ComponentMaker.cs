using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class ComponentMaker
    {

        Stack<Object[]> VerticesToUtil = new Stack<object[]>();
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
                    Component component = new Component();
                    Components.Add(component);
                    Object[] firstData = { vertex, VisitedVertices, component };
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

        private void IterateOnVertices(Object[] data)
        {
            Vertex currentVertex = (Vertex)data[0];
            Dictionary<Vertex, bool> visited = (Dictionary<Vertex, bool>)data[1];
            Component component = (Component)data[2];
            visited[currentVertex] = true;
            component.Vertices.Add(currentVertex);

            foreach (Vertex adjacent in currentVertex.adjacents)
            {
                if (!visited[adjacent])
                {
                    object[] newData = { adjacent, visited, component };
                    VerticesToUtil.Push(newData);
                }
            }
        }
    }
}