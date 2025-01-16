using Microsoft.AspNetCore.Mvc;

namespace web_museu.Controllers
{
    public class ObrasController : Controller
    {
        public IActionResult Obras()
        {
            return View();
        }
    }
}
