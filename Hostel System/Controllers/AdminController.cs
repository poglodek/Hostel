using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

    }
}
