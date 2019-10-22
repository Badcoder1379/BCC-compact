using System.Collections.Generic;

namespace BCCCompact.Models.BccAlgorithm
{
    public class Path
    {
        private readonly HashSet<Vertex> vertices = new HashSet<Vertex>();
        private readonly Stack<Vertex> list = new Stack<Vertex>();
        private readonly Dictionary<Vertex, int> children = new Dictionary<Vertex, int>();


        public void Push(Vertex vertex)
        {
            vertices.Add(vertex);
            list.Push(vertex);
            children[vertex] = 0;
        }

        public bool Contains(Vertex vertex)
        {
            return vertices.Contains(vertex);
        }

        public Vertex Peek()
        {
            return list.Peek();
        }

        public Vertex Pop()
        {
            var vertex = list.Pop();
            vertices.Remove(vertex);
            return vertex;
        }

        public int Count()
        {
            return vertices.Count;
        }

        public int Children()
        {
            return children[list.Peek()];
        }

        public void ChildrenUp()
        {
            children[list.Peek()] = children[list.Peek()] + 1;
        }

    }
}