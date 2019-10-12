using System.Collections.Generic;
using System.Linq;

namespace BCCCompact.Models.Compacts.MMD.Engines
{
    public class TrianglePicker
    {
        public void Process(Component component)
        {
            PickVertices(component.LargestTriangle);
        }

        private void PickVertices(Triangle currentTriangle)
        {
            var adjacenty = currentTriangle.AdjacentNodesWithConnectiongThisVertex;
            var boundryVertices = new LinkedList<Vertex>(adjacenty.Keys);
            boundryVertices.Remove(currentTriangle.VertexConnectorToParent);

            var otherVertices = new HashSet<Vertex>(currentTriangle.Vertices);
            otherVertices.ExceptWith(boundryVertices);
            otherVertices.Remove(currentTriangle.VertexConnectorToParent);

            var orbitals = GetreadyOrbitalsForPushing(currentTriangle);

            PushWithVertices(orbitals, otherVertices);

            MergeLastOrbitalWithBoundryVertices(orbitals.First(), boundryVertices);

            currentTriangle.Orbitals = orbitals;

            double edgeLenghtToChild = orbitals.Count * 2;

            double angleCounter = 0;
            foreach (var vertex in boundryVertices)
            {
                foreach (var child in adjacenty[vertex])
                {
                    if (child != currentTriangle.Parent)
                    {
                        child.AngleToConnectToParent = angleCounter + child.CenteralAngle / 2;
                        child.EdgeToParentLenght = edgeLenghtToChild;
                        angleCounter += child.CenteralAngle;
                    }
                }
            }
            foreach (Triangle child in currentTriangle.Children)
            {
                PickVertices(child);
            }
        }

        private LinkedList<LinkedList<Vertex>> GetreadyOrbitalsForPushing(Triangle currentTriangle)
        {
            var orbitals = new LinkedList<LinkedList<Vertex>>();

            if (currentTriangle.VertexConnectorToParent != null)
            {
                orbitals.AddFirst(new LinkedList<Vertex>());
                orbitals.First().AddFirst(currentTriangle.VertexConnectorToParent);
            }
            return orbitals;
        }

        private void PushWithVertices(LinkedList<LinkedList<Vertex>> orbitals, HashSet<Vertex> vertices)
        {
            int lastOrbitalLength = orbitals.Count;
            int lastOrbitalPointer = 0;
            foreach (Vertex vertex in vertices)
            {
                if (lastOrbitalPointer == 0)
                {
                    orbitals.AddFirst(new LinkedList<Vertex>());
                }
                lastOrbitalPointer++;
                orbitals.First().AddFirst(vertex);
                if (lastOrbitalPointer > lastOrbitalLength)
                {
                    lastOrbitalPointer = 0;
                    lastOrbitalLength++;
                }
            }
        }

        private void MergeLastOrbitalWithBoundryVertices(LinkedList<Vertex> lastOrbitalvertices, LinkedList<Vertex> vertices)
        {
            var newLinkedList = new LinkedList<Vertex>();
            Dictionary<Vertex, double> angleOfVertices = new Dictionary<Vertex, double>();
            int i = 0;
            foreach (Vertex vertex in lastOrbitalvertices)
            {
                angleOfVertices[vertex] = i / lastOrbitalvertices.Count;
                i++;
            }
            foreach (Vertex vertex in vertices)
            {
                angleOfVertices[vertex] = i / vertices.Count;
                i++;
            }
            while (angleOfVertices.Count > 0 || vertices.Count > 0)
            {
                if (angleOfVertices[vertices.First()] > angleOfVertices[lastOrbitalvertices.First()])
                {
                    newLinkedList.AddFirst(lastOrbitalvertices.First());
                    lastOrbitalvertices.RemoveFirst();
                }
                else
                {
                    newLinkedList.AddFirst(vertices.First());
                    vertices.RemoveFirst();
                }
            }

        }
    }
}