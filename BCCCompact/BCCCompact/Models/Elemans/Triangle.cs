using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models.Compacts.MMD
{
    public class Triangle
    {
        public Dictionary<Vertex, HashSet<Triangle>> AdjacentNodesWithConnectiongThisVertex = new Dictionary<Vertex, HashSet<Triangle>>();
        public HashSet<Vertex> Vertices = new HashSet<Vertex>();
        public HashSet<Triangle> Children = new HashSet<Triangle>();
        public Dictionary<Vertex, Tuple<double, double>> VertexToLocation = new Dictionary<Vertex, Tuple<double, double>>();
        public Triangle Parent;
        public double XCenter;
        public double YCenter;
        public LinkedList<LinkedList<Vertex>> Orbitals;
        public double CenteralAngle;
        public double BeginAngle;
        public double FinishAngle;
        public double Size;
        public Vertex VertexConnectorToParent;
        public double EdgeToParentLenght;
        public double AngleToConnectToParent;

        public void AddAdjacenty(Vertex vertex, Triangle triangle)
        {
            if (!AdjacentNodesWithConnectiongThisVertex.ContainsKey(vertex))
            {
                AdjacentNodesWithConnectiongThisVertex[vertex] = new HashSet<Triangle>();
            }

            AdjacentNodesWithConnectiongThisVertex[vertex].Add(triangle);
        }

        public void SetAngles(double beginAngle, double finishAngle)
        {
            this.BeginAngle = beginAngle;
            this.FinishAngle = finishAngle;
            this.CenteralAngle = finishAngle - beginAngle;
            this.AngleToConnectToParent = (finishAngle+finishAngle)/ 2;
        }

        public void AddVertexLocation(Vertex vertex, double orbitalCount,double angle)
        {
            VertexToLocation[vertex] = new Tuple<double, double>(orbitalCount, angle);
        }

    }
}