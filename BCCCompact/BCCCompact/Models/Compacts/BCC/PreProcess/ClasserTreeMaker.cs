using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    class ClasserTreeMaker
    {
        private Classer FatherClasser;
        private Stack<Vertex> stackOfVertices;
        private HashSet<Vertex> visitedVertices;
        private HashSet<Classer> visitedClassers;


        public void Process(Component component)
        {
            stackOfVertices = new Stack<Vertex>();
            visitedVertices = new HashSet<Vertex>();
            visitedClassers = new HashSet<Classer>();
            var father = component.LargestClasser;
            this.FatherClasser = father;
            var randomVertex = FatherClasser.Vertices.ToList().First();
            visitedClassers.Add(randomVertex.Classer);
            visitedVertices.Add(randomVertex);
            stackOfVertices.Push(randomVertex);
            while (stackOfVertices.Count > 0)
            {
                IterateOnVertices();
            }
        }

        public void IterateOnVertices()
        {
            var current = stackOfVertices.Pop();
            foreach (var adjacent in current.adjacents)
            {
                if (!visitedVertices.Contains(adjacent))
                {
                    visitedVertices.Add(adjacent);
                    var classer1 = current.Classer;
                    var classer2 = adjacent.Classer;
                    if (classer1 != classer2 && !visitedClassers.Contains(classer2))
                    {
                        classer1.AddAdjacenty(current, classer2);
                        classer2.AddAdjacenty(adjacent, classer1);
                        classer1.Children.Add(classer2);
                        classer2.Parent = classer1;
                        classer2.VertexConnectorToParent = adjacent;
                        visitedClassers.Add(classer2);
                    }
                    stackOfVertices.Push(adjacent);
                }
            }
        }
    }
}