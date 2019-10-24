using BCCCompact.Models;
using BCCCompact.Models.Elemans.Star;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.BCC_Compact
{
    public class DataConverter
    {
        private readonly Graph graph;
        private readonly BCCGraph bccGraph;
        private readonly Dictionary<Guid, int> guidToVertex = new Dictionary<Guid, int>();
        public DataConverter(Graph graph)
        {
            this.graph = graph;
        }

        public BCCGraph GetBCCGraph()
        {
            int v = graph.Nodes.Count;

            foreach (var node in graph.Nodes)
            {
                AddVertex(node.NodeId);
            }
            foreach (var edge in edges)
            {
                var source = edge.FromNode;
                var target = edge.ToNode;
                guidToVertex[target].AddAdjacent(guidToVertex[source]);
                guidToVertex[source].AddAdjacent(guidToVertex[target]);
            }
        

        

            return new BCCGraph(graph);
        }

        public void AddVertex(Guid guid)
        {
            int lastID = Vertices.Count;
            var vertex = new Vertex(lastID);
            Vertices.Add(vertex);
            guidToVertex[guid] = vertex;
        }

        public Graph GetResultGraph()
        {
            return graph;
        }
    }
}