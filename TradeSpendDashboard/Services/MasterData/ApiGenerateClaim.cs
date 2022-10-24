using TradeSpendDashboard.Model.AppSettings;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Services.MasterData
{
    public class ApiGenerateClaim
    {
        private const string _urlApi = "invoice-detail1/pageable-params";
        private readonly int limit = 10000;
        private readonly IMapper _mapperClaim;
        public readonly Application _app;
        private readonly string _conn;
        private readonly string _tableheader = "RequestClaim";
        private readonly string _table = "TempClaimDetail";
        private dynamic headerDataclaim;

        public ApiGenerateClaim(HttpClient client, IOptions<Application> application)
        {
            _app = application.Value;
            _conn = _app.ConnectionStrings;
            client.BaseAddress = new Uri("https://mmd.frisianflag.co.id:446/api/");

        }

        public async Task<dynamic> Getdatagenerate(DateTime startDate)
        {
            try
            {
                TruncateTable();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("error get Claim Header.", ex);
            }
        }
        public void TruncateTable()
        {
            using (SqlConnection sqlCon = new SqlConnection(_conn))
            {
                sqlCon.Open();
                var sql = @"truncate table " + _table;
                using (SqlCommand command = new SqlCommand(sql, sqlCon))
                {
                    command.ExecuteScalar();
                    Console.WriteLine(command);
                }
            }
        }
        public void InsertTable(string startDate, string endDate)
        {
            using (SqlConnection sqlCon = new SqlConnection(_conn))
            {
                sqlCon.Open();
                var sql = @"exec [dbo].[SP_Insert_STGClaimDetail] " + "'" + startDate + "'" + "," + "'" + endDate + "'";
                using (SqlCommand command = new SqlCommand(sql, sqlCon))
                {
                    command.ExecuteScalar();
                    Console.WriteLine(command);
                }
            }
        }

        public void InsertTableClaim(string startDate, string endDate)
        {
            using (SqlConnection sqlCon = new SqlConnection(_conn))
            {
                sqlCon.Open();
                var sql = @"exec [dbo].[SP_Insert_ClaimDetail] " + "'" + startDate + "'" + "," + "'" + endDate + "'";
                using (SqlCommand command = new SqlCommand(sql, sqlCon))
                {
                    command.ExecuteScalar();
                    Console.WriteLine(command);
                }
            }
        }
    }
}


