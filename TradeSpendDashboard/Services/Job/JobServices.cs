using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Services.Job.interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Services.Job
{
    public class JobServices : IJobServices
    {
        private const string _urlApi = "dms-product-price/pageable";
        private readonly AppHelper _app;
        private readonly int limit = 10000;
        private readonly string _conn;
        //private readonly IHierarchyProductRepository _productRepo;
        //private readonly IMasterProductGroupDetailRepository _productDetailRepo;

        public JobServices(HttpClient client, AppHelper app)
        {
            _app = app;
            _conn = app.Application.ConnectionStrings;
            client.BaseAddress = new Uri(app.Application.HostMasterData);
        }

        public async Task<string> CallAPI()
        {
            var content = "";
            using (var client = new HttpClient())
            {
                var baseUrl = "https://mmd.frisianflag.co.id/api/business/role-access";
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                var Res = await client.GetAsync(baseUrl);
                if (Res.IsSuccessStatusCode)
                {
                    content = Res.Content.ReadAsStringAsync().Result;
                }
            }
            return content;
        }

        public string Test()
        {
            var content = "Ali Mutasal";
            return content;
        }
    }
}