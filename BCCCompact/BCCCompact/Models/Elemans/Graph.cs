using System;
using System.Collections.Generic;
using System.IO;

namespace BCCCompact.Models
{
    public class Graph
    {
        public int V;
        public Vertex[] Vertices;

        public Graph(int v, HashSet<Edge> edges)
        {
            V = v;
            Vertices = new Vertex[V];
            for (int i = 0; i < V; i++)
            {
                Vertices[i] = new Vertex(i);
            }
            foreach (Edge edge in edges)
            {
                this.AddEdge(edge.A, edge.B);
                this.AddEdge(edge.B, edge.A);
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
                foreach (Vertex adjacent in vertex.adjacents)
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

    }
}