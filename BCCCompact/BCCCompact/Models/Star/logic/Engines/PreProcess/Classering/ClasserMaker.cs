using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models
{
    class ClasserMaker
    {
        private readonly Component component;
        private readonly HashSet<Classer> classers = new HashSet<Classer>();
        private Classer largestClasser;
        private Dictionary<BCCVertex, int> vertexToClasserId = new Dictionary<BCCVertex, int>();

        public ClasserMaker(Component component)
        {
            this.component = component;
        }

        /// <summary>
        /// this method gets a component and returns all classers and finds largest classer 
        /// </summary>
        /// <param name="component"></param>
        public void Process()
        {
            ClasserLabelTagging();
            ConstructClassers();
            component.LargestClasser = largestClasser;
        }

        /// <summary>
        /// this method will make classers from the data that has classer id of each vertex 
        /// </summary>
        private void ConstructClassers()
        {
            largestClasser = new Classer();
            var classerIdToClasser = new Dictionary<int, Classer>();
            foreach (var vertex in component.Vertices)
            {
                int classerId = vertexToClasserId[vertex];

                if (!classerIdToClasser.ContainsKey(classerId))
                {
                    classerIdToClasser[classerId] = new Classer();
                    classers.Add(classerIdToClasser[classerId]);
                }

                var classer = classerIdToClasser[classerId];
                classer.Vertices.Add(vertex);
                vertex.Classer = classer;

                if (classer.Vertices.Count > largestClasser.Vertices.Count)
                {
                    largestClasser = classer;
                }
            }
        }

        /// <summary>
        /// this method will tag a id number on each vertex
        /// </summary>
        private void ClasserLabelTagging()
        {
            var ARandomVertex = component.Vertices.ToList().First();
            vertexToClasserId = new BCCAlgorithm().NodingComponentFromThisVertex(component, ARandomVertex);
            ConstructClassers();
            var ARandomVertexFromLargestClasser = largestClasser.Vertices.ToList().First();
            vertexToClasserId = new BCCAlgorithm().NodingComponentFromThisVertex(component, ARandomVertexFromLargestClasser);
        }
    }
}
