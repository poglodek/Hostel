using Hostel_System.Dto.Dto;

namespace Hostel_System.Core.IServices;

public interface IUserServices
{
    int RegisterUser(RegisterUserDto userDto);
}