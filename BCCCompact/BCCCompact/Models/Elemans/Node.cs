using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class Node
    {
        public HashSet<Vertex> Vertices = new HashSet<Vertex>();
        public Dictionary<Vertex, HashSet<Node>> AdjacentNodesWithConnectiongThisVertex = new Dictionary<Vertex, HashSet<Node>>();
        public Dictionary<Vertex, double> AnglesOfInnerVertices = new Dictionary<Vertex, double>();
        public HashSet<Node> Children = new HashSet<Node>();
        public Node Parent;
        public double XCenter;
        public double YCenter;
        public double internallRadius;
        public double externallRadius;
        public double EdgeToParentLenght;
        public double AngleShareFromParentCenter;
        public Vertex VertexConnectorToParent;
        public double AngleToConnectToParent;
        public double FreeAngleAround;


        public void AddAdjacenty(Vertex vertex, Node node)
        {
            if (!AdjacentNodesWithConnectiongThisVertex.ContainsKey(vertex))
            {
                AdjacentNodesWithConnectiongThisVertex[vertex] = new HashSet<Node>();
            }

            AdjacentNodesWithConnectiongThisVertex[vertex].Add(node);
        }

        public void PickVertexByAngle(Vertex vertex, double angle)
        {
            AnglesOfInnerVertices[vertex] = angle;
            vertex.angleInNode = angle;
        }
    }
}