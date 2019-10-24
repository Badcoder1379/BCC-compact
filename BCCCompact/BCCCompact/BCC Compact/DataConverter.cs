using BCCCompact.Models;
using BCCCompact.Models.Elemans.Star;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.BCC_Compact
{
    public class DataConverter
    {
        private readonly Graph graph; 
        public DataConverter(Graph graph)
        {
            this.graph = graph;
        }

        public BCCGraph GetBCCGraph()
        {
            return new BCCGraph(graph);
        }


        public Graph GetResultGraph()
        {
            return graph;
        }
    }
}