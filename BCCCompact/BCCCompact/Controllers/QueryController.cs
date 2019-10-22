using BCCCompact.Models;
using BCCCompact.Models.Compacts;
using System.IO;
using System.Web.Mvc;

namespace BCCCompact.Controllers
{
    public class QueryController : Controller
    {

        [HttpPost]
        public JsonResult compact(string query)
        {
            var importer = new Importer(query);
            Graph graph;
            
            try
            {
                graph = importer.Import();
            }
            catch {
                return null;
            }
            var result = CompactGraph(graph);

            return Json(result);
        }

        [HttpPost]
        public JsonResult randomGraph(string query)
        {
            string[] str = query.Split('-');
            int V = int.Parse(str[0]);
            int E = int.Parse(str[1]);
            string fileName = str[2];

            var graph = Graph.GetRandomGraph(V, E, fileName);
            var result = CompactGraph(graph);
            return Json(result);
        }

        private CompactResult CompactGraph(Graph graph)
        {
            new BCC(graph).Process();
            var result = graph.GetResult();
            return result;
        }



        [HttpPost]
        public JsonResult getFiles(string query)
        {
            string[] files = Directory.GetFiles(Importer.SRCAddress);
            return Json(files);
        }


        [HttpPost]
        public JsonResult deleteFile(string query)
        {
            string alertTxt;
            string path = Importer.SRCAddress + query;
            try
            {
                System.IO.File.Delete(path);
                alertTxt = query + "deleted successfully!";
            }
            catch 
            {
                alertTxt = "there isn't any file by this name!";
            }
            return Json(alertTxt);
        }

    }
}