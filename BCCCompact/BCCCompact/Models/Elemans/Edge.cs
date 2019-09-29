using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class Edge
    {
        public int A { get; set; }
        public int B { get; set; }
        public Edge(int a, int b)
        {
            this.A = a;
            this.B = b;
        }
    }
}