using BCCCompact.Models.Compacts.MMD;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class Component
    {
        public HashSet<Vertex> Vertices = new HashSet<Vertex>();
        public Node LargestNode;
        public Triangle LargestTriangle;
    }
}