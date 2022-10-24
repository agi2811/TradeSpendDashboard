using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Models;
using TradeSpendDashboard.Models.Entity.TradeSpendDashboard.Transaction.Upload;
using TradeSpendDashboard.Models.Entity.Transaction;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IReportRepository
    {
        Task<List<dynamic>> FN_Get_MTD_Actual_ProfitCenter(string year, string month,long snapshotID);
        Task<List<dynamic>> FN_Get_MTD_Outlook_ProfitCenter(string year, string month, long snapshotID);
        Task<List<dynamic>> FN_Get_MTD_Budget_ProfitCenter(string year,long snapshotID);
        Task<List<dynamic>> FN_Get_MTD_Variance_OutlookActual(string year, string month, string yearOutlook, string monthOutlook, long snapshotID);
        Task<List<dynamic>> FN_Get_MTD_Variance_BudgetActual(string year, string month, string yearBudget, long snapshotID);
        Task<List<dynamic>> spGetMTDActualSummaryOutlook(string year, string month, string yearOutlook, string monthOutlook, long snapshotID);
        Task<List<dynamic>> spGetMTDActualSummaryBudget(string year, string month, string yearBudget, long snapshotID);
        Task<List<dynamic>> spGetFC_vs_Budget(string profitCenter, string source1, string source2, string source1Year, string source1Month, string source2Year, string source2month);
        Task<List<dynamic>> fnGetMTDColNameList(string profitCenter, string source1, string source2);
        Task<string> spSnapshotMTDActual(SnapshotParams param);
        Task<List<dynamic>> fnGetSnapshotHistory();
    }
}
