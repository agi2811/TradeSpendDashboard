using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Data.Services.Interface.Transaction
{
    public interface IActualService
    {
        Task<List<dynamic>> GetPrimarySalesActual(string year, string month);
        Task<List<dynamic>> GetSecondarySalesActual(string year, string month);
        Task<List<dynamic>> GetSpendingPhasingActual(string year, string month);
        Task<List<dynamic>> GetYearsPrimarySalesActual();
        Task<List<dynamic>> GetYearsSecondarySalesActual();
        Task<List<dynamic>> GetYearsSpendingPhasingActual();
        ValidationDTO ImportPrimarySales(IList<IFormFile> file, bool isBulk, string year, string month);
        ValidationDTO ImportSecondarySales(IList<IFormFile> file, bool isBulk, string year, string month);
        ValidationDTO InterfaceSpendingPhasing(string year, string month);
    }
}
