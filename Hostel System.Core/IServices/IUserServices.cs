using Hostel_System.Database.Entity;
using Hostel_System.Dto.Dto;
using System.Security.Claims;

namespace Hostel_System.Core.IServices;

public interface IUserServices
{
    int RegisterUser(RegisterUserDto userDto);
    int CreateUser(RegisterUserDto userDto);
    void ForgotPassword(string email);
    bool VerifyUser(LoginDto loginDto);
    User GetUserById(int id);
    User GetUserByEmail(string email);
    ClaimsPrincipal GetClaimsPrincipal(LoginDto loginDto);
    IEnumerable<UserDto> GetAllUsers();
    IEnumerable<UserDto> GetUsersByName(string searchParse);
    IEnumerable<UserDto> GetUsersByPhone(string searchParse);
    IEnumerable<UserDto> GetUsersByEmail(string searchParse);
    bool ChangePassword(ChangePasswordDto changePasswordDto);
    bool ChangeData(UserDto userDto);
    UserDto GetUserDtoById(int id);
    void UpdateRole(UserDto userDto);
}