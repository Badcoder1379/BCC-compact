using System.Collections.Generic;

namespace BCCCompact.Models.BCCAlgorithm
{
    public class BccPath
    {
        private readonly HashSet<BccVertex> vertices = new HashSet<BccVertex>();
        private readonly Stack<BccVertex> verticesStack = new Stack<BccVertex>();
        private readonly Dictionary<BccVertex, int> children = new Dictionary<BccVertex, int>();

        /// <summary>
        /// push a vertex on your path
        /// </summary>
        /// <param name="vertex"></param>
        public void Push(BccVertex vertex)
        {
            vertices.Add(vertex);
            verticesStack.Push(vertex);
            children[vertex] = 0;
        }

        /// <summary>
        /// if your path contains this vertex returns true and else return false
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool Contains(BccVertex vertex)
        {
            return vertices.Contains(vertex);
        }

        /// <summary>
        /// returns last vertex of your path
        /// </summary>
        /// <returns></returns>
        public BccVertex Peek()
        {
            return verticesStack.Peek();
        }

        /// <summary>
        /// remove last vertex of your path
        /// </summary>
        /// <returns></returns>
        public BccVertex Pop()
        {
            var vertex = verticesStack.Pop();
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
            return children[verticesStack.Peek()];
        }

        /// <summary>
        /// add a child to children of last vertex of path
        /// </summary>
        public void ChildrenUp()
        {
            children[verticesStack.Peek()] = children[verticesStack.Peek()] + 1;
        }
    }
}
