using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity.Transaction;

namespace TradeSpendDashboard.Data.Repository.Interface.Transaction
{
    public interface IActualRepository
    {
        Task<List<dynamic>> GetPrimarySalesActual(string userLoginID, string year, string month);
        Task<List<dynamic>> GetSecondarySalesActual(string userLoginID, string year, string month);
        Task<List<dynamic>> GetSpendingPhasingActual(string userLoginID, string year, string month);
        Task<List<dynamic>> GetTempMappingPrimarySalesActual(string userLoginID, string year, string month);
        Task<List<dynamic>> GetTempMappingSecondarySalesActual(string userLoginID, string year, string month);
        Task<List<dynamic>> GetTempMappingSpendingPhasingActual(string userLoginID, string year, string month);
        Task<List<dynamic>> GetYearsPrimarySalesActual();
        Task<List<dynamic>> GetYearsSecondarySalesActual();
        Task<List<dynamic>> GetYearsSpendingPhasingActual();
        List<dynamic> TruncateTempMappingPrimarySales(string usercode, string year, string month);
        List<dynamic> TruncateTempMappingSecondarySales(string usercode, string year, string month);
        List<ErrorMessage> SpImportPrimarySalesActual(string usercode, string filename, string year, string month);
        List<ErrorMessage> SpImportSecondarySalesActual(string usercode, string filename, string year, string month);
        List<ErrorMessage> SpInsertSpendingPhasingActual(string usercode, string year, string month, int plus);
        List<ErrorMessage> SpInterfaceSpendingPhasingActual(string usercode, string year, string month);
    }
}
