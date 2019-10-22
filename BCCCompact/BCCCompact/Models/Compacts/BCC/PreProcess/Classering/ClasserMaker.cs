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

        /// <summary>
        /// this method gets a component and returns all classers and finds largest classer 
        /// </summary>
        /// <param name="component"></param>
        public void Process(Component component)
        {
            SetComponent(component);
            ClasserLabelTagging();
            ConstructClassers();
            component.LargestClasser = LargestClasser;
        }

        /// <summary>
        /// this method will make classers from the data that has classer id of each vertex 
        /// </summary>
        private void ConstructClassers()
        {
            LargestClasser = new Classer();
            var classerIdToClasser = new Dictionary<int, Classer>();
            foreach (var vertex in Component.Vertices)
            {
                int classerId = vertexToClasserId[vertex];
                if (!classerIdToClasser.ContainsKey(classerId))
                {
                    classerIdToClasser[classerId] = new Classer();
                    Classers.Add(classerIdToClasser[classerId]);
                }
                var classer = classerIdToClasser[classerId];
                classer.Vertices.Add(vertex);
                vertex.SetClasser(classer);
                if (classer.Vertices.Count > LargestClasser.Vertices.Count)
                {
                    LargestClasser = classer;
                }
            }
        }

        /// <summary>
        /// this method will tag a id number on each vertex
        /// </summary>
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
