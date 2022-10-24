using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity.TradeSpendDashboard.Transaction.Upload;
using TradeSpendDashboard.Models.Entity.Transaction;

namespace TradeSpendDashboard.Data.Repository.Interface.Transaction
{
    public interface IUploadRepository
    {
        Task<List<dynamic>> GetPrimarySalesUpload(string userLoginID, string year, string month);
        Task<List<dynamic>> GetSecondarySalesUpload(string userLoginID, string year, string month);
        Task<List<dynamic>> GetSpendingPhasingUpload(string userLoginID, string year, string month);
        Task<List<dynamic>> GetTempMappingPrimarySalesUpload(string userLoginID, string year, string month);
        Task<List<dynamic>> GetTempMappingSecondarySalesUpload(string userLoginID, string year, string month);
        Task<List<dynamic>> GetTempMappingSpendingPhasingUpload(string userLoginID, string year, string month);
        Task<List<dynamic>> GetYearsPrimarySalesUpload();
        Task<List<dynamic>> GetYearsSecondarySalesUpload();
        Task<List<dynamic>> GetYearsSpendingPhasingUpload();
        Task<TradeHeadPrimarySalesOutlook> GetPrimarySalesHeadUpload(string year, string month);
        Task<TradeHeadSecondarySalesOutlook> GetSecondarySalesHeadUpload(string year, string month);
        Task<TradeHeadSpendingOutlook> GetSpendingPhasingHeadUpload(string year, string month);
        Task<TradeHeadPrimarySalesOutlook> AddPrimarySalesHeadUpload(TradeHeadPrimarySalesOutlook entity);
        Task<TradeHeadSecondarySalesOutlook> AddSecondarySalesHeadUpload(TradeHeadSecondarySalesOutlook entity);
        Task<TradeHeadSpendingOutlook> AddSpendingPhasingHeadUpload(TradeHeadSpendingOutlook entity);
        Task<TradeHeadPrimarySalesOutlook> UpdatePrimarySalesHeadUpload(TradeHeadPrimarySalesOutlook entity);
        Task<TradeHeadSecondarySalesOutlook> UpdateSecondarySalesHeadUpload(TradeHeadSecondarySalesOutlook entity);
        Task<TradeHeadSpendingOutlook> UpdateSpendingPhasingHeadUpload(TradeHeadSpendingOutlook entity);
        Task<dynamic> CheckPrimarySalesHeadUpload(string year, string month);
        Task<dynamic> CheckSecondarySalesHeadUpload(string year, string month);
        Task<dynamic> CheckSpendingPhasingHeadUpload(string year, string month);
        List<dynamic> TruncateTempMappingPrimarySales(string usercode, string year, string month);
        List<dynamic> TruncateTempMappingSecondarySales(string usercode, string year, string month);
        List<dynamic> TruncateTempMappingSpendingPhasing(string usercode, string year, string month);
        List<ErrorMessage> SpImportPrimarySalesUpload(string usercode, string filename, string year, string month);
        List<ErrorMessage> SpImportSecondarySalesUpload(string usercode, string filename, string year, string month);
        List<ErrorMessage> SpImportSpendingPhasingUpload(string usercode, string filename, string year, string month);       
    }
}
