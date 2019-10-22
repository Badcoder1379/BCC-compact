using System.Collections.Generic;

namespace BCCCompact.Models.BccAlgorithm
{
    public class Path
    {
        private readonly HashSet<Vertex> vertices = new HashSet<Vertex>();
        private readonly Stack<Vertex> list = new Stack<Vertex>();
        private readonly Dictionary<Vertex, int> children = new Dictionary<Vertex, int>();

        /// <summary>
        /// push a vertex on your path
        /// </summary>
        /// <param name="vertex"></param>
        public void Push(Vertex vertex)
        {
            vertices.Add(vertex);
            list.Push(vertex);
            children[vertex] = 0;
        }

        /// <summary>
        /// if your path contains this vertex returns true and else return false
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool Contains(Vertex vertex)
        {
            return vertices.Contains(vertex);
        }

        /// <summary>
        /// returns last vertex of your path
        /// </summary>
        /// <returns></returns>
        public Vertex Peek()
        {
            return list.Peek();
        }

        /// <summary>
        /// remove last vertex of your path
        /// </summary>
        /// <returns></returns>
        public Vertex Pop()
        {
            var vertex = list.Pop();
            vertices.Remove(vertex);
            return vertex;
        }

        /// <summary>
        /// returns number of vertices in path
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return vertices.Count;
        }

        /// <summary>
        /// returns number of children of last vertex of path
        /// </summary>
        /// <returns></returns>
        public int Children()
        {
            return children[list.Peek()];
        }

        /// <summary>
        /// add a child to children of last vertex of path
        /// </summary>
        public void ChildrenUp()
        {
            children[list.Peek()] = children[list.Peek()] + 1;
        }

    }
}