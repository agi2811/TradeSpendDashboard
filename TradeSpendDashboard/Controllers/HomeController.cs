using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        AppHelper _app;
        IWebHostEnvironment environment;
        IMasterMenuService MasterMenu;

        public HomeController(
            ILogger<HomeController> logger, 
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu
        ) : base(logger, app, environment, masterMenu)
        {
            _logger = logger;
            _app = app;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task Logout()
        {
            RemoveCookies("RoleId");
            RemoveCookies("RoleName");
            await HttpContext.SignOutAsync("Cookies");
            var prop = new AuthenticationProperties()
            {
                RedirectUri = "Home/Index"
            };
            await HttpContext.SignOutAsync("oidc", prop);
        }
    }
}
