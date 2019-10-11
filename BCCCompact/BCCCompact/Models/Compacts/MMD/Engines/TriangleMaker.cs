using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models.Compacts.MMD.Engines
{
    public class TriangleMaker
    {
        public void Process(Component component)
        {
            var nodeMaker = new NodeMaker();
            var nodeTreeMaker = new NodeTreeMaker();
            nodeMaker.Process(component);
            nodeTreeMaker.Process(component);
            var nodes = nodeMaker.Nodes;
            var nodeToTriangle = new Dictionary<Node, Triangle>();
            foreach(var node in nodes)
            {
                var triangle = new Triangle();
                triangle.Vertices = node.Vertices;
                nodeToTriangle[node] = triangle;
                nodeToTriangle[node].VertexConnectorToParent = node.VertexConnectorToParent;
            }
            foreach(var node in nodes)
            {
                foreach(var child in node.Children)
                {
                    nodeToTriangle[node].Children.Add(nodeToTriangle[child]);
                    nodeToTriangle[child].Parent = nodeToTriangle[node];
                }
            }
            component.LargestTriangle = nodeToTriangle[component.LargestNode];
            foreach(Node node in nodes)
            {
                var adjacenty = node.AdjacentNodesWithConnectiongThisVertex;
                foreach(Vertex vertex in adjacenty.Keys)
                {
                    foreach(Node child in adjacenty[vertex])
                    {
                        nodeToTriangle[node].AddAdjacenty(vertex, nodeToTriangle[child]);
                    }
                }
            }
        }
    }
}