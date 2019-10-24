using BCCCompact.BCC_Compact;
using BCCCompact.BCC_Compact.logic;
using BCCCompact.Models.Compacts;
using BCCCompact.Models.Elemans.Star;
using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class PackageBCC
    {

        private readonly Graph graph;
        private BCCGraph bccGraph;
        private readonly Dictionary<Guid, int> guidToInt = new Dictionary<Guid, int>();

        public PackageBCC(Graph graph)
        {
            this.graph = graph;
        }

        
        public void Process()
        {
            ConvertData();
            var business = new Business(bccGraph);
            business.Process();
            ConstrucResult();
        }

        private void ConvertData()
        {
            int v = graph.Nodes.Count;
            var edges = new List<BCCEdge>();
            foreach (var node in graph.Nodes)
            {
                AddVertex(node.NodeId);
            }
            foreach (var edge in graph.Edges)
            {
                var source = edge.FromNode;
                var target = edge.ToNode;
                edges.Add(new BCCEdge(guidToInt[source], guidToInt[target]));
            }
            bccGraph = new BCCGraph(v, edges);
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
                Vertex vertex = bccGraph.GetVertexById(guidToInt[guid]);
                node.X = vertex.X;
                node.Y = vertex.Y;
            }
        }

    }
}
