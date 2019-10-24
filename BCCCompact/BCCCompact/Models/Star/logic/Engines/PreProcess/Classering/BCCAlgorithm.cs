using BCCCompact.Models.BccAlgorithm;
using System.Collections.Generic;

using System.Linq;

namespace BCCCompact.Models
{
    public class BCCAlgorithm
    {
        private Dictionary<BCCVertex, int> disc = new Dictionary<BCCVertex, int>();
        private Dictionary<BCCVertex, int> low = new Dictionary<BCCVertex, int>();
        private Dictionary<BCCVertex, BCCVertex> parent = new Dictionary<BCCVertex, BCCVertex>();
        private int count = 0;
        private Dictionary<int, int> classerIdOfVertex = new Dictionary<int, int>();
        private LinkedList<BCCEdge> stackOfEdges = new LinkedList<BCCEdge>();
        private BCCPath path = new BCCPath();
        private int time = 0;

        public Dictionary<BCCVertex, int> NodingComponentFromThisVertex(Component component, BCCVertex startingVertex)
        {
            count = 0;
            time = 0;
            disc = new Dictionary<BCCVertex, int>();
            low = new Dictionary<BCCVertex, int>();
            parent = new Dictionary<BCCVertex, BCCVertex>();
            classerIdOfVertex = new Dictionary<int, int>();
            
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
        private void NodingFromThisVertex(BCCVertex startingVertex)
        {
            count = 0;
            stackOfEdges = new LinkedList<BCCEdge>();
            path = new BCCPath();
            disc[startingVertex] = low[startingVertex] = ++time;
            path.Push(startingVertex);

            while (path.Count() > 0)
            {
                IterateOnGraph();
            }

            while (stackOfEdges.Count > 0)
            {
                var edge = stackOfEdges.Last();
                classerIdOfVertex[edge.Source] = count;
                classerIdOfVertex[edge.Target] = count;
                stackOfEdges.RemoveLast();
            }
        }

        /// <summary>
        /// return a dictionary that map each vertex to id number of his classer
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        private Dictionary<BCCVertex, int> GetResultOfNoding(Component component)
        {
            var result = new Dictionary<BCCVertex, int>();
            int i = 0;
            
            foreach (var vertex in component.Vertices)
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
            
            foreach (var v in u.Adjacents)
            {
                if (disc[v] == -1)
                {
                    path.ChildrenUp();
                    parent[v] = u;
                    stackOfEdges.AddLast(new BCCEdge(u.Id, v.Id));
                    disc[v] = low[v] = ++time;
                    path.Push(v);
                    return;
                }
                else if (v != parent[u] && disc[v] < disc[u])
                {
                    if (low[u] > disc[v])
                        low[u] = disc[v];

                    stackOfEdges.AddLast(new BCCEdge(u.Id, v.Id));
                }
            }

            var adjacent = path.Pop();
            
            if (path.Count() > 0)
            {
                u = path.Peek();

                if (low[u] > low[adjacent])
                {
                    low[u] = low[adjacent];
                }

                if ((disc[u] == 1 && path.Children() > 1) || (disc[u] > 1 && low[adjacent] >= disc[u]))
                {
                    while (stackOfEdges.Last().Source != u.Id || stackOfEdges.Last().Target != adjacent.Id)
                    {
                        var e = stackOfEdges.Last();
                        classerIdOfVertex[e.Source] = count;
                        classerIdOfVertex[e.Target] = count;
                        stackOfEdges.RemoveLast();
                    }
                    var edge = stackOfEdges.Last();
                    classerIdOfVertex[edge.Source] = count;
                    classerIdOfVertex[edge.Target] = count;
                    stackOfEdges.RemoveLast();

                    count++;
                }
            }
        }
    }
}
