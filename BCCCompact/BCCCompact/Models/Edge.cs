using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class Edge
    {
        public int u;
        public int v;
        public Edge(int u, int v)
        {
            this.u = u;
            this.v = v;
        }
    }
}