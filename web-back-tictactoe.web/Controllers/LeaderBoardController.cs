﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_back_tictactoe.web.Services;

namespace web_back_tictactoe.web.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly IUserService _userService;

        public LeaderboardController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetTopUsers(10);
            return View(users);
        }
    }
}