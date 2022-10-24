using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity.TradeSpendDashboard.Transaction.Update;
using TradeSpendDashboard.Models.Entity.Transaction;

namespace TradeSpendDashboard.Data.Repository.Interface.Transaction
{
    public interface IUpdateRepository
    {
        Task<List<dynamic>> GetPrimarySalesUpdate(string userLoginID, string year, string month);
        Task<List<dynamic>> GetSecondarySalesUpdate(string userLoginID, string year, string month);
        Task<List<dynamic>> GetSpendingPhasingUpdate(string userLoginID, string year, string month);
        Task<List<dynamic>> GetTempMappingPrimarySalesUpdate(string userLoginID, string year, string month);
        Task<List<dynamic>> GetTempMappingSecondarySalesUpdate(string userLoginID, string year, string month);
        Task<List<dynamic>> GetTempMappingSpendingPhasingUpdate(string userLoginID, string year, string month);
        Task<List<dynamic>> GetYearsPrimarySalesUpdate();
        Task<List<dynamic>> GetYearsSecondarySalesUpdate();
        Task<List<dynamic>> GetYearsSpendingPhasingUpdate();
        Task<TradeHeadPrimarySalesUpdate> GetPrimarySalesHeadUpdate(string year, string month);
        Task<TradeHeadSecondarySalesUpdate> GetSecondarySalesHeadUpdate(string year, string month);
        Task<TradeHeadSpendingUpdate> GetSpendingPhasingHeadUpdate(string year, string month);
        Task<TradeHeadPrimarySalesUpdate> AddPrimarySalesHeadUpdate(TradeHeadPrimarySalesUpdate entity);
        Task<TradeHeadSecondarySalesUpdate> AddSecondarySalesHeadUpdate(TradeHeadSecondarySalesUpdate entity);
        Task<TradeHeadSpendingUpdate> AddSpendingPhasingHeadUpdate(TradeHeadSpendingUpdate entity);
        Task<TradeHeadPrimarySalesUpdate> UpdatePrimarySalesHeadUpdate(TradeHeadPrimarySalesUpdate entity);
        Task<TradeHeadSecondarySalesUpdate> UpdateSecondarySalesHeadUpdate(TradeHeadSecondarySalesUpdate entity);
        Task<TradeHeadSpendingUpdate> UpdateSpendingPhasingHeadUpdate(TradeHeadSpendingUpdate entity);
        Task<dynamic> CheckPrimarySalesHeadUpdate(string year, string month);
        Task<dynamic> CheckSecondarySalesHeadUpdate(string year, string month);
        Task<dynamic> CheckSpendingPhasingHeadUpdate(string year, string month);
        List<dynamic> TruncateTempMappingPrimarySales(string usercode, string year, string month);
        List<dynamic> TruncateTempMappingSecondarySales(string usercode, string year, string month);
        List<dynamic> TruncateTempMappingSpendingPhasing(string usercode, string year, string month);
        List<ErrorMessage> SpImportPrimarySalesUpdate(string usercode, string filename, string year, string month);
        List<ErrorMessage> SpImportSecondarySalesUpdate(string usercode, string filename, string year, string month);
        List<ErrorMessage> SpImportSpendingPhasingUpdate(string usercode, string filename, string year, string month);
        Task<List<dynamic>> DownloadSpendingPhasingUpdate(string userLoginID, string year, string month, string budgetOwner, List<string> categoryList, List<string> profitCenterList);
    }
}
