using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using server_render_elm.Models;

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
            
            ViewBag.flags = flags;
            return View();
        }
    }
}
