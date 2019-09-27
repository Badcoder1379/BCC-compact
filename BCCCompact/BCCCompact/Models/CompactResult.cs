using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class CompactResult
    {
        public HashSet<Edge> edges { get; set; }
        public Location[] locations { get; set; }

        public CompactResult(HashSet<Edge> edges, Location[] locations)
        {
            this.edges = edges;
            this.locations = locations;
        }
    }
}