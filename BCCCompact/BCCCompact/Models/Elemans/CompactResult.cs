using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class CompactResult
    {
        public HashSet<Edge> Edges { get; set; }
        public Location[] Locations { get; set; }

        public CompactResult(HashSet<Edge> edges, Location[] locations)
        {
            this.Edges = edges;
            this.Locations = locations;
        }
    }
}