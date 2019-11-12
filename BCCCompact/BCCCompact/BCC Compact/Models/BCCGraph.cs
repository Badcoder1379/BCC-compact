using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class BccGraph
    {
        public int VerticesCount { get; set; }
        public List<BccVertex> Vertices { get; set; }

        private void AddEdge(int v, int w)
        {
            Vertices[v].AddAdjacent(Vertices[w]);
        }

        public BccGraph(int v, List<BccEdge> edges)
        {
            Vertices = new List<BccVertex>();

            VerticesCount = v;

            for (int i = 0; i < VerticesCount; i++)
            {
                Vertices.Add(new BccVertex(i));
            }
            
            foreach (var edge in edges)
            {
                this.AddEdge(edge.Source, edge.Target);
                this.AddEdge(edge.Target, edge.Source);
            }
        }

        public BccVertex GetVertexById(int ID)
        {
            return Vertices[ID];
        }
    }
}
