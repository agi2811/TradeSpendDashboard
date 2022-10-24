using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Data.Services.Interface.Transaction
{
    public interface IUploadService
    {
        Task<List<dynamic>> GetPrimarySalesUpload(string year, string month);
        Task<List<dynamic>> GetSecondarySalesUpload(string year, string month);
        Task<List<dynamic>> GetSpendingPhasingUpload(string year, string month);
        Task<List<dynamic>> GetYearsPrimarySalesUpload();
        Task<List<dynamic>> GetYearsSecondarySalesUpload();
        Task<List<dynamic>> GetYearsSpendingPhasingUpload();
        Task<dynamic> CheckPrimarySalesHeadUpload(string year, string month);
        Task<dynamic> CheckSecondarySalesHeadUpload(string year, string month);
        Task<dynamic> CheckSpendingPhasingHeadUpload(string year, string month);
        Task<long> CreatePrimarySalesHeadUpload(string userCode, string year, string month);
        Task<long> CreateSecondarySalesHeadUpload(string userCode, string year, string month);
        Task<long> CreateSpendingPhasingHeadUpload(string userCode, string year, string month);
        Task<long> UpdatePrimarySalesHeadUpload(string userCode, string year, string month, bool isLock = false);
        Task<long> UpdateSecondarySalesHeadUpload(string userCode, string year, string month, bool isLock = false);
        Task<long> UpdateSpendingPhasingHeadUpload(string userCode, string year, string month, bool isLock = false);
        ValidationDTO ImportPrimarySales(IList<IFormFile> file, bool isBulk, string year, string month);
        ValidationDTO ImportSecondarySales(IList<IFormFile> file, bool isBulk, string year, string month);
        ValidationDTO ImportSpendingPhasing(IList<IFormFile> file, bool isBulk, string year, string month, string budgetOwner, List<string> categoryList, List<string> profitCenterList);
    }
}
