using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models.Compacts.BCC.Engines
{
    public class ListsMerger
    {
        public static LinkedList<Vertex> MergeLastOrbitalWithBoundryVertices(LinkedList<Vertex> lastOrbitalvertices, LinkedList<Vertex> vertices)
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
                if (vertices.Count == 0)
                {
                    newLinkedList.AddFirst(lastOrbitalvertices.First());
                    lastOrbitalvertices.RemoveFirst();
                    continue;
                }
                if (lastOrbitalvertices.Count == 0)
                {
                    newLinkedList.AddFirst(vertices.First());
                    vertices.RemoveFirst();
                    continue;
                }
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
            return newLinkedList;
        }
    }
}