using BCCCompact.BCC_Compact.logic;
using BCCCompact.Models.Elemans.Star;
using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class BccPackage
    {
        private readonly Graph graph;
        private BccGraph bccGraph;
        private readonly Dictionary<Guid, int> guidToInt = new Dictionary<Guid, int>();

        public BccPackage(Graph graph)
        {
            this.graph = graph;
        }

        public void Process()
        {
            ConvertData();
            var business = new BccBusiness(bccGraph);
            business.Process();
            ConstrucResult();
        }

        private void ConvertData()
        {
            int v = graph.Nodes.Count;
            var edges = new List<BccEdge>();

            foreach (var node in graph.Nodes)
            {
                AddVertex(node.NodeId);
            }

            foreach (var edge in graph.Edges)
            {
                var source = edge.FromNode;
                var target = edge.ToNode;
                edges.Add(new BccEdge(guidToInt[source], guidToInt[target]));
            }

            bccGraph = new BccGraph(v, edges);
        }

        private void AddVertex(Guid guid)
        {
            int newId = guidToInt.Count;
            guidToInt[guid] = newId;
        }

        private void ConstrucResult()
        {
            foreach (var node in graph.Nodes)
            {
                var guid = node.NodeId;
                BccVertex vertex = bccGraph.GetVertexById(guidToInt[guid]);
                node.X = vertex.X;
                node.Y = vertex.Y;
            }
        }
    }
}
