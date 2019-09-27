using BCCCompact.Models;
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
            Importer importer = new Importer(query);
            Graph graph = importer.import();

            BccCompact bcc = new BccCompact();
            bcc.Process(graph);
            CompactResult result = graph.getResult();

            return Json(result);
        }

        [HttpPost]
        public JsonResult randomGraph(string query)
        {
            string[] str = query.Split('-');
            int V = int.Parse(str[0]);
            int E = int.Parse(str[1]);

            Graph graph = Graph.getRandomGraph(V, E);
            BCC Bcc = new BCC();
            Bcc.Process(graph);
            CompactResult result = graph.getResult();

            return Json(result);
        }
    }
}