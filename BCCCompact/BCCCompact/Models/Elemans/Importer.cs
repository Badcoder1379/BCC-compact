using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            string path = @"D:\Files\" + FileName;

            var lines = File.ReadAllLines(path);

            if (lines.Length == 1)
            {
                lines = lines[0].Split(' ');
            }

            var edges = new HashSet<Edge>();

            foreach (string line in lines)
            {
                if (line.Equals(""))
                {
                    continue;
                }

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