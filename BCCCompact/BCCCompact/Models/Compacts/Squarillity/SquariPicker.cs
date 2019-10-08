using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models.Compacts.Squarillity
{
    public class SquariPicker
    {
        private Tuple<int, int> Pointer;
        private readonly double Side = 50;
        private readonly double V;
        private readonly int Row;

        public SquariPicker(int numberOfVertices)
        {
            this.V = numberOfVertices;
            Row = (int)Math.Pow(V, 0.5);
            if (V - Row * Row > Row)
            {
                Row++;
            }
            Pointer = new Tuple<int, int>(0, 0);
        }

        public void SetLocation(Vertex vertex)
        {
            vertex.SetLocation(Pointer.Item1 * Side, Pointer.Item2 * Side);
            NextPointer();
        }

        private void NextPointer()
        {
            int x = Pointer.Item1;
            int y = Pointer.Item2;
            y++;
            if (y >= Row)
            {
                x++;
                y = 0;
            }
            Pointer = new Tuple<int, int>(x,y);
        }
    }
}