using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    class NodeTreeMaker
    {
        private Node FatherNode;
        private Stack<Vertex> StackOfVertices = new Stack<Vertex>();
        private HashSet<Vertex> VisitedVertices = new HashSet<Vertex>();
        private HashSet<Node> VisitedNodes = new HashSet<Node>();


        public void Process(Component component)
        {
            var father = component.LargestNode;
            this.FatherNode = father;
            var randomVertex = FatherNode.Vertices.ToList().First();
            VisitedNodes.Add(randomVertex.node);
            VisitedVertices.Add(randomVertex);
            StackOfVertices.Push(randomVertex);
            while (StackOfVertices.Count > 0)
            {
                IterateOnVertices();
            }
        }

        public void IterateOnVertices()
        {
            var current = StackOfVertices.Pop();
            foreach (var adjacent in current.adjacents)
            {
                if (!VisitedVertices.Contains(adjacent))
                {
                    VisitedVertices.Add(adjacent);
                    var node1 = current.node;
                    var node2 = adjacent.node;
                    if (node1 != node2 && !VisitedNodes.Contains(node2))
                    {
                        node1.AddAdjacenty(current, node2);
                        node2.AddAdjacenty(adjacent, node1);
                        node1.Children.Add(node2);
                        node2.Parent = node1;
                        node2.VertexConnectorToParent = adjacent;
                        VisitedNodes.Add(node2);
                    }
                    StackOfVertices.Push(adjacent);
                }
            }
        }
    }
}