using TradeSpendDashboard.Data.Repository.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TradeSpendDashboard.Helper
{
    public class ReportHelper
    {
        private readonly ILogger<ReportHelper> _logger;
        AppHelper _app;
        //private readonly IRequestsRepository _reqRepo;
        ////private readonly IRequestContractRepository _contractRepo;
        //private readonly IRequestContractDetailRepository _contractDetailRepo;
        ////private readonly IRequestSalesProjectionRepository _forecastRepo;
        //private readonly IRequestSalesProjectionDetailRepository _forecastDetailRepo;
        //private readonly IRequestClaimRepository _claimRepo;
        //private readonly IRequestClaimDetailRepository _claimDetailRepo;


        private static TimeZoneInfo timeZoneId { get { return TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); } }

        public ReportHelper(
            ILogger<ReportHelper> logger,
            AppHelper app
            //IRequestsRepository reqRepo,
            ////IRequestContractRepository contractRepo,
            //IRequestContractDetailRepository contractDetailRepo,
            ////IRequestSalesProjectionRepository forecastRepo,
            //IRequestSalesProjectionDetailRepository forecastDetailRepo,
            //IRequestClaimRepository claimRepo,
            //IRequestClaimDetailRepository claimDetailRepo
        )
        {
            //_reqRepo = reqRepo;
            _logger = logger;
            _app = app;
            ////_contractRepo = contractRepo;
            //_contractDetailRepo = contractDetailRepo;
            ////_forecastRepo = forecastRepo;
            //_forecastDetailRepo = forecastDetailRepo;
            //_claimRepo = claimRepo;
            //_claimDetailRepo = claimDetailRepo;
        }

        public String CalculateReportRanking()
        {
            var conn = _app.ConnectionString();
            using (SqlConnection sqlCon = new SqlConnection(conn))
            {
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlCon;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"exec dbo.CalculateReportRanking";

                    SqlDataReader x = cmd.ExecuteReader();
                    var dataTable = new DataTable();
                    dataTable.Load(x);

                    string JSONString = string.Empty;
                    JSONString = JsonConvert.SerializeObject(dataTable);
                    return JSONString;
                }
            }
        }
    }
}