using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_back_tictactoe.web.Models;
using web_back_tictactoe.web.Services;

namespace web_back_tictactoe.web.Controllers
{
    public class UserRegistrationController : Controller
    {
        private readonly IUserService _userService;

        public UserRegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var culture = Request.HttpContext.Session.GetString("culture");
            ViewBag.Language = culture;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                await _userService.RegisterUser(userModel);
                return RedirectToAction(nameof(EmailConfirmation), new { userModel.Email });

                //return Content($"User {userModel.FirstName} {userModel.LastName} has been registered sucessfully");
            }

            return View(userModel);
        }

        [HttpGet]
        public async Task<IActionResult> EmailConfirmation(string email)
        {
            var user = await _userService.GetUserByEmail(email);

            if (user?.IsEmailConfirmed == true)
            {
                return RedirectToAction("Index", "GameInvitation", new { email = email });
            }

            ViewBag.Email = email;
            //user.IsEmailConfirmed = true;
            //user.EmailConfirmationDate = DateTime.Now;
            //await _userService.UpdateUser(user);

            return View();
        }
    }
}