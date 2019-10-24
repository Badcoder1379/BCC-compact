using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class BCCVertex : IEquatable<BCCVertex>, IComparable<BCCVertex>
    {
        public int Id;
        public HashSet<BCCVertex> Adjacents = new HashSet<BCCVertex>();
        public double X;
        public double Y;
        public double angleInClasser;
        public Classer Classer;

        public BCCVertex(int Id)
        {
            this.Id = Id;
        }

        public void AddAdjacent(BCCVertex vertex)
        {
            Adjacents.Add(vertex);
        }

        public void SetLocation(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public bool Equals(BCCVertex other)
        {
            return other.Id == Id;
        }

        public int CompareTo(BCCVertex other)
        {
            double dif = angleInClasser - other.angleInClasser;
            return (int)(100000 * dif);
        }

    }
}
