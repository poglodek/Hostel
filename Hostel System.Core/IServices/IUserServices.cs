using System.Security.Claims;
using Hostel_System.Dto.Dto;

namespace Hostel_System.Core.IServices;

public interface IUserServices
{
    int RegisterUser(RegisterUserDto userDto);
    bool VerifyUser(LoginDto loginDto);
    ClaimsPrincipal GetClaimsPrincipal(LoginDto loginDto);
}