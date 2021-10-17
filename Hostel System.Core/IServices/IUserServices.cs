using System.Security.Claims;
using Hostel_System.Database.Entity;
using Hostel_System.Dto.Dto;

namespace Hostel_System.Core.IServices;

public interface IUserServices
{
    int RegisterUser(RegisterUserDto userDto);
    bool VerifyUser(LoginDto loginDto);
    User GetUserById(int id);
    ClaimsPrincipal GetClaimsPrincipal(LoginDto loginDto);
}