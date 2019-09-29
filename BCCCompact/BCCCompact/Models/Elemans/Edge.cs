using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class Edge
    {
        public int u { get; set; }
        public int v { get; set; }
        public Edge(int u, int v)
        {
            this.u = u;
            this.v = v;
        }
    }
}