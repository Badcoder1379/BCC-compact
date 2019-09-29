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
        public ActionResult compact(string query)
        {
            Importer importer = new Importer(query);
            Graph graph = importer.import();

            BCC bcc = new BCC();
            bcc.Process(graph);
            CompactResult result = graph.getResult();

            return new JsonCamelCaseResult(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult randomGraph(string query)
        {
            string[] str = query.Split('-');
            int V = int.Parse(str[0]);
            int E = int.Parse(str[1]);

            Graph graph = Graph.getRandomGraph(V, E);
            BCC bcc = new BCC();
            bcc.Process(graph);
            CompactResult result = graph.getResult();

            return new JsonCamelCaseResult(result, JsonRequestBehavior.AllowGet);
        }
    }
}