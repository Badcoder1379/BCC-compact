using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class Component
    {
        public HashSet<Vertex> Vertices = new HashSet<Vertex>();
        public Node LasrgestNode;
    }
}