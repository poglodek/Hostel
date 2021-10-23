using AutoMapper;
using Hostel_System.Core.Exception;
using Hostel_System.Core.IServices;
using Hostel_System.Database;
using Hostel_System.Database.Entity;
using Hostel_System.Dto.Dto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hostel_System.Core.Services
{
    public class UserServices : IUserServices
    {
        private readonly HostelSystemDbContext _hostelSystemDbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMailServices _mailServices;
        private readonly IRoleServices _roleServices;

        public UserServices(HostelSystemDbContext hostelSystemDbContext
            , IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IMailServices mailServices,
            IRoleServices roleServices)
        {
            _hostelSystemDbContext = hostelSystemDbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _mailServices = mailServices;
            _roleServices = roleServices;
        }
        public int RegisterUser(RegisterUserDto userDto)
        {
            if (CheckIfUserExist(userDto.Email)) return -1;
            var user = _mapper.Map<User>(userDto);
            user.RoleName = _roleServices.GetDefaultRole();
            var hashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            user.Password = hashedPassword;
            _hostelSystemDbContext.Users.Add(user);
            _hostelSystemDbContext.SaveChanges();
            return user.Id;
        }
        public int CreateUser(RegisterUserDto userDto)
        {
            if (CheckIfUserExist(userDto.Email)) return -1;
            var user = _mapper.Map<User>(userDto);
            user.RoleName = _roleServices.GetDefaultRole();
            user.Password = "";
            _hostelSystemDbContext.Users.Add(user);
            _hostelSystemDbContext.SaveChanges();
            return user.Id;
        }

        public void ForgotPassword(string email)
        {
            var user = GetUserByEmail(email);
            _mailServices.ForgotPassword(email, user);
        }

        public bool VerifyUser(LoginDto loginDto)
        {
            var user = GetUserByEmail(loginDto.Email);
            if (user is null || string.IsNullOrWhiteSpace(user.Password)) return false;
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
            if (result == PasswordVerificationResult.Failed) return false;
            return true;
        }

        public User GetUserById(int id)
        {
            return _hostelSystemDbContext
                .Users
                .Include(x=>x.RoleName)
                .FirstOrDefault(x => x.Id == id);
        }
        public User GetUserByEmail(string email)
        {
            return _hostelSystemDbContext
                .Users
                .Include(x=> x.RoleName)
                .FirstOrDefault(x => x.Email.Contains(email));
        }

        public ClaimsPrincipal GetClaimsPrincipal(LoginDto loginDto)
        {
            var user = GetUserByEmail(loginDto.Email);
            var claims = GetClaims(user);
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(claimsIdentity);
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<UserDto>>(_hostelSystemDbContext
                .Users
                .AsQueryable());
        }

        public IEnumerable<UserDto> GetUsersByName(string searchParse)
        {
            return _mapper.Map<IEnumerable<UserDto>>(_hostelSystemDbContext
                .Users
                .Where(x => (x.FirstName + " " + x.LastName).Contains(searchParse))
                .AsQueryable());
        }

        public IEnumerable<UserDto> GetUsersByPhone(string searchParse)
        {
            return _mapper.Map<IEnumerable<UserDto>>(_hostelSystemDbContext
                .Users
                .Where(x => x.Phone.ToString().Contains(searchParse))
                .AsQueryable());
        }

        public IEnumerable<UserDto> GetUsersByEmail(string searchParse)
        {
            return _mapper.Map<IEnumerable<UserDto>>(_hostelSystemDbContext
                .Users
                .Where(x => x.Email.Contains(searchParse))
                .AsQueryable());
        }

        public bool ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = GetUserByEmail(changePasswordDto.Email);
            var oldPasswordCorrect =
                _passwordHasher.VerifyHashedPassword(user, user.Password, changePasswordDto.ActualPassword);
            if (oldPasswordCorrect == PasswordVerificationResult.Failed || changePasswordDto.ReTypNewPassword != changePasswordDto.NewPassword) return false;
            var newPassword = _passwordHasher.HashPassword(user, changePasswordDto.NewPassword);
            user.Password = newPassword;
            _hostelSystemDbContext.SaveChanges();
            return true;
        }

        public bool ChangeData(UserDto userDto)
        {
            var user = GetUserById(userDto.Id);
            if (user is null) return false;
            user.Address = userDto.Address;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.BirthDay = userDto.BirthDay;
            user.CarRegistrationNumber = userDto.CarRegistrationNumber;
            user.Phone = userDto.Phone;
            _hostelSystemDbContext.SaveChanges();
            return true;

        }

        public UserDto GetUserDtoById(int id)
        {
            return _mapper.Map<UserDto>(GetUserById(id));
        }

        public void UpdateRole(UserDto userDto)
        {
            var role = _roleServices.GetRoleByNamme(userDto.RoleName);
            var user = GetUserById(userDto.Id);
            user.RoleName = role;
            _hostelSystemDbContext.SaveChanges();
        }

        private List<Claim> GetClaims(User user)
        {
            if (user is null) throw new NotFoundException("User not found.");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.RoleName.RoleName}")
            };
            return claims;
        }

        private bool CheckIfUserExist(string email)
        {
            return _hostelSystemDbContext.Users.Any(x => x.Email.Equals(email));
        }
    }
}
