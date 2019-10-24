using BCCCompact.BCC_Compact;
using BCCCompact.BCC_Compact.logic;
using BCCCompact.Models.Compacts;
using BCCCompact.Models.Elemans.Star;

namespace BCCCompact.Models
{
    public class PackageBCC
    {

        private readonly Graph graph;
        public PackageBCC(Graph graph)
        {
            this.graph = graph;
        }



        /// <summary>
        /// this method give the gragh and set the vertices locations
        /// </summary>
        /// <param name="graph"></param>
        public void Process()
        {
            var converter = new DataConverter(graph);
            var g = converter.GetBCCGraph();
            var business = new Business(g);
            business.Process();
            converter.GetResultGraph();
        }
    }
}
