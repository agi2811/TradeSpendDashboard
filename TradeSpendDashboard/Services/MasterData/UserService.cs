using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO.Identity;
using TradeSpendDashboard.Models.Pagination;
using TradeSpendDashboard.Services.MasterData.interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity.Master;

namespace TradeSpendDashboard.Services.MasterData
{
    public class UserServices : ApiIdentityServerServices<UserData>, IUserServices
    {
        private readonly AppHelper _app;
        //private const string _urlApi = "dna/api/users";

        public UserServices(HttpClient client, AppHelper app) : base(client, app, app.IdentityServerOptions.UrlApiUser)
        {
            _app = app;
        }

        public async Task<UserData> GetUserByUserCodeAsync(string userCode)
        {
            try
            {
                var response = await _client.GetAsync($"{_app.IdentityServerOptions.UrlApiUser}/usercode/" + userCode);
                string content = await response.Content.ReadAsStringAsync();
                var result = JObject.Parse(content).SelectToken("data");
                UserData data = result.ToObject<UserData>();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ApplicationUser>> GetUserByKey(PaginationParam param)
        {

            param.pageSize = 50;
            param.search = param.search ?? "";

            string stringParam = JsonConvert.SerializeObject(param);
            HttpContent httpContent = new StringContent(stringParam, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_app.IdentityServerOptions.UrlApiUser}/", httpContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(content).SelectToken("result");
            List<ApplicationUser> data = new List<ApplicationUser>();

            foreach (var item in result)
            {
                ApplicationUser list = item.ToObject<ApplicationUser>();
                data.Add(list);
            }
            return data;
        }
    }
}