﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using web_back_tictactoe.web.Models;
using web_back_tictactoe.web.Services;

namespace web_back_tictactoe.web.Controllers
{
    public class UserRegistrationController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<UserRegistrationController> _logger;
        private readonly IUserService _userService;

        public UserRegistrationController(IUserService userService, IEmailService emailService,
            ILogger<UserRegistrationController> logger)
        {
            _userService = userService;
            _emailService = emailService;
            _logger = logger;
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
                return RedirectToAction(nameof(EmailConfirmation), new {userModel.Email});

                //return Content($"User {userModel.FirstName} {userModel.LastName} has been registered sucessfully");
            }

            return View(userModel);
        }

        [HttpGet]
        public async Task<IActionResult> EmailConfirmation(string email)
        {
            _logger.LogInformation($"##Start## Email confirmation process for {email}");
            var user = await _userService.GetUserByEmail(email);
            var urlAction = new UrlActionContext
            {
                Controller = "UserRegistration",
                Action = "ConfirmEmail",
                Values = new {email},
                Host = Request.Host.ToString(),
                Protocol = Request.Scheme
            };

            var message =
                $"Thank you for your registration on our web site, please click here to confirm your email {Url.Action(urlAction)}";

            try
            {
                _emailService.SendEmail(email, "Tic-Tac-Toe Email Confirmation", message).Wait();
            }
            catch (Exception e)
            {
            }

            if (user?.IsEmailConfirmed == true) return RedirectToAction("Index", "GameInvitation", new {email});

            ViewBag.Email = email;
            //user.IsEmailConfirmed = true;
            //user.EmailConfirmationDate = DateTime.Now;
            //await _userService.UpdateUser(user);

            return View();
        }
    }
}