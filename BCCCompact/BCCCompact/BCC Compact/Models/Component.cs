using System.Collections.Generic;

namespace BCCCompact.Models
{
    public class Component
    {
        public HashSet<BCCVertex> Vertices = new HashSet<BCCVertex>();
        public Classer LargestClasser;
    }
}
