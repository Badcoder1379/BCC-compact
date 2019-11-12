using BCCCompact.Models.BCCAlgorithm;
using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    public class BccAlgorithm
    {
        private Dictionary<BccVertex, int> disc = new Dictionary<BccVertex, int>();
        private Dictionary<BccVertex, int> low = new Dictionary<BccVertex, int>();
        private Dictionary<BccVertex, BccVertex> parent = new Dictionary<BccVertex, BccVertex>();
        private int count = 0;
        private Dictionary<int, int> clusterIdOfVertex = new Dictionary<int, int>();
        private LinkedList<BccEdge> stackOfEdges = new LinkedList<BccEdge>();
        private BccPath path = new BccPath();
        private int time = 0;

        public Dictionary<BccVertex, int> NodingComponentFromThisVertex(Component component, BccVertex startingVertex)
        {
            count = 0;
            time = 0;
            disc = new Dictionary<BccVertex, int>();
            low = new Dictionary<BccVertex, int>();
            parent = new Dictionary<BccVertex, BccVertex>();
            clusterIdOfVertex = new Dictionary<int, int>();
            
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
        /// start from selected vertex and try to discover the clusters with looking up from this vertex
        /// </summary>
        /// <param name="startingVertex"></param>
        private void NodingFromThisVertex(BccVertex startingVertex)
        {
            count = 0;
            stackOfEdges = new LinkedList<BccEdge>();
            path = new BccPath();
            disc[startingVertex] = low[startingVertex] = ++time;
            path.Push(startingVertex);

            while (path.Count() > 0)
            {
                IterateOnGraph();
            }

            while (stackOfEdges.Count > 0)
            {
                var edge = stackOfEdges.Last();
                clusterIdOfVertex[edge.Source] = count;
                clusterIdOfVertex[edge.Target] = count;
                stackOfEdges.RemoveLast();
            }
        }

        /// <summary>
        /// return a dictionary that map each vertex to id number of his cluster
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        private Dictionary<BccVertex, int> GetResultOfNoding(Component component)
        {
            var result = new Dictionary<BccVertex, int>();
            int i = 0;
            
            foreach (var vertex in component.Vertices)
            {
                if (clusterIdOfVertex.ContainsKey(vertex.Id))
                {
                    result[vertex] = clusterIdOfVertex[vertex.Id];
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
                    stackOfEdges.AddLast(new BccEdge(u.Id, v.Id));
                    disc[v] = low[v] = ++time;
                    path.Push(v);
                    return;
                }
                else if (v != parent[u] && disc[v] < disc[u])
                {
                    if (low[u] > disc[v])
                        low[u] = disc[v];

                    stackOfEdges.AddLast(new BccEdge(u.Id, v.Id));
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
                        clusterIdOfVertex[e.Source] = count;
                        clusterIdOfVertex[e.Target] = count;
                        stackOfEdges.RemoveLast();
                    }
                    var edge = stackOfEdges.Last();
                    clusterIdOfVertex[edge.Source] = count;
                    clusterIdOfVertex[edge.Target] = count;
                    stackOfEdges.RemoveLast();

                    count++;
                }
            }
        }
    }
}
