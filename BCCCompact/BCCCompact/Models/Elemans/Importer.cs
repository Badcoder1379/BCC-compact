using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    class Importer
    {
        private string FileName { get; set; }
        private Dictionary<string, int> StrToId;
        public Importer(string fileName)
        {
            this.FileName = fileName;
        }

        public Graph Import()
        {
            StrToId = new Dictionary<string, int>();
            var lastNumberUsed = 0;
            var lines = File.ReadAllLines(FileName);
            var edges = new HashSet<Edge>();

            foreach (string line in lines)
            {

                string[] parts = line.Split(new char[] { ',' });

                if (!StrToId.ContainsKey(parts[0]))
                {
                    StrToId[parts[0]] = lastNumberUsed++;
                }

                if (!StrToId.ContainsKey(parts[1]))
                {
                    StrToId[parts[1]] = lastNumberUsed++;
                }

                var u = StrToId[parts[0]];
                var v = StrToId[parts[1]];

                edges.Add(new Edge(u, v));
            }
            var V = StrToId.Count();
            var graph = new Graph(V, edges);
            return graph;
        }
    }
}