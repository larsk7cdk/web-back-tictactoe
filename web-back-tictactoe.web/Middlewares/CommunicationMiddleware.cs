﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using web_back_tictactoe.web.Services;

namespace TicTacToe.Middlewares
{
    public class CommunicationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;

        public CommunicationMiddleware(RequestDelegate next, IUserService userService)
        {
            _next = next;
            _userService = userService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Equals("/CheckEmailConfirmationStatus"))
                await ProcesEmailConfirmationStatus(context);
            else
                await _next.Invoke(context);
        }

        private async Task ProcesEmailConfirmationStatus(HttpContext context)
        {
            var email = context.Request.Query["email"];
            var user = _userService.GetUserByEmail(email).Result;


            if (string.IsNullOrEmpty(email))
            {
                await context.Response.WriteAsync("BadRequest:Email is required!");
            }
            else if ((await _userService.GetUserByEmail(email)).IsEmailConfirmed)
            {
                await context.Response.WriteAsync("OK");
            }
            else
            {
                await context.Response.WriteAsync("WaitingForEmailConfirmation");
                user.IsEmailConfirmed = true;
                user.EmailConfirmationDate = DateTime.Now;
                _userService.UpdateUser(user).Wait();
            }
        }
    }
}