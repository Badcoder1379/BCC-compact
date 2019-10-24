using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    class ClasserTreeMaker
    {
        private readonly Classer FatherClasser;
        private readonly Stack<Vertex> stackOfVertices = new Stack<Vertex>();
        private readonly HashSet<Vertex> visitedVertices = new HashSet<Vertex>();
        private readonly HashSet<Classer> visitedClassers = new HashSet<Classer>();

        public ClasserTreeMaker(Component component)
        {
            FatherClasser = component.LargestClasser;
        }



        /// <summary>
        /// this method gets a component and returns a tree of classers that each classer has a lot of vertices
        /// </summary>
        /// <param name="component"></param>
        public void Process()
        {
            var randomVertex = FatherClasser.Vertices.ToList().First();
            visitedClassers.Add(randomVertex.Classer);
            visitedVertices.Add(randomVertex);
            stackOfVertices.Push(randomVertex);
            while (stackOfVertices.Count > 0)
            {
                IterateOnVertices();
            }
        }

        /// <summary>
        /// this method move on graph vertices and called a lot
        /// </summary>
        public void IterateOnVertices()
        {
            var current = stackOfVertices.Pop();
            foreach (var adjacent in current.Adjacents)
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