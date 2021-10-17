using System.Security.Claims;
using Hostel_System.Core.IServices;
using Microsoft.AspNetCore.Http;

namespace Hostel_System.Core.Services
{
    public class UserContextServices : IUserContextServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal GetUser => _httpContextAccessor.HttpContext?.User;

        public int GetUserId()
        {
            if (GetUser is null) return -1;
            try
            {
                return int.Parse(GetUser.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            }
            catch
            {
                return -1;
            }
        }
    }

  
}