using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    public class Node
    {
        public HashSet<Vertex> vertices = new HashSet<Vertex>();
        public Dictionary<Node, Vertex> adjacents = new Dictionary<Node, Vertex>();
        public Dictionary<Vertex, HashSet<Node>> adjacenty_vertex_nodes = new Dictionary<Vertex, HashSet<Node>>();
        public Dictionary<Vertex, double> innerVertices_angle = new Dictionary<Vertex, double>();
        public double NodeCount;
        public HashSet<Node> children = new HashSet<Node>();
        public Node parent;
        public Double XCenter;
        public Double YCenter;
        public double firstInternallRadius = 30;
        public Double internallRadius;
        public Double externallRadius;
        public double connectionLenght;
        public double parentAngleShare;
        public Vertex connectedToParent;
        public double angleToConnectToParent;
        public double bestAngleForRotate;

        public Node()
        {
        }

        public void addVertices(Vertex Vertex)
        {
            vertices.Add(Vertex);
        }

        public int VerticesCount()
        {
            return vertices.Count;
        }

        public void AddAdjacenty(Vertex vertex, Node node)
        {
            if (!adjacenty_vertex_nodes.Keys.Contains(vertex))
            {
                adjacenty_vertex_nodes[vertex] = new HashSet<Node>();
            }
            adjacenty_vertex_nodes[vertex].Add(node);
        }

        public void AddChildren(Node child)
        {
            children.Add(child);
        }

        public void SetParent(Node parent)
        {
            this.parent = parent;
        }

        public HashSet<Vertex> GetVertices()
        {
            return vertices;
        }

        public void PickVertexByAngle(Vertex vertex, double angle)
        {
            innerVertices_angle[vertex] = angle;
            vertex.angleInNode = angle;
        }
    }
}