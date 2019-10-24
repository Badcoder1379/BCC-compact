using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models.Compacts.BCC.Engines.ComponentMaker
{
    public class IteratorPack
    {
        public Vertex vertex;
        public Component component;

        public IteratorPack(Vertex vertex, Component component)
        {
            this.vertex = vertex;
            this.component = component;
        }
    }
}