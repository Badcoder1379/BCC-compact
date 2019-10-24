using System;
using System.Collections.Generic;

namespace BCCCompact.Models.Elemans.Star
{
    public class Graph
    {
        public List<Node> Nodes { get; set; }

        public List<Edge> Edges { get; set; }

        public static implicit operator Graph(BCCGraph v)
        {
            throw new NotImplementedException();
        }
    }

    public class Node
    {
        public Guid NodeId { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Edge
    {
        public Guid FromNode { get; set; }

        public Guid ToNode { get; set; }
    }
}
