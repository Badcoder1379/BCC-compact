using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    class ClusterMaker
    {
        private readonly Component component;
        private readonly HashSet<Cluster> clusters = new HashSet<Cluster>();
        private Cluster largestCluster;
        private Dictionary<BCCVertex, int> vertexToClusterId = new Dictionary<BCCVertex, int>();

        public ClusterMaker(Component component)
        {
            this.component = component;
        }

        /// <summary>
        /// this method gets a component and returns all clusters and finds largest cluster 
        /// </summary>
        /// <param name="component"></param>
        public void Process()
        {
            ClusterLabelTagging();
            ConstructClusters();
            component.LargestCluster = largestCluster;
        }

        /// <summary>
        /// this method will make clusters from the data that has cluster id of each vertex 
        /// </summary>
        private void ConstructClusters()
        {
            largestCluster = new Cluster();
            var clusterIdToCluster = new Dictionary<int, Cluster>();

            foreach (var vertex in component.Vertices)
            {
                int clusterId = vertexToClusterId[vertex];

                if (!clusterIdToCluster.ContainsKey(clusterId))
                {
                    clusterIdToCluster[clusterId] = new Cluster();
                    clusters.Add(clusterIdToCluster[clusterId]);
                }

                var cluster = clusterIdToCluster[clusterId];
                cluster.Vertices.Add(vertex);
                vertex.Cluster = cluster;

                if (cluster.Vertices.Count > largestCluster.Vertices.Count)
                {
                    largestCluster = cluster;
                }
            }
        }

        /// <summary>
        /// this method will tag a id number on each vertex
        /// </summary>
        private void ClusterLabelTagging()
        {
            var ARandomVertex = component.Vertices.ToList().First();
            vertexToClusterId = new BCCAlgorithm().NodingComponentFromThisVertex(component, ARandomVertex);
            ConstructClusters();
            var ARandomVertexFromLargestCluster = largestCluster.Vertices.ToList().First();
            vertexToClusterId = new BCCAlgorithm().NodingComponentFromThisVertex(component, ARandomVertexFromLargestCluster);
        }
    }
}
