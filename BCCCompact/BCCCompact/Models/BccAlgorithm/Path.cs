using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models.BccAlgorithm
{
    public class Path
    {
        HashSet<Vertex> vertices = new HashSet<Vertex>();
        Stack<Vertex> list = new Stack<Vertex>();
        Dictionary<Vertex, int> children = new Dictionary<Vertex, int>();
        int time = 0;

        public void Push(Vertex vertex)
        {
            vertex.disc = vertex.low = ++time;
            vertices.Add(vertex);
            list.Push(vertex);
            children[vertex] = 0;
        }

        public bool Contains(Vertex vertex)
        {
            if (vertices.Contains(vertex))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vertex Peek()
        {
            return list.Peek();
        }

        public Vertex Pop()
        {
            Vertex vertex = list.Pop();
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