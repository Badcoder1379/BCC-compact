using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    class NodeTreeMaker
    {
        Node fatherNode;
        Stack<Vertex> verticesToUtil = new Stack<Vertex>();
        HashSet<Vertex> visitedVertices = new HashSet<Vertex>();
        HashSet<Node> visitedNodes = new HashSet<Node>();

        private void SetFatherNode(Node father)
        {
            this.fatherNode = father; ;
        }

        public void process(Component component)
        {
            Node father = component.lasrgestNode;
            SetFatherNode(father);
            Vertex randomVertex = fatherNode.GetVertices().ToList().First();
            visitedNodes.Add(randomVertex.node);
            visitedVertices.Add(randomVertex);
            verticesToUtil.Push(randomVertex);
            while (verticesToUtil.Count > 0)
            {
                Util();
            }
        }

        public void Util()
        {
            Vertex current = verticesToUtil.Pop();
            foreach (Vertex adjacent in current.adjacents)
            {
                if (!visitedVertices.Contains(adjacent))
                {
                    visitedVertices.Add(adjacent);
                    Node node1 = current.node;
                    Node node2 = adjacent.node;
                    if (node1 != node2 && !visitedNodes.Contains(node2))
                    {
                        node1.AddAdjacenty(current, node2);
                        node2.AddAdjacenty(adjacent, node1);
                        node1.AddChildren(node2);
                        node2.SetParent(node1);
                        node2.connectedToParent = adjacent;
                        visitedNodes.Add(node2);
                    }
                    verticesToUtil.Push(adjacent);
                }
            }
        }
    }
}