using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.Entity.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly ILogger<BaseController> _logger;
        AppHelper _app;
        IWebHostEnvironment _environment;
        IMasterUsersRoleService _userRoleService;
        IMasterMenuService _menuService;
        IMasterRoleService _roleService;

        public BaseController(
            ILogger<BaseController> logger,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu
        )
        {
            _logger = logger;
            _app = app;
            _environment = environment;
            _menuService = masterMenu;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var roleId = Convert.ToInt64(_app.RoleId);
            ViewBag.Menu = _menuService.GetMenuByRole(roleId);
            ViewBag.AppName = _app != null ? _app.Application.Name : "Trade Spend";
            ViewBag.Creator = _app.UserName ?? "-";
            ViewBag.Fullname = _app.FullName ?? "-";
            ViewBag.BaseUrl = _app.Application.BaseUrl ?? "";
            ViewBag.Email = _app.Email ?? "-";

            // Cek Apakah sudah ada Role Id Atau belum
            ViewBag.RoleId = _app.RoleId;
            ViewBag.RoleName = _app.RoleName();
            base.OnActionExecuted(context);
        }

        public MasterRole SetCookieAsync(string userCode)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Append("RoleId", _app.RoleId, option);
            Response.Cookies.Append("RoleName", _app.RoleName().ToString(), option);

            var dataMasterRole = new MasterRole();
            dataMasterRole.Id = Convert.ToInt64(_app.RoleId);
            dataMasterRole.Name = _app.RoleName();
            return dataMasterRole;
        }

        public string GetCookie(string key)
        {
            return Request.Cookies[key];
        }

        public void RemoveCookies(string key)
        {
            Response.Cookies.Delete(key);
        }

        public RedirectResult Unauthorize()
        {
            return Redirect("Error/Unauthorized");
        }

        public async Task<string> UploadedFile(IList<IFormFile> files, string dir, string defaultFileNamexlsx)
        {
            try
            {
                string strFiles = "";
                var fullPath = "";
                if (files.Count > 0)
                {

                    var file = files[0];
                    if (file != null && file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        strFiles = fileName.ToString();

                        // Get Mime Type
                        var ext = Path.GetExtension(fileName);
                        if (ext == ".xlsx")
                        {
                            //var path = @"E:\Upload\" + dir;
                            var path = @"D:\SCRS_SOURCE\Upload\" + dir;
                            bool isExists = System.IO.Directory.Exists(path);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(path);

                            fullPath = Path.Combine(path, defaultFileNamexlsx + "_" + _app.UserName + "_" + DateTime.Now.ToString("YYYYMMDD") + ".xlsx");

                            //string currentPath = Request.MapPath(fullPath);

                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }

                            using (FileStream output = System.IO.File.Create(fullPath))
                            {
                                await file.CopyToAsync(output);
                            }
                        }
                    }
                }
                return fullPath;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string filename)
        {
            return _environment.WebRootPath + "\\files\\" + filename;
        }
    }
}
