using BCCCompact.Models.BccAlgorithm;
using System.Collections.Generic;

using System.Linq;

namespace BCCCompact.Models
{
    public class BccAlgrtm
    {
        private Dictionary<Vertex, int> disc;
        private Dictionary<Vertex, int> low;
        private Dictionary<Vertex, Vertex> parent;
        private static int count = 0;
        private readonly Dictionary<int, int> classerIdOfVertex = new Dictionary<int, int>();
        private LinkedList<Edge> stackOfEdges;
        private Path path;
        private int time;

        public Dictionary<Vertex, int> NodingComponentFromThisVertex(Component component, Vertex startingVertex)
        {
            time = 0;
            disc = new Dictionary<Vertex, int>();
            low = new Dictionary<Vertex, int>();
            parent = new Dictionary<Vertex, Vertex>();
            foreach (var vertex in component.Vertices)
            {
                disc[vertex] = -1;
                low[vertex] = -1;
                parent[vertex] = null;
            }
            NodingFromThisVertex(startingVertex);
            return GetResultOfNoding(component);
        }
        /// <summary>
        /// start from selected vertex and try to discover the classers with looking up from this vertex
        /// </summary>
        /// <param name="startingVertex"></param>
        private void NodingFromThisVertex(Vertex startingVertex)
        {
            count = 0;
            stackOfEdges = new LinkedList<Edge>();
            path = new Path();
            disc[startingVertex] = low[startingVertex] = ++time;
            path.Push(startingVertex);

            while (path.Count() > 0)
            {
                IterateOnGraph();
            }

            while (stackOfEdges.Count > 0)
            {
                var edge = stackOfEdges.Last();
                classerIdOfVertex[edge.A] = count;
                classerIdOfVertex[edge.B] = count;
                stackOfEdges.RemoveLast();
            }
        }

        /// <summary>
        /// return a dictionary that map each vertex to id number of his classer
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        private Dictionary<Vertex, int> GetResultOfNoding(Component component)
        {
            var result = new Dictionary<Vertex, int>();
            int i = 0;
            foreach (Vertex vertex in component.Vertices)
            {
                if (classerIdOfVertex.ContainsKey(vertex.Id))
                {
                    result[vertex] = classerIdOfVertex[vertex.Id];
                }
                else
                {
                    i++;
                    result[vertex] = -1;
                }

            }
            return result;
        }

        /// <summary>
        /// this method will be call to move on graph and discover
        /// </summary>
        private void IterateOnGraph()
        {
            var u = path.Peek();
            foreach (var v in u.adjacents)
            {
                if (disc[v] == -1)
                {
                    path.ChildrenUp();
                    parent[v] = u;
                    stackOfEdges.AddLast(new Edge(u.Id, v.Id));
                    disc[v] = low[v] = ++time;
                    path.Push(v);
                    return;
                }
                else if (v != parent[u] && disc[v] < disc[u])
                {
                    if (low[u] > disc[v])
                        low[u] = disc[v];

                    stackOfEdges.AddLast(new Edge(u.Id, v.Id));
                }
            }

            var adjacent = path.Pop();
            if (path.Count() > 0)
            {
                u = path.Peek();

                if (low[u] > low[adjacent])
                    low[u] = low[adjacent];

                if ((disc[u] == 1 && path.Children() > 1) || (disc[u] > 1 && low[adjacent] >= disc[u]))
                {
                    while (stackOfEdges.Last().A != u.Id || stackOfEdges.Last().B != adjacent.Id)
                    {
                        var e = stackOfEdges.Last();
                        classerIdOfVertex[e.A] = count;
                        classerIdOfVertex[e.B] = count;
                        stackOfEdges.RemoveLast();
                    }
                    var edge = stackOfEdges.Last();
                    classerIdOfVertex[edge.A] = count;
                    classerIdOfVertex[edge.B] = count;
                    stackOfEdges.RemoveLast();

                    count++;
                }
            }

        }


    }
}