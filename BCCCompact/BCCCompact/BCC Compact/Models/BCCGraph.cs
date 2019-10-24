using BCCCompact.Models.Elemans.Star;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCCCompact.Models
{
    public class BCCGraph
    {
        public int V;
        public List<Vertex> Vertices = new List<Vertex>();

        private void AddEdge(int v, int w)
        {
            Vertices[v].AddAdjacent(Vertices[w]);
        }


        public BCCGraph(int v, List<BCCEdge> edges)
        {
            V = v;
            for (int i = 0; i < V; i++)
            {
                Vertices.Add(new Vertex(i));
            }
            foreach (var edge in edges)
            {
                this.AddEdge(edge.A, edge.B);
                this.AddEdge(edge.B, edge.A);
            }
        }

        public Vertex GetVertexById(int ID)
        {
            return Vertices[ID];
        }
    }
}