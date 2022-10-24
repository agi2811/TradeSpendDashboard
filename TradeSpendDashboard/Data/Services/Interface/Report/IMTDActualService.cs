using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Data.Services.Interface.Report
{
    public interface IMTDActualService
    {
        Task<List<dynamic>> FN_Get_MTD_Actual_ProfitCenter(string year, string month);
    }
}
