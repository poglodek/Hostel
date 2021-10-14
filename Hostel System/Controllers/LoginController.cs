﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hostel_System.Core.IServices;
using Hostel_System.Dto;
using Hostel_System.Dto.Dto;
using Hostel_System.Mappers;
using Hostel_System.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
    public class LoginController : Controller 
    {
        private readonly IUserServices _userServices;
        private readonly HostelSystemModelMapper _mapper;

        public LoginController(IUserServices userServices,
            HostelSystemModelMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }
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
        public IActionResult Register(RegisterUserModel registerUserModel)
        {
            var registerUserDto = _mapper.MapToDto(registerUserModel);
            _userServices.RegisterUser(registerUserDto);
            return View();
        }
    }
}