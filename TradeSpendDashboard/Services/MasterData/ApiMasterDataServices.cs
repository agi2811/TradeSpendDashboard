using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Services.MasterData.Interfaces;
using System;
using System.Net.Http;

namespace TradeSpendDashboard.Services.MasterData
{
    public class ApiMasterDataServices<TModel> : ApiService<TModel>, IApiMasterDataServices<TModel> where TModel : class
    {
        protected readonly HttpClient _client;

        public ApiMasterDataServices(HttpClient client, AppHelper app, string urlApi) : base(client, app, urlApi)
        {
            client.BaseAddress = new Uri(app.Application.HostMasterData);
        }
    }
}