using Hostel_System.Core.IServices;
using Hostel_System.Dto.Dto;
using Hostel_System.Mappers;
using Hostel_System.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hostel_System.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly HostelSystemModelMapper _mapper;

        public UserController(IUserServices userServices,
            HostelSystemModelMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet("/Login")]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost("/Login")]
        public async Task<IActionResult> Index(LoginModel loginModel)
        {
            var user = _mapper.Map<LoginDto>(loginModel);
            if (!ModelState.IsValid || user is null || !_userServices.VerifyUser(user))
            {
                ViewBag.ErrorMessage = "Email or Password is wrong!";
                return View();
            }

            await HttpContext.SignInAsync(_userServices.GetClaimsPrincipal(user));
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]

        [HttpGet("/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost("/Register")]
        public IActionResult Register(RegisterUserModel registerUserModel)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "All fields required!";
                return View();
            }
            var registerUserDto = _mapper.Map<RegisterUserDto>(registerUserModel);
            var userId = _userServices.RegisterUser(registerUserDto);
            if (userId == -1)
            {
                ViewBag.ErrorMessage = "User Exist! Please Login in!";
                return View();
            }
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public async Task<IActionResult> SingOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Me()
        {
            return View();
        }
        [HttpGet("AllUsers")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult AllUsers([FromQuery] string SearchParse)
        {
            var users = _userServices.GetAllUsers();
            TempData["Users"] = JsonConvert.SerializeObject(users);
            return RedirectToAction("UsersList", "User");
        }
        [HttpGet("GetUserByName")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetUserByName([FromQuery] string SearchParse)
        {
            var users = _userServices.GetUsersByName(SearchParse);
            TempData["Users"] = JsonConvert.SerializeObject(users);
            return RedirectToAction("UsersList", "User");
        }
        [HttpGet("UsersList")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult UsersList()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(TempData["Users"] as string);
            return View(model);
        }
        [HttpGet("GetUserByPhone")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetUserByPhone([FromQuery] string SearchParse)
        {
            var users = _userServices.GetUsersByPhone(SearchParse);
            TempData["Users"] = JsonConvert.SerializeObject(users);
            return RedirectToAction("UsersList", "User");
        }
        [HttpGet("GetUserByEmail")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetUserByEmail([FromQuery] string SearchParse)
        {
            var users = _userServices.GetUsersByEmail(SearchParse);
            TempData["Users"] = JsonConvert.SerializeObject(users);
            return RedirectToAction("UsersList", "User");
        }
        [HttpGet("ForgotPassword")]
        public IActionResult ForgotPassword([FromQuery] string mail)
        {
            _userServices.ForgotPassword(mail);
            return View();
        }
        [HttpGet("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Result = false;
                return View();
            }

            var result = _userServices.ChangePassword(_mapper.Map<ChangePasswordDto>(changePasswordModel));
            ViewBag.Result = result;
            return View();
        }
    }
}
