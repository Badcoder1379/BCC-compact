﻿using System;
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
                this.addEdge(edge.u, edge.v);
                this.addEdge(edge.v, edge.u);
            }
        }

        public void addEdge(int v, int w)
        {
            Vertices[v].addAdjacent(Vertices[w]);
        }

        public CompactResult getResult()
        {
            HashSet<Edge> edges = new HashSet<Edge>();
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

            Location[] locations = new Location[V];
            int i = 0;
            foreach (Vertex vertex in Vertices)
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