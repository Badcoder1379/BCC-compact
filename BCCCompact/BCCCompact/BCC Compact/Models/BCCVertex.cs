using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class BccVertex : IEquatable<BccVertex>, IComparable<BccVertex>
    {
        public int Id { get; set; }
        public HashSet<BccVertex> Adjacents { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double AngleInCluster { get; set; }
        public Cluster Cluster { get; set; }

        public BccVertex(int Id)
        {
            this.Id = Id;
            Adjacents = new HashSet<BccVertex>();
        }

        public void AddAdjacent(BccVertex vertex)
        {
            Adjacents.Add(vertex);
        }

        public void SetLocation(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public bool Equals(BccVertex other)
        {
            return other.Id == Id;
        }

        public int CompareTo(BccVertex other)
        {
            double dif = AngleInCluster - other.AngleInCluster;
            return (int)(100000 * dif);
        }
    }
}
