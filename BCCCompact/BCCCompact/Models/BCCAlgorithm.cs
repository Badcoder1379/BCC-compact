using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    class BCCAlgorithm
    {
        static int count = 0, time = 0;
        Dictionary<int, int> vertex_nodeId = new Dictionary<int, int>();
        HashSet<Vertex> vertices;
        LinkedList<Edge> st;
        int DFS_deep = 500;
        public Dictionary<Vertex, int> Process(Component component)
        {
            count = 0;
            time = 0;
            vertex_nodeId = new Dictionary<int, int>();
            vertices = component.GetVertices();
            st = new LinkedList<Edge>();
            foreach (Vertex vertex in vertices)
            {
                if (vertex.disc == -1)
                {
                    Util(vertex, 1);
                }

                int j = 0;
                // If stack is not empty, pop all edges from stack 
                while (st.Count > 0)
                {
                    j = 1;
                    //System.out.print(st.getLast().u + "--" + st.getLast().v + " ");
                    Edge edge = st.Last();
                    vertex_nodeId[edge.u] = count;
                    vertex_nodeId[edge.v] = count;
                    st.RemoveLast();
                }
                if (j == 1)
                {
                    //System.out.println();
                    count++;
                }
            }
            int i = 0;
            Dictionary<Vertex, int> result = new Dictionary<Vertex, int>();
            foreach (Vertex vertex in vertices)
            {
                if (vertex_nodeId.Keys.Contains(vertex.Id))
                {
                    result[vertex] = vertex_nodeId[vertex.Id];
                }
                else
                {
                    i++;
                    result[vertex] = -1;
                }

            }
            return result;
        }

        public void Util(Vertex u, int deep)
        {

            // Initialize discovery time and low value 
            u.disc = u.low = ++time;
            int children = 0;

            // Go through all vertices adjacent to this 

            foreach (Vertex v in u.adjacents)
            { // v is current adjacent of 'u' 

                // If v is not visited yet, then recur for it 
                if (v.disc == -1)
                {
                    children++;
                    v.parent = u;

                    // store the edge in stack 
                    st.AddLast(new Edge(u.Id, v.Id));
                    if (deep < DFS_deep)
                    {
                        Util(v, deep + 1);
                    }

                    // Check if the subtree rooted with 'v' has a 
                    // connection to one of the ancestors of 'u' 
                    // Case 1 -- per Strongly Connected Components Article 
                    if (u.low > v.low)
                        u.low = v.low;

                    // If u is an articulation point, 
                    // pop all edges from stack till u -- v 
                    if ((u.disc == 1 && children > 1) || (u.disc > 1 && v.low >= u.disc))
                    {
                        while (st.Last().u != u.Id || st.Last().v != v.Id)
                        {
                            //System.out.print(st.getLast().u + "--" + st.getLast().v + " ");
                            Edge e = st.Last();
                            vertex_nodeId[e.u] = count;
                            vertex_nodeId[e.v] = count;
                            st.RemoveLast();
                        }
                        //System.out.println(st.getLast().u + "--" + st.getLast().v + " ");
                        Edge edge = st.Last();
                        vertex_nodeId[edge.u] = count;
                        vertex_nodeId[edge.v] = count;
                        st.RemoveLast();

                        count++;
                    }
                }

                // Update low value of 'u' only if 'v' is still in stack 
                // (i.e. it's a back edge, not cross edge). 
                // Case 2 -- per Strongly Connected Components Article 
                else if (v != u.parent && v.disc < u.disc)
                {
                    if (u.low > v.disc)
                        u.low = v.disc;

                    st.AddLast(new Edge(u.Id, v.Id));
                }
            }

        }

    }
}