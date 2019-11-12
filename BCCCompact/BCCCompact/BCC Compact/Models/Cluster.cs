using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class Cluster
    {
        public HashSet<BccVertex> Vertices { get; set; }
        /// <summary>
        /// in each cluster some vertices are boundry and connect his cluster to some other clusters
        /// "adjacenty" is a dictionary that show adjacents of each boundry vertex
        /// </summary>
        public Dictionary<BccVertex, HashSet<Cluster>> Adjacenty { get; set; }
        public Dictionary<BccVertex, double> AnglesOfInnerVertices { get; set; }
        public HashSet<Cluster> Children { get; set; }
        public Cluster Parent { get; set; }
        public double XCenter { get; set; }
        public double YCenter { get; set; }
        public double InternallRadius { get; set; }
        public double ExternallRadius { get; set; }
        public double EdgeToParentLenght { get; set; }
        public double AngleShareFromParentCenter { get; set; }
        public BccVertex VertexConnectorToParent { get; set; }
        public double AngleToConnectToParent { get; set; }
        public double FreeAngleAround { get; set; }

        public Cluster()
        {
            Vertices = new HashSet<BccVertex>();
            Adjacenty = new Dictionary<BccVertex, HashSet<Cluster>>();
            AnglesOfInnerVertices = new Dictionary<BccVertex, double>();
            Children = new HashSet<Cluster>();
        }

        public void AddAdjacenty(BccVertex vertex, Cluster cluster)
        {
            if (!Adjacenty.ContainsKey(vertex))
            {
                Adjacenty[vertex] = new HashSet<Cluster>();
            }

            Adjacenty[vertex].Add(cluster);
        }

        public void PickVertexByAngle(BccVertex vertex, double angle)
        {
            if (angle > 2 * Math.PI)
            {
                angle -= 2 * Math.PI;
            }

            AnglesOfInnerVertices[vertex] = angle;
            vertex.AngleInCluster = angle;
        }
    }
}
