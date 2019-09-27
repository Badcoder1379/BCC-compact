using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class ComponentMaker
    {

        Stack<Object[]> verticesToUtil = new Stack<object[]>();
        Graph graph;
        HashSet<Component> components = new HashSet<Component>();
        Dictionary<Vertex, bool> visited;


        private void SetGraph(Graph graph)
        {
            this.graph = graph;
            visited = new Dictionary<Vertex, bool>();
            foreach (Vertex vertex in graph.vertices)
            {
                visited[vertex] = false;
            }
        }

        public void Process(Graph graph)
        {
            SetGraph(graph);
            foreach (Vertex vertex in graph.vertices)
            {
                if (!visited[vertex])
                {
                    Component component = new Component();
                    components.Add(component);
                    Object[] firstData = { vertex, visited, component };
                    verticesToUtil.Push(firstData);
                    while (verticesToUtil.Count > 0)
                    {
                        Util(verticesToUtil.Pop());
                    }
                }
            }
        }

        public HashSet<Component> MakeComponents()
        {
            return components;
        }

        private void Util(Object[] data)
        {
            Vertex currentVertex = (Vertex)data[0];
            Dictionary<Vertex, bool> visited = (Dictionary<Vertex, bool>)data[1];
            Component component = (Component)data[2];
            visited[currentVertex] = true;
            component.addVertex(currentVertex);

            foreach (Vertex adjacent in currentVertex.adjacents)
            {
                if (!visited[adjacent])
                {
                    object[] newData = { adjacent, visited, component };
                    verticesToUtil.Push(newData);
                }
            }
        }
    }
}