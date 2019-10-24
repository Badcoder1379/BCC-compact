using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class Cluster
    {
        public HashSet<BCCVertex> Vertices = new HashSet<BCCVertex>();
        /// <summary>
        /// in each cluster some vertices are boundry and connect his cluster to some other clusters
        /// "adjacenty" is a dictionary that show adjacents of each boundry vertex
        /// </summary>
        public Dictionary<BCCVertex, HashSet<Cluster>> Adjacenty = new Dictionary<BCCVertex, HashSet<Cluster>>();
        public Dictionary<BCCVertex, double> AnglesOfInnerVertices = new Dictionary<BCCVertex, double>();
        public HashSet<Cluster> Children = new HashSet<Cluster>();
        public Cluster Parent;
        public double XCenter;
        public double YCenter;
        public double InternallRadius;
        public double ExternallRadius;
        public double EdgeToParentLenght;
        public double AngleShareFromParentCenter;
        public BCCVertex VertexConnectorToParent;
        public double AngleToConnectToParent;
        public double FreeAngleAround;


        public void AddAdjacenty(BCCVertex vertex, Cluster cluster)
        {
            if (!Adjacenty.ContainsKey(vertex))
            {
                Adjacenty[vertex] = new HashSet<Cluster>();
            }

            Adjacenty[vertex].Add(cluster);
        }

        public void PickVertexByAngle(BCCVertex vertex, double angle)
        {
            if (angle > 2 * Math.PI)
            {
                angle -= 2 * Math.PI;
            }
            AnglesOfInnerVertices[vertex] = angle;
            vertex.angleInCluster = angle;
        }
    }
}
