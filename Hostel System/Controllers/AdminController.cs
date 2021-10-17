using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class AdminController : Controller
    {

    }
}
