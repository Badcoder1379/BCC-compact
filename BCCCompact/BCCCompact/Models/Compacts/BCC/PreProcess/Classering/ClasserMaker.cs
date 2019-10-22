using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    class ClasserMaker
    {
        public Component Component { get; set; }
        public HashSet<Classer> Classers { get; set; }
        public Classer LargestClasser { get; set; }
        private Dictionary<Vertex, int> vertexToClasserId;


        private void SetComponent(Component component)
        {
            this.Component = component;
            Classers = new HashSet<Classer>();
            vertexToClasserId = new Dictionary<Vertex, int>();
        }

        public void Process(Component component)
        {
            SetComponent(component);
            ClasserLabelTagging();
            ConstructClassers();
            component.LargestClasser = LargestClasser;
        }

        private void ConstructClassers()
        {
            LargestClasser = new Classer();
            var classerId_classer = new Dictionary<int, Classer>();
            foreach (var vertex in Component.Vertices)
            {
                int classerId = vertexToClasserId[vertex];
                if (!classerId_classer.ContainsKey(classerId))
                {
                    classerId_classer[classerId] = new Classer();
                    Classers.Add(classerId_classer[classerId]);
                }
                var classer = classerId_classer[classerId];
                classer.Vertices.Add(vertex);
                vertex.SetClasser(classer);
                if (classer.Vertices.Count > LargestClasser.Vertices.Count)
                {
                    LargestClasser = classer;
                }
            }
        }

        private void ClasserLabelTagging()
        {
            var bbc = new BccAlgrtm();
            var ARandomVertex = Component.Vertices.ToList().First();
            vertexToClasserId = bbc.NodingComponentFromThisVertex(Component, ARandomVertex);
            ConstructClassers();
            var ARandomVertexFromLargestClasser = LargestClasser.Vertices.ToList().First();
            vertexToClasserId = bbc.NodingComponentFromThisVertex(Component, ARandomVertexFromLargestClasser);
        }
    }
}
