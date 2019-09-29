using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class Graph
    {
        public int V;
        public Vertex[] vertices;
        public Vertex leftestVertex;
        public Vertex rightestVertex;
        public Vertex topestVertex;
        public Vertex downestVertext;
        HashSet<Edge> edges;

        public static int count = 0, time = 0;

        public Graph(int v, HashSet<Edge> edges)
        {
            V = v;
            vertices = new Vertex[V];
            for (int i = 0; i < V; i++)
            {
                vertices[i] = new Vertex(i);
            }
            this.edges = edges;
            foreach (Edge edge in edges)
            {
                this.addEdge(edge.u, edge.v);
                this.addEdge(edge.v, edge.u);
            }
        }

        public void addEdge(int v, int w)
        {
            vertices[v].addAdjacent(vertices[w]);
        }

        public CompactResult getResult()
        {
            HashSet<Edge> edges = new HashSet<Edge>();
            foreach (Vertex vertex in vertices)
            {
                foreach (Vertex adjacent in vertex.adjacents)
                {
                    if (vertex.Id > adjacent.Id)
                    {
                        edges.Add(new Edge(vertex.Id, adjacent.Id));
                    }
                }
            }

            Location[] locations = new Location[V];
            int i = 0;
            foreach (Vertex vertex in vertices)
            {
                locations[i] = new Location(vertex.X, vertex.Y);
                i++;
            }
            return new CompactResult(edges, locations);
        }

        public static Graph getRandomGraph(int V, int E)
        {
            HashSet<Edge> edges = new HashSet<Edge>();
            Random random = new Random();
            while (E > 0)
            {
                int n1 = random.Next() % V;
                int n2 = random.Next() % V;
                if (n1 != n2)
                {
                    Edge edge = new Edge(n1, n2);
                    edges.Add(edge);
                    E--;
                }
            }
            return new Graph(V, edges);
        }

    }
}