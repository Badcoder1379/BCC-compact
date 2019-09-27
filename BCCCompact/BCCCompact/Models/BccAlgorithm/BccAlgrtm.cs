using BCCCompact.Models.BccAlgorithm;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class BccAlgrtm
    {
        static int count = 0;
        Dictionary<int, int> vertex_nodeId = new Dictionary<int, int>();
        LinkedList<Edge> st;
        Path path;
        public Dictionary<Vertex , int> Process(Component component)
        {
            count = 0;
            st = new LinkedList<Edge>();
            Vertex firstVertex = component.Vertices.ToList().First();
            path = new Path();
            path.Push(firstVertex);
            while (path.Count() > 0)
            {
                Util();
            }

            while (st.Count > 0)
            {
                Edge edge = st.Last();
                vertex_nodeId[edge.u] = count;
                vertex_nodeId[edge.v] = count;
                st.RemoveLast();
            }

            return Result(component);
        }

        public Dictionary<Vertex,int> Result(Component component)
        {
            Dictionary<Vertex, int> result = new Dictionary<Vertex, int>();
            int i = 0;
            foreach (Vertex vertex in component.Vertices)
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

        public void Util()
        {
            Vertex u = path.Peek();
            foreach(Vertex v in u.adjacents)
            {
                if(v.disc == -1)
                {
                    path.ChildrenUp();
                    v.parent = u;
                    st.AddLast(new Edge(u.Id, v.Id));
                    path.Push(v);
                    return;
                }
                else if (v != u.parent && v.disc < u.disc)
                {
                    if (u.low > v.disc)
                        u.low = v.disc;

                    st.AddLast(new Edge(u.Id, v.Id));
                }
            }

            Vertex adjacent = path.Pop();
            if (path.Count() > 0)
            {
                u = path.Peek();

                if (u.low > adjacent.low)
                    u.low = adjacent.low;

                if ((u.disc == 1 && path.Children() > 1) || (u.disc > 1 && adjacent.low >= u.disc))
                {
                    while (st.Last().u != u.Id || st.Last().v != adjacent.Id)
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
            
        }
        
       
    }
}