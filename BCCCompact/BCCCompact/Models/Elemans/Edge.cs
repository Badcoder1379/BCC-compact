using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class Edge
    {
        public int FirstVertex { get; set; }
        public int SecondVertex { get; set; }
        public Edge(int u, int v)
        {
            this.FirstVertex = u;
            this.SecondVertex = v;
        }
    }
}