using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class Vertex : IEquatable<Vertex>, IComparable<Vertex>
    {
        public int Id { get; set; }
        public HashSet<Vertex> adjacents = new HashSet<Vertex>();
        public double X;
        public double Y;
        public int type;
        public double angleInNode;
        public Node node;

        public Vertex(int Id)
        {
            this.Id = Id;
        }

        public void addAdjacent(Vertex vertex)
        {
            adjacents.Add(vertex);
        }

        public void setLocation(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public bool Equals(Vertex other)
        {
            if (other.Id == Id)
            {
                return true;
            }
            else
            {
                return false;
            }
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