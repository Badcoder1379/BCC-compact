using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                this.addEdge(edge.A, edge.B);
                this.addEdge(edge.B, edge.A);
            }
        }

        public void addEdge(int v, int w)
        {
            Vertices[v].AddAdjacent(Vertices[w]);
        }

        public CompactResult getResult()
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

        public static Graph getRandomGraph(int V, int E)
        {
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
                }
            }
            return new Graph(V, edges);
        }

    }
}