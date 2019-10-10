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
            var result = CompactGraph(graph, new BCC());

            return Json(result);
        }

        [HttpPost]
        public JsonResult randomGraph(string query)
        {
            string[] str = query.Split('-');
            int V = int.Parse(str[0]);
            int E = int.Parse(str[1]);
            string fileName = str[2];

            var graph = Graph.getRandomGraph(V, E, fileName);
            var result = CompactGraph(graph, new BCC());
            return Json(result);
        }

        private CompactResult CompactGraph(Graph graph, Compact compact)
        {
            compact.Process(graph);
            var result = graph.getResult();
            return result;
        }



        [HttpPost]
        public JsonResult getFiles(string query)
        {
            string[] files = Directory.GetFiles(@"D:\Files\");
            return Json(files);
        }


        [HttpPost]
        public JsonResult deleteFile(string fileName)
        {
            string path = @"D:\Files\" + fileName;
            try
            {
                Directory.Delete(path);
            }
            catch 
            { 
                return Json("error"); 
            }
            return null;
        }

    }
}