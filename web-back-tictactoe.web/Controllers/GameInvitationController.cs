using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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

        [HttpGet]
        public IActionResult Index(string email)
        {
            var culture = Request.HttpContext.Session.GetString("culture");
            ViewBag.Language = culture;

            var gameInvitationModel = new GameInvitationModel {InvitedBy = email};
            HttpContext.Session.SetString("email", email);
            return View(gameInvitationModel);
        }

        [HttpPost]
        public IActionResult Index(GameInvitationModel gameInvitationModel, [FromServices] IEmailService emailService)
        {
            var gameInvitationService = Request.HttpContext.RequestServices.GetService<IGameInvitationService>();
            if (ModelState.IsValid)
            {
                emailService.SendEmail(gameInvitationModel.EmailTo,
                    _stringLocalizer["Invitation for playing a Tic-Tac-Toe game"],
                    _stringLocalizer[
                        $"Hello, you have been invited to play the Tic-Tac-Toe game by {0}. For joining the game, please click here {1}",
                        gameInvitationModel.InvitedBy, Url.Action("GameInvitationConfirmation", "GameInvitation",
                            new {gameInvitationModel.InvitedBy, gameInvitationModel.EmailTo},
                            Request.Scheme,
                            Request.Host.ToString())]);

                var invitation = gameInvitationService.Add(gameInvitationModel).Result;
                return RedirectToAction("GameInvitationConfirmation", new {id = invitation.Id});
            }

            return View(gameInvitationModel);
        }

        [HttpGet]
        public IActionResult GameInvitationConfirmation(Guid id, [FromServices]IGameInvitationService gameInvitationService)
        {
            var gameInvitation = gameInvitationService.Get(id).Result;
            return View(gameInvitation);
        }
    }
}