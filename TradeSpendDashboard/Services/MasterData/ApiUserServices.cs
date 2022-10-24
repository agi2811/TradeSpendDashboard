using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO.Identity;
using TradeSpendDashboard.Services.MasterData.interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Services.MasterData
{
    public class ApiUserServices : ApiIdentityServerServices<UserData>, IApiUserServices
    {
        private const string _urlApi = "users";
        public ApiUserServices(HttpClient client, AppHelper app) : base(client, app, _urlApi) { }

        public async Task<UserData> GetUserByUserCodeAsync(string userCode)
        {
            try
            {
                var response = await _client.GetAsync($"{_urlApi}/usercode/" + userCode);
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
    }
}