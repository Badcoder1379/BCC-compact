using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models.Elemans.Star
{
    public class StarGraph
    {
        public List<StarNode> Nodes { get; set; }

        public List<StarEdge> Edges { get; set; }
    }

    public class StarNode
    {
        public Guid NodeId { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
    }

    public class StarEdge
    {
        public Guid FromNode { get; set; }

        public Guid ToNode { get; set; }
    }
}