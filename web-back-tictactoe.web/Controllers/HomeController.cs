using Microsoft.AspNetCore.Mvc;

namespace web_back_tictactoe.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}