using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class Classer
    {
        public HashSet<Vertex> Vertices = new HashSet<Vertex>();
        /// <summary>
        /// in each classer some vertices are boundry and connect his classer to some other classers
        /// "adjacenty" is a dictionary that show adjacents of each boundry vertex
        /// </summary>
        public Dictionary<Vertex, HashSet<Classer>> Adjacenty = new Dictionary<Vertex, HashSet<Classer>>();
        public Dictionary<Vertex, double> AnglesOfInnerVertices = new Dictionary<Vertex, double>();
        public HashSet<Classer> Children = new HashSet<Classer>();
        public Classer Parent;
        public double XCenter;
        public double YCenter;
        public double InternallRadius;
        public double ExternallRadius;
        public double EdgeToParentLenght;
        public double AngleShareFromParentCenter;
        public Vertex VertexConnectorToParent;
        public double AngleToConnectToParent;
        public double FreeAngleAround;


        public void AddAdjacenty(Vertex vertex, Classer classer)
        {
            if (!Adjacenty.ContainsKey(vertex))
            {
                Adjacenty[vertex] = new HashSet<Classer>();
            }

            Adjacenty[vertex].Add(classer);
        }

        public void PickVertexByAngle(Vertex vertex, double angle)
        {
            if (angle > 2 * Math.PI)
            {
                angle -= 2 * Math.PI;
            }
            AnglesOfInnerVertices[vertex] = angle;
            vertex.angleInClasser = angle;
        }
    }
}