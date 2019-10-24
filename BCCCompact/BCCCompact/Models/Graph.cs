using BCCCompact.Models.Elemans.Star;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCCCompact.Models
{
    public class Graph
    {
        public int V;
        public List<Vertex> Vertices = new List<Vertex>();
        private readonly Dictionary<Guid, Vertex> guidToVertex = new Dictionary<Guid, Vertex>();
        private readonly List<StarNode> nodes;
        public Graph(int v, HashSet<Edge> edges)
        {
            V = v;
            for (int i = 0; i < V; i++)
            {
                Vertices.Add(new Vertex(i));
            }
            foreach (Edge edge in edges)
            {
                this.AddEdge(edge.A, edge.B);
                this.AddEdge(edge.B, edge.A);
            }
        }

        public Graph(List<StarEdge> edges, List<StarNode> nodes)
        {
            this.nodes = nodes;
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

        public void AddVertex(Guid guid)
        {
            int lastID = Vertices.Count;
            var vertex = new Vertex(lastID);
            Vertices.Add(vertex);
            guidToVertex[guid] = vertex;

        }

        public void ConstructResults()
        {
            //removable
            if (nodes == null) return;
            //removable


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


        public CompactResult GetResult()
        {
            var edges = new HashSet<Edge>();

            foreach (Vertex vertex in Vertices)
            {
                foreach (Vertex adjacent in vertex.Adjacents)
                {
                    if (vertex.Id > adjacent.Id)
                    {
                        edges.Add(new Edge(vertex.Id, adjacent.Id));
                    }
                }
            }

            var locations = new Location[V];
            var i = 0;

            foreach (Vertex vertex in Vertices)
            {
                locations[i] = new Location(vertex.X, vertex.Y);
                i++;
            }

            return new CompactResult(edges, locations);
        }

        public static Graph GetRandomGraph(int V, int E, string fileName)
        {
            string path = Importer.SRCAddress + fileName;
            StreamWriter sw;
            if (!File.Exists(path))
            {
                sw = new StreamWriter(File.Create(path));
            }
            else
            {
                sw = new StreamWriter(path);

            }


            var edges = new HashSet<Edge>();
            var random = new Random();
            while (E > 0)
            {
                int n1 = random.Next() % V;
                int n2 = random.Next() % V;
                if (n1 != n2)
                {
                    var edge = new Edge(n1, n2);
                    edges.Add(edge);
                    E--;
                    sw.WriteLine(n1 + "," + n2);
                }
            }
            sw.Flush();
            sw.Close();
            return new Graph(V, edges);
        }

        public static Graph GetRandomTree(int v, string fileName)
        {
            string path = Importer.SRCAddress + fileName;
            StreamWriter sw;
            if (!File.Exists(path))
            {
                sw = new StreamWriter(File.Create(path));
            }
            else
            {
                sw = new StreamWriter(path);

            }


            var edges = new HashSet<Edge>();
            var random = new Random();
            int i = 1;
            while (i < v)
            {
                int rnd = random.Next() % i;
                var edge = new Edge(rnd, i);
                edges.Add(edge);
                sw.WriteLine(i + "," + rnd);
                i++;
            }
            sw.Flush();
            sw.Close();
            return new Graph(v, edges);
        }

    }
}