using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using web_back_tictactoe.web.Models;
using web_back_tictactoe.web.Services;

namespace web_back_tictactoe.web.Controllers
{
    public class GameInvitationController : Controller
    {
        private readonly IStringLocalizer<GameInvitationController> _stringLocalizer;
        private readonly IUserService _userService;

        public GameInvitationController(IStringLocalizer<GameInvitationController> stringLocalizer,
            IUserService userService)
        {
            _stringLocalizer = stringLocalizer;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Index(GameInvitationModel model)
        {
            return Content(_stringLocalizer["GameInvitationConfirmationMessage", model.EmailTo]);
        }


        [HttpGet]
        public IActionResult Index(string email)
        {
            var culture = Request.HttpContext.Session.GetString("culture");
            ViewBag.Language = culture;

            var gameInvitationModel = new GameInvitationModel {InvitedBy = email};
            HttpContext.Session.SetString("email", "email");
            return View(gameInvitationModel);
        }
    }
}