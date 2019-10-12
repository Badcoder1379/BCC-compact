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
        static int count = 0;
        private readonly Dictionary<int, int> NodeIdOfVertex = new Dictionary<int, int>();
        private LinkedList<Edge> st;
        Path path;
        int time;

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
            return GetresultOfNoding(component);
        }

        private void NodingFromThisVertex(Vertex startingVertex)
        {
            count = 0;
            st = new LinkedList<Edge>();
            path = new Path();
            disc[startingVertex] = low[startingVertex] = ++time;
            path.Push(startingVertex);

            while (path.Count() > 0)
            {
                IterateOnGraph();
            }

            while (st.Count > 0)
            {
                var edge = st.Last();
                NodeIdOfVertex[edge.A] = count;
                NodeIdOfVertex[edge.B] = count;
                st.RemoveLast();
            }
        }

        private Dictionary<Vertex, int> GetresultOfNoding(Component component)
        {
            var result = new Dictionary<Vertex, int>();
            int i = 0;
            foreach (Vertex vertex in component.Vertices)
            {
                if (NodeIdOfVertex.ContainsKey(vertex.Id))
                {
                    result[vertex] = NodeIdOfVertex[vertex.Id];
                }
                else
                {
                    i++;
                    result[vertex] = -1;
                }

            }
            return result;
        }

        private void IterateOnGraph()
        {
            var u = path.Peek();
            foreach (var v in u.adjacents)
            {
                if (disc[v] == -1)
                {
                    path.ChildrenUp();
                    parent[v] = u;
                    st.AddLast(new Edge(u.Id, v.Id));
                    disc[v] = low[v] = ++time;
                    path.Push(v);
                    return;
                }
                else if (v != parent[u] && disc[v] < disc[u])
                {
                    if (low[u] > disc[v])
                        low[u] = disc[v];

                    st.AddLast(new Edge(u.Id, v.Id));
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
                    while (st.Last().A != u.Id || st.Last().B != adjacent.Id)
                    {
                        var e = st.Last();
                        NodeIdOfVertex[e.A] = count;
                        NodeIdOfVertex[e.B] = count;
                        st.RemoveLast();
                    }
                    var edge = st.Last();
                    NodeIdOfVertex[edge.A] = count;
                    NodeIdOfVertex[edge.B] = count;
                    st.RemoveLast();

                    count++;
                }
            }

        }


    }
}