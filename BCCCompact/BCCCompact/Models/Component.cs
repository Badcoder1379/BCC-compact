using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class Component
    {
        public HashSet<Vertex> Vertices { get; set; } = new HashSet<Vertex>();
        public int numberOfVertices { get; set; }
        public Node lasrgestNode { get; set; }

        public void addVertex(Vertex vertex)
        {
            Vertices.Add(vertex);
            numberOfVertices = Vertices.Count;
        }

        public HashSet<Vertex> GetVertices()
        {
            return Vertices;
        }
    }
}