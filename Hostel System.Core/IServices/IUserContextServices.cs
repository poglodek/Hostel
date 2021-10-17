using System.Security.Claims;

namespace Hostel_System.Core.IServices;

public interface IUserContextServices
{
    ClaimsPrincipal GetUser { get; }
    int GetUserId();
}