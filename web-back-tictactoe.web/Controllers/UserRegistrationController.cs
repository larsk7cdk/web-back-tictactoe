using Microsoft.AspNetCore.Mvc;

namespace web_back_tictactoe.web.Controllers
{
    public class UserRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}