using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;

namespace server_render_elm.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Elm([FromServices] INodeServices nodeServices)
        {
            var flags = "{\"count\": 25}";
            var result = await nodeServices.InvokeExportAsync<string>("./NodeCode/render-elm.js", "renderElmFromJS", flags);
            ViewBag.fromNode = result;
            ViewBag.flags = flags;
            return View();
        }
    }
}
