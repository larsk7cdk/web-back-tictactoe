using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_back_tictactoe.web.Models;
using web_back_tictactoe.web.Services;

namespace web_back_tictactoe.web.Controllers
{
    public class GameInvitationController : Controller
    {
        private readonly IUserService _userService;

        public GameInvitationController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string email)
        {
            var gameInvitationModel = new GameInvitationModel { InvitedBy = email };
            return View(gameInvitationModel);
        }
    }
}