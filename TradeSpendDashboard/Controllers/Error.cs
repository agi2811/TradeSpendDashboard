using Microsoft.AspNetCore.Mvc;

namespace TradeSpendDashboard.Controllers
{
    public class Error : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Unauthorized()
        {
            return View("~/Views/Shared/_Unauth.cshtml");
        }
    }
}
