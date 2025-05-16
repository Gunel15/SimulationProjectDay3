using Microsoft.AspNetCore.Mvc;

namespace MVCProjectDay3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
