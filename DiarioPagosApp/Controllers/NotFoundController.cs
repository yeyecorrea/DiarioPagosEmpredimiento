using DiarioPagosApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DiarioPagosApp.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult NotFoundError()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ModelError()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
