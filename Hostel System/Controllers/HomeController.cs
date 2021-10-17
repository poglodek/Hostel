using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}