using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace web_back_tictactoe.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var culture = Request.HttpContext.Session.GetString("culture");
            ViewBag.Language = culture;
            return View();
        }

        public IActionResult SetCulture(string culture)
        {
            HttpContext.Session.SetString("culture", culture);
            return RedirectToAction("Index");
        }
    }
}