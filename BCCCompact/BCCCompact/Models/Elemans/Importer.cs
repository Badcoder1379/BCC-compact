using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    class Importer
    {
        private string fileName { get; set; }
        private Dictionary<string, int> strToId;
        public Importer(string fileName)
        {
            this.fileName = fileName;
        }

        public Graph import()
        {
            strToId = new Dictionary<string, int>();
            int lastNumberUsed = 0;
            string[] lines = File.ReadAllLines(@"MmdData/" + fileName);
            HashSet<Edge> edges = new HashSet<Edge>();
            foreach (string line in lines)
            {

                string[] parts = line.Split(new char[] { ',' });
                if (!strToId.Keys.Contains(parts[0]))
                {
                    strToId[parts[0]] = lastNumberUsed++;
                }
                if (!strToId.Keys.Contains(parts[1]))
                {
                    strToId[parts[1]] = lastNumberUsed++;
                }

                int u = strToId[parts[0]];
                int v = strToId[parts[1]];

                edges.Add(new Edge(u, v));
            }
            int V = strToId.Count();
            Graph graph = new Graph(V, edges);
            return graph;
        }
    }
}