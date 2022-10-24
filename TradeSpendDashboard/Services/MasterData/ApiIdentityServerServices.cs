using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Services.MasterData.Interfaces;
using IdentityModel.Client;
using System;
using System.Net.Http;

namespace TradeSpendDashboard.Services.MasterData
{
    public class ApiIdentityServerServices<TModel> : ApiService<TModel>, IApiIdentityServerServices<TModel> where TModel : class
    {
        private readonly string _urlApi = "";
        protected readonly HttpClient _client;

        public ApiIdentityServerServices(HttpClient client, AppHelper app, string urlApi) : base(client, app, urlApi)
        {
            client.BaseAddress = new Uri(app.IdentityServerOptions.Authority);
            client.SetToken("Bearer", app.Token);

            _urlApi = urlApi;
            _client = client;
        }

        public string ExampleMethod()
        {
            return "Ali";
        }
    }
}


