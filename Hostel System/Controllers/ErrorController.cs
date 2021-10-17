using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Forbidden")]
        public IActionResult Forbidden()
        {
            return View();
        }
        [Route("/NotFound")]
        public IActionResult NotFound()
        {
            return Redirect("Forbidden");
        }
    }
}
