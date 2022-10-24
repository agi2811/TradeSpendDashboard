using TradeSpendDashboard.Model.AppSettings;
using TradeSpendDashboard.Models.AppSettings;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Helper
{
    public class HttpClientHelper
    {
        public readonly Application _app;
        public IdentityServerOptions _identityServerOpt;
        public static string clientId = "ProductReturn";
        public static string clientSecret = "P@ssw0rd123!";
        public static string authority = "https://devapps.frisianflag.co.id/dna";

        public HttpClientHelper(
            IOptions<Application> application,
            IOptions<IdentityServerOptions> identityServerOptions
        )
        {
            _app = application.Value;
            _identityServerOpt = identityServerOptions.Value;
        }

        public static async Task<HttpClient> GetCient()
        {
            var settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var application = settings.GetSection("Application");
            var IdentityServerOptions = settings.GetSection("IdentityServerOptions");
            var authority = IdentityServerOptions["Authority"];
            var clientId = IdentityServerOptions["ClientId"];
            var clientSecret = IdentityServerOptions["ClientSecret"];

            var _client = new HttpClient();
            try
            {

                using (var clientAuht = new HttpClient())
                {
                    var disco = await clientAuht.GetDiscoveryDocumentAsync(authority);

                    if (disco.IsError)
                    {
                        throw new Exception(disco.Error);
                    }

                    var response = await clientAuht.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                    {
                        Address = disco.TokenEndpoint,
                        ClientId = clientId,
                        ClientSecret = clientSecret
                    });

                    if (response.IsError)
                    {
                        throw new Exception(response.Error);
                    }
                    _client.SetBearerToken(response.AccessToken);
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken);
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _client;
        }

        public static async Task<HttpClient> GetCient_old()
        {
            var _client = new HttpClient();
            try
            {
                using (var clientAuht = new HttpClient())
                {
                    var disco = await clientAuht.GetDiscoveryDocumentAsync("https://devapps.frisianflag.co.id/dna");

                    var id = "atc";
                    var secret = "P@ssw0rd123!";
                    var scope = "IdentityServerApi,MasterData.read_only,openid,email,profile,DMSInvoice.read_only";
                    var admin = "admin";
                    var passadmin = "P@ssw0rd123!";

                    if (disco.IsError)
                    {
                        throw new Exception(disco.Error);
                    }

                    PasswordTokenRequest tokenRequest = new PasswordTokenRequest();
                    tokenRequest.Address = disco.TokenEndpoint;
                    tokenRequest.ClientId = id;
                    tokenRequest.ClientSecret = secret;
                    tokenRequest.GrantType = "password";

                    tokenRequest.UserName = admin;
                    tokenRequest.Password = passadmin;
                    tokenRequest.Method = HttpMethod.Post;

                    var response = await clientAuht.RequestPasswordTokenAsync(tokenRequest);

                    if (response.IsError)
                    {
                        throw new Exception(response.Error);
                    }
                    _client.SetBearerToken(response.AccessToken);
                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", response.AccessToken);
                    _client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _client;
        }
    }
}