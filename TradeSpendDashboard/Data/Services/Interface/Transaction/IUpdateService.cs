using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Data.Services.Interface.Transaction
{
    public interface IUpdateService
    {
        Task<List<dynamic>> GetPrimarySalesUpdate(string year, string month);
        Task<List<dynamic>> GetSecondarySalesUpdate(string year, string month);
        Task<List<dynamic>> GetSpendingPhasingUpdate(string year, string month);
        Task<List<dynamic>> GetYearsPrimarySalesUpdate();
        Task<List<dynamic>> GetYearsSecondarySalesUpdate();
        Task<List<dynamic>> GetYearsSpendingPhasingUpdate();
        Task<dynamic> CheckPrimarySalesHeadUpdate(string year, string month);
        Task<dynamic> CheckSecondarySalesHeadUpdate(string year, string month);
        Task<dynamic> CheckSpendingPhasingHeadUpdate(string year, string month);
        Task<long> CreatePrimarySalesHeadUpdate(string userCode, string year, string month);
        Task<long> CreateSecondarySalesHeadUpdate(string userCode, string year, string month);
        Task<long> CreateSpendingPhasingHeadUpdate(string userCode, string year, string month);
        Task<long> UpdatePrimarySalesHeadUpdate(string userCode, string year, string month, bool isLock = false);
        Task<long> UpdateSecondarySalesHeadUpdate(string userCode, string year, string month, bool isLock = false);
        Task<long> UpdateSpendingPhasingHeadUpdate(string userCode, string year, string month, bool isLock = false);
        ValidationDTO ImportPrimarySales(IList<IFormFile> file, bool isBulk, string year, string month);
        ValidationDTO ImportSecondarySales(IList<IFormFile> file, bool isBulk, string year, string month);
        ValidationDTO ImportSpendingPhasing(IList<IFormFile> file, bool isBulk, string year, string month, string budgetOwner, List<string> categoryList, List<string> profitCenterList);
        Task<List<dynamic>> DownloadSpendingPhasingUpdate(string year, string month, string budgetOwner, List<string> categoryList, List<string> profitCenterList);
    }
}
