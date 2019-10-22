using System;
using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class Classer
    {
        public HashSet<Vertex> Vertices = new HashSet<Vertex>();
        public Dictionary<Vertex, HashSet<Classer>> AdjacentClassersWithConnectiongThisVertex = new Dictionary<Vertex, HashSet<Classer>>();
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
            if (!AdjacentClassersWithConnectiongThisVertex.ContainsKey(vertex))
            {
                AdjacentClassersWithConnectiongThisVertex[vertex] = new HashSet<Classer>();
            }

            AdjacentClassersWithConnectiongThisVertex[vertex].Add(classer);
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