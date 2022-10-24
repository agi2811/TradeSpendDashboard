using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Model.AppSettings;
using TradeSpendDashboard.Models.AppSettings;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Helper
{
    public class AppHelper
    {
        private readonly ILogger<AppHelper> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMasterUsersRoleRepository _userRepo;
        private readonly IMasterRoleRepository _roleRepo;
        private readonly IHttpContextAccessor _accessor;

        public readonly Application Application;
        public readonly IdentityServerOptions IdentityServerOptions;

        private string _token { get; set; }

        public AppHelper(
            ILogger<AppHelper> logger,
            IHttpContextAccessor httpContextAccessor,
            IHttpContextAccessor accessor,
            IMasterUsersRoleRepository userRepo,
            IMasterRoleRepository roleRepo,
            IOptions<Application> application,
            IOptions<IdentityServerOptions> identityServerOptions)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _accessor = accessor;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            Application = application.Value;
            IdentityServerOptions = identityServerOptions.Value;
        }

        public string ConnectionString()
        {
            return Application.ConnectionStrings;
        }

        public IEnumerable<Claim> Claims { get { return _httpContextAccessor.HttpContext.User.Claims; } }

        public string UserName { get { return Claims.Where(w => w.Type.Equals("user_code")).FirstOrDefault().Value; } }

        public string Email { get { return Claims.Where(w => w.Type.Equals(JwtClaimTypes.Email)).FirstOrDefault().Value; } }

        public string FullName { get { return Claims.Where(w => w.Type.Equals(JwtClaimTypes.Name)).FirstOrDefault().Value; } }

        public string Token { get { return Claims.Where(w => w.Type.Equals("access_token")).FirstOrDefault().Value; } }

        public string RoleId
        {
            get
            {
                try
                {
                    return Claims.Where(w => w.Type.Equals("RoleId")).FirstOrDefault().Value;
                }
                catch (Exception err)
                {
                    return "0";
                    throw;
                }
            }
        }

        public string receiver { get { return Application.Receiver; } }

        public string ccEmail { get { return Application.CcEmail; } }

        public string RoleName()
        {
            var RoleIdData = this.RoleId ?? "0";
            var data = Convert.ToInt64(RoleIdData);
            if (data != 0)
            {
                var dataRole = _roleRepo.GetById(data);
                if (dataRole != null)
                {
                    return dataRole.Name;
                }
            }
            return "";
        }

        public async Task<string> getToken()
        {
            //if (string.IsNullOrWhiteSpace(_token))
            //    await generateToken();

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var jwtSecurityToken = tokenHandler.ReadJwtToken(_token);

            //if (jwtSecurityToken.ValidTo < DateTime.UtcNow.AddSeconds(10))
            //    await generateToken();

            return Token;
        }

        private async Task<string> getBaseUrl()
        {
            var baseUrl = Application.BaseUrl;
            return baseUrl;
        }

        private async Task generateToken()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims.ToList();
            _token = claims.Find(x => x.Type == "access_token").Value;
        }

        public string UploadStagingDir { get { return Application.UploadStagingDir; } }

        public string UploadTargetDir { get { return Application.UploadTargetDir; } }

        public string BaseUrl { get { return Application.BaseUrl; } }
    }
}
