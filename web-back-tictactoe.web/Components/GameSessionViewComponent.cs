﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_back_tictactoe.web.Services;

namespace web_back_tictactoe.web.Components
{
    [ViewComponent(Name = "GameSession")]
    public class GameSessionViewComponent : ViewComponent
    {
        private readonly IGameSessionService _gameSessionService;

        public GameSessionViewComponent(IGameSessionService gameSessionService)
        {
            _gameSessionService = gameSessionService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid gameSessionId)
        {
            var session = await _gameSessionService.GetGameSession(gameSessionId);
            return View(session);
        }
    }
}