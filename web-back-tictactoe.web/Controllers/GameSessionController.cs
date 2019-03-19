﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using web_back_tictactoe.web.Services;

namespace web_back_tictactoe.web.Controllers
{
    public class GameSessionController : Controller
    {
        private readonly IGameSessionService _gameSessionService;

        public GameSessionController(IGameSessionService gameSessionService)
        {
            _gameSessionService = gameSessionService;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var session = await _gameSessionService.GetGameSession(id);
            if (session == null)
            {
                var gameInvitationService = Request.HttpContext.RequestServices.GetService<IGameInvitationService>();
                var invitation = await gameInvitationService.Get(id);
                session = await _gameSessionService.CreateGameSession(invitation.Id, invitation.InvitedBy,
                    invitation.EmailTo);
            }

            return View(session);
        }

        public async Task<IActionResult> SetPosition(Guid id, string email, int x, int y)
        {
            var gameSession = await _gameSessionService.GetGameSession(id);
            await _gameSessionService.AddTurn(gameSession.Id, email, x, y);
            return View("Index", gameSession);
        }
    }
}