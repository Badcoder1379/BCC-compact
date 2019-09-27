using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class Component
    {
        private HashSet<Vertex> vertices { get; set; } = new HashSet<Vertex>();
        public int numberOfVertices { get; set; }
        public Node lasrgestNode { get; set; }

        public void addVertex(Vertex vertex)
        {
            vertices.Add(vertex);
            numberOfVertices = vertices.Count;
        }

        public HashSet<Vertex> GetVertices()
        {
            return vertices;
        }
    }
}