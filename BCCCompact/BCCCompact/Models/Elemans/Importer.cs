using BCCCompact.Models.Elemans.Star;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BCCCompact.Models
{
    class Importer
    {
        public static string SRCAddress = @"D:\Projects\Compact\Files\";
        private string FileName { get; set; }
        private Dictionary<string, int> VerticesNameToID;
        public Importer(string fileName)
        {
            this.FileName = fileName;
        }

        public Graph Import()
        {
            VerticesNameToID = new Dictionary<string, int>();
            var lastNumberUsed = 0;
            string path = SRCAddress + FileName;

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

                if (parts.Length < 2)
                {
                    continue;
                }

                if (!VerticesNameToID.ContainsKey(parts[0]))
                {
                    VerticesNameToID[parts[0]] = lastNumberUsed++;
                }

                if (!VerticesNameToID.ContainsKey(parts[1]))
                {
                    VerticesNameToID[parts[1]] = lastNumberUsed++;
                }

                var u = VerticesNameToID[parts[0]];
                var v = VerticesNameToID[parts[1]];

                edges.Add(new Edge(u, v));
            }
            var V = VerticesNameToID.Count();
            var graph = new Graph(V, edges);
            return graph;
        }

        
    }
}