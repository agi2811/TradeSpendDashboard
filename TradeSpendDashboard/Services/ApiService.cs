using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.Pagination;
using TradeSpendDashboard.Services.ExceptionHandler;
using AutoMapper;
using IdentityModel.Client;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Services
{
    public class ApiService<TModel> : IApiService<TModel> where TModel : class
    {
        private readonly string _urlApi = "";
        protected readonly HttpClient _client;
        private HttpClient client;
        private AppHelper app;
        //private IRequestsService reqService;
        private IMapper mapperRequest;
        private string urlApi;

        public ApiService(HttpClient client, AppHelper app, string urlApi)
        {
            client.SetToken("Bearer", app.getToken().Result);
            _urlApi = urlApi;
            _client = client;
        }

        public ApiService(HttpClient client, AppHelper app, IMapper mapperRequest, string urlApi) //IRequestsService reqService
        {
            this.client = client;
            this.app = app;
            //this.reqService = reqService;
            //this.repository = repository;
            this.mapperRequest = mapperRequest;
            this.urlApi = urlApi;
        }

        public async Task<RequestResponse<TModel>> GetAsync()
        {
            var response = await _client.GetAsync($"{_urlApi}/");

            if (!response.IsSuccessStatusCode)
            {
                var responseException = new ResponseException(response);

                return ProtocolResponse.FromException<RequestResponse<TModel>>(responseException.Exception);
            }

            return await ProtocolResponse.FromHttpResponseAsync<RequestResponse<TModel>>(response).ConfigureAwait(true);
        }

        public async Task<RequestResponse<TModel>> GetAsync(long id)
        {
            var response = await _client.GetAsync($"{_urlApi}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var responseException = new ResponseException(response);

                return ProtocolResponse.FromException<RequestResponse<TModel>>(responseException.Exception);
            }

            return await ProtocolResponse.FromHttpResponseAsync<RequestResponse<TModel>>(response).ConfigureAwait(true);
        }

        public async Task<PaginatedResponse<TModel>> GetAsync(PaginationParam paginationParam)
        {
            string param = JsonConvert.SerializeObject(paginationParam);

            HttpContent httpContent = new StringContent(param, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_urlApi}/pageable", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                var responseException = new ResponseException(response);

                return ProtocolResponse.FromException<PaginatedResponse<TModel>>(responseException.Exception);
            }

            return await ProtocolResponse.FromHttpResponseAsync<PaginatedResponse<TModel>>(response).ConfigureAwait(true);
        }

        public async Task<RequestResponse<TModel>> PostAsync(TModel model)
        {
            string param = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(param, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_urlApi, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                var responseException = new ResponseException(response);

                return ProtocolResponse.FromException<RequestResponse<TModel>>(responseException.Exception);
            }

            return await ProtocolResponse.FromHttpResponseAsync<RequestResponse<TModel>>(response).ConfigureAwait(true);
        }

        public async Task<RequestResponse<TModel>> PutAsync(long id, TModel model)
        {
            string param = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(param, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"{_urlApi}/{id}", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                var responseException = new ResponseException(response);

                return ProtocolResponse.FromException<RequestResponse<TModel>>(responseException.Exception);
            }

            return await ProtocolResponse.FromHttpResponseAsync<RequestResponse<TModel>>(response).ConfigureAwait(true);
        }

        public async Task<RequestResponse<TModel>> PutUserAsync(string id, TModel model)
        {
            string param = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(param, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"{_urlApi}/{id}", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                var responseException = new ResponseException(response);

                return ProtocolResponse.FromException<RequestResponse<TModel>>(responseException.Exception);
            }

            return await ProtocolResponse.FromHttpResponseAsync<RequestResponse<TModel>>(response).ConfigureAwait(true);
        }

        public async Task<RequestResponse<TModel>> DeleteAsync(long id)
        {
            var response = await _client.DeleteAsync($"{_urlApi}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var responseException = new ResponseException(response);

                return ProtocolResponse.FromException<RequestResponse<TModel>>(responseException.Exception);
            }

            return await ProtocolResponse.FromHttpResponseAsync<RequestResponse<TModel>>(response).ConfigureAwait(true);
        }
    }
}