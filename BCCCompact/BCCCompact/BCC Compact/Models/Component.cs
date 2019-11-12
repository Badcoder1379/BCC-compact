using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class Component
    {
        public HashSet<BccVertex> Vertices { get; set; }
        public Cluster LargestCluster { get; set; }

        public Component()
        {
            Vertices = new HashSet<BccVertex>();
        }
    }
}
