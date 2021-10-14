using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hostel_System.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
    public class LoginController : Controller 
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Email and Password is required";
                return View();
            }

            return Ok("ok");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            return View();
        }
    }
}
