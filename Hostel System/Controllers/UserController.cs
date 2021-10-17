using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hostel_System.Core.IServices;
using Hostel_System.Dto;
using Hostel_System.Dto.Dto;
using Hostel_System.Mappers;
using Hostel_System.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
    [Authorize]
    public class UserController : Controller 
    {
        private readonly IUserServices _userServices;
        private readonly HostelSystemModelMapper _mapper;

        public UserController(IUserServices userServices,
            HostelSystemModelMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet("/Login")]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost("/Login")]
        public async Task<IActionResult> Index(LoginModel loginModel)
        {
            var user = _mapper.MapToDto(loginModel);
            if (!ModelState.IsValid || !_userServices.VerifyUser(user))
            {
                ViewBag.ErrorMessage = "Email or Password is wrong!";
                return View();
            }

            await HttpContext.SignInAsync(_userServices.GetClaimsPrincipal(user));
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]

        [HttpGet("/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost("/Register")]
        public IActionResult Register(RegisterUserModel registerUserModel)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "All fields required!";
                return View();
            }
            var registerUserDto = _mapper.MapToDto(registerUserModel);
            var userId = _userServices.RegisterUser(registerUserDto);
            if (userId == -1)
            {
                ViewBag.ErrorMessage = "User Exist! Please Login in!";
                return View();
            }
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public async Task<IActionResult> SingOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
