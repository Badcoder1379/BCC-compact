﻿using BCCCompact.Models.BccAlgorithm;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class BccAlgrtm
    {
        private Dictionary<Vertex, int> disc;
        private Dictionary<Vertex, int> low;
        private Dictionary<Vertex, Vertex> parent;
        static int count = 0;
        Dictionary<int, int> NodeIdOfVertex = new Dictionary<int, int>();
        LinkedList<Edge> st;
        Path path;
        int time;

        public Dictionary<Vertex , int> NodingComponentFromThisVertex(Component component, Vertex startingVertex)
        {
            time = 0;
            disc = new Dictionary<Vertex, int>();
            low = new Dictionary<Vertex, int>();
            parent = new Dictionary<Vertex, Vertex>();
            foreach(Vertex vertex in component.Vertices)
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
                Edge edge = st.Last();
                NodeIdOfVertex[edge.u] = count;
                NodeIdOfVertex[edge.v] = count;
                st.RemoveLast();
            }
        }

        private Dictionary<Vertex,int> GetresultOfNoding(Component component)
        {
            Dictionary<Vertex, int> result = new Dictionary<Vertex, int>();
            int i = 0;
            foreach (Vertex vertex in component.Vertices)
            {
                if (NodeIdOfVertex.Keys.Contains(vertex.Id))
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
            Vertex u = path.Peek();
            foreach(Vertex v in u.adjacents)
            {
                if(disc[v] == -1)
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

            Vertex adjacent = path.Pop();
            if (path.Count() > 0)
            {
                u = path.Peek();

                if (low[u] > low[adjacent])
                    low[u] = low[adjacent];

                if ((disc[u] == 1 && path.Children() > 1) || (disc[u] > 1 && low[adjacent] >= disc[u]))
                {
                    while (st.Last().u != u.Id || st.Last().v != adjacent.Id)
                    {
                        Edge e = st.Last();
                        NodeIdOfVertex[e.u] = count;
                        NodeIdOfVertex[e.v] = count;
                        st.RemoveLast();
                    }
                    Edge edge = st.Last();
                    NodeIdOfVertex[edge.u] = count;
                    NodeIdOfVertex[edge.v] = count;
                    st.RemoveLast();

                    count++;
                }
            }
            
        }
        
       
    }
}