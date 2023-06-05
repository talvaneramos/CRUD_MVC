using Microsoft.AspNetCore.Mvc;

namespace Projeto.Presentation.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
