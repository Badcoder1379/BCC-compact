using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    class NodeMaker
    {
        Component component { get; set; }
        HashSet<Node> nodes { get; set; }
        Node largestNode { get; set; }
        Dictionary<Vertex, int> vertex_low;
        Dictionary<Vertex, int> vertex_disc;
        int time = 0;
        static readonly int DFS_deep = 500;



        private void SetComponent(Component component)
        {
            this.component = component;
            vertex_low = new Dictionary<Vertex, int>();
            vertex_disc = new Dictionary<Vertex, int>();
            nodes = new HashSet<Node>();
        }

        public HashSet<Node> GetNodes()
        {
            return nodes;
        }

        public Node GetLargestNode()
        {
            return largestNode;
        }

        public void Process(Component component)
        {
            SetComponent(component);
            NodeLabelTagging();
            ConstructNodes();
            component.lasrgestNode = largestNode;
        }

        private void ConstructNodes()
        {
            largestNode = new Node();
            Dictionary<int, Node> nodeId_node = new Dictionary<int, Node>();
            foreach (Vertex vertex in component.GetVertices())
            {
                int nodeId = vertex_low[vertex];
                if (!nodeId_node.Keys.Contains(nodeId))
                {
                    nodeId_node[nodeId] = new Node();
                    nodes.Add(nodeId_node[nodeId]);
                }
                Node node = nodeId_node[nodeId];
                node.addVertices(vertex);
                vertex.SetNode(node);
                if (node.VerticesCount() > largestNode.VerticesCount())
                {
                    largestNode = node;
                }
            }
        }

        private void NodeLabelTagging()
        {
            BccAlgrtm mmd = new BccAlgrtm();
            vertex_low = mmd.Process(component);
        }
    }
}