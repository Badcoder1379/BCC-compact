using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    class ClusterTreeMaker
    {
        private readonly Cluster FatherCluster;
        private readonly Stack<BccVertex> stackOfVertices = new Stack<BccVertex>();
        private readonly HashSet<BccVertex> visitedVertices = new HashSet<BccVertex>();
        private readonly HashSet<Cluster> visitedClusters = new HashSet<Cluster>();

        public ClusterTreeMaker(Component component)
        {
            FatherCluster = component.LargestCluster;
        }

        /// <summary>
        /// this method gets a component and returns a tree of clusters that each cluster has a lot of vertices
        /// </summary>
        /// <param name="component"></param>
        public void Process()
        {
            var randomVertex = FatherCluster.Vertices.First();
            visitedClusters.Add(randomVertex.Cluster);
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

            foreach (var adjacent in current.Adjacents.Where(x => !visitedVertices.Contains(x)))
            {
                visitedVertices.Add(adjacent);
                var cluster1 = current.Cluster;
                var cluster2 = adjacent.Cluster;

                if (cluster1 != cluster2 && !visitedClusters.Contains(cluster2))
                {
                    cluster1.AddAdjacenty(current, cluster2);
                    cluster2.AddAdjacenty(adjacent, cluster1);
                    cluster1.Children.Add(cluster2);
                    cluster2.Parent = cluster1;
                    cluster2.VertexConnectorToParent = adjacent;
                    visitedClusters.Add(cluster2);
                }

                stackOfVertices.Push(adjacent);
            }
        }
    }
}
