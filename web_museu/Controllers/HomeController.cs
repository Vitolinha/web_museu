using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using web_museu.Models;

namespace web_museu.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Iniciar()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
