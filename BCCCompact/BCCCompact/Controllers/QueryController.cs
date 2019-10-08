using BCCCompact.Models;
using BCCCompact.Models.Compacts;
using BCCCompact.Models.Compacts.Squarillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCCCompact.Controllers
{
    public class QueryController : Controller
    {

        [HttpPost]
        public JsonResult compact(string query)
        {
            var importer = new Importer(query);
            Graph graph = importer.Import();

            var result = CompactGraph(graph, new SQR());

            return result;
        }

        [HttpPost]
        public JsonResult randomGraph(string query)
        {
            string[] str = query.Split('-');
            int V = int.Parse(str[0]);
            int E = int.Parse(str[1]);

            var graph = Graph.getRandomGraph(V, E);
            var result = CompactGraph(graph, new SQR());
            return result;
        }

        private JsonResult CompactGraph(Graph graph, Compact compact)
        {

            compact.Process(graph);
            var result = graph.getResult();
            return Json(result);
        }

    }
}