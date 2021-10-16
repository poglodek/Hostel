﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hostel_System.Core.IServices;
using Hostel_System.Database;
using Hostel_System.Database.Entity;
using Hostel_System.Dto.Dto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hostel_System.Core.Services
{
    public class UserServices : IUserServices
    {
        private readonly HostelSystemDbContext _hostelSystemDbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRoleServices _roleServices;

        public UserServices(HostelSystemDbContext hostelSystemDbContext
            , IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IRoleServices roleServices)
        {
            _hostelSystemDbContext = hostelSystemDbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _roleServices = roleServices;
        }
        public int RegisterUser(RegisterUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.RoleName = _roleServices.GetDefaultRole();
            var hashedPassword = _passwordHasher.HashPassword(user,userDto.Password);
            user.Password = hashedPassword;
            _hostelSystemDbContext.Users.Add(user);
            _hostelSystemDbContext.SaveChanges();
            return user.Id;
        }

        public bool VerifyUser(LoginDto loginDto)
        {
            var user = GetUserByEmail(loginDto.Email);
            var result =_passwordHasher.VerifyHashedPassword(user, user.Password ,loginDto.Password);
            if (result == PasswordVerificationResult.Failed) return false;
            return true;
        }

        public ClaimsPrincipal GetClaimsPrincipal(LoginDto loginDto)
        {
            var user = GetUserByEmail(loginDto.Email);
            var claims = GetClaims(user);
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(claimsIdentity);
        }
        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.RoleName.RoleName}")
            };
            return claims;
        }
        private User GetUserByEmail(string email)
        {
            var user = _hostelSystemDbContext
                .Users
                .Include(x => x.RoleName)
                .FirstOrDefault(x => x.Email.Equals(email));
            if (user is null) throw new Exception("User not Found");
            return user;
        }
    }
}
