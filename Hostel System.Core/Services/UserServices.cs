using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hostel_System.Core.IServices;
using Hostel_System.Database;
using Hostel_System.Database.Entity;
using Hostel_System.Dto.Dto;
using Microsoft.AspNetCore.Identity;

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
            _hostelSystemDbContext.Users.Add(user);
            _hostelSystemDbContext.SaveChanges();
            return user.Id;
        }
    }
}
