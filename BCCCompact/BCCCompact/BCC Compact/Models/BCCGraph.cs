using BCCCompact.Models.Elemans.Star;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCCCompact.Models
{
    public class BCCGraph
    {
        public int V;
        public List<Vertex> Vertices = new List<Vertex>();
        private readonly Dictionary<Guid, Vertex> guidToVertex = new Dictionary<Guid, Vertex>();
        private readonly List<Node> nodes;

        public BCCGraph(Graph graph)
        {
            this.nodes = graph.Nodes;
            var edges = graph.Edges;
            foreach (var node in nodes)
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
        }

        public BCCGraph(int v)
        {
            this.V = v;
            for (int i = 0; i < V; i++)
            {
                Vertices.Add(new Vertex(i));
            }
        }

        public void AddVertex(Guid guid)
        {
            int lastID = Vertices.Count;
            var vertex = new Vertex(lastID);
            Vertices.Add(vertex);
            guidToVertex[guid] = vertex;
        }

        public void ConstructResults()
        {
            foreach (var node in nodes)
            {
                var guid = node.NodeId;
                Vertex vertex = guidToVertex[guid];
                node.X = vertex.X;
                node.Y = vertex.Y;
            }
        }

        public void AddEdge(int v, int w)
        {
            Vertices[v].AddAdjacent(Vertices[w]);
        }


        public BCCGraph(int v, List<BCCEdge> edges)
        {
            V = v;
            for (int i = 0; i < V; i++)
            {
                Vertices.Add(new Vertex(i));
            }
            foreach (var edge in edges)
            {
                this.AddEdge(edge.A, edge.B);
                this.AddEdge(edge.B, edge.A);
            }
        }
    }
}