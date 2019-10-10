using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class Vertex : IEquatable<Vertex>, IComparable<Vertex>
    {
        public int Id;
        public HashSet<Vertex> adjacents = new HashSet<Vertex>();
        public double X;
        public double Y;
        public string Name;
        public int Type;
        public double angleInNode;
        public Node node;

        public Vertex(int Id)
        {
            this.Id = Id;
        }

        public void AddAdjacent(Vertex vertex)
        {
            adjacents.Add(vertex);
        }

        public void SetLocation(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public bool Equals(Vertex other)
        {
            return other.Id == Id;
        }

        public int CompareTo(Vertex other)
        {
            double dif = angleInNode - other.angleInNode;
            return (int)(100000 * dif);
        }

        public void SetNode(Node node)
        {
            this.node = node;
        }
    }
}