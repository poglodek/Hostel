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
using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
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
        [HttpGet("/Login")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/Login")]
        public async Task<IActionResult> Index(LoginModel loginModel)
        {
            var user = _mapper.MapToDto(loginModel);
            if (!ModelState.IsValid || !_userServices.VerifyUser(user))
            {
                ViewBag.ErrorMessage = "Email and Password is wrong!";
                return View();
            }
            await HttpContext.SignInAsync(_userServices.GetClaimsPrincipal(user));
            return Ok(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value);
        }
        [HttpGet("/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("/Register")]
        public IActionResult Register(RegisterUserModel registerUserModel)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "All fields required!";
                return View();
            }
            var registerUserDto = _mapper.MapToDto(registerUserModel);
            _userServices.RegisterUser(registerUserDto);
            return Ok("TO DO");
        }
    }
}
