using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class Vertex : IEquatable<Vertex>, IComparable<Vertex>
    {
        public int Id;
        public HashSet<Vertex> Adjacents = new HashSet<Vertex>();
        public double X;
        public double Y;
        public double angleInClasser;
        public Classer Classer;

        public Vertex(int Id)
        {
            this.Id = Id;
        }

        public void AddAdjacent(Vertex vertex)
        {
            Adjacents.Add(vertex);
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
            double dif = angleInClasser - other.angleInClasser;
            return (int)(100000 * dif);
        }

    }
}