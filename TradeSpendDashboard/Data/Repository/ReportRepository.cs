using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Repository.Interface.Transaction;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.TradeSpendDashboard.Transaction.Upload;
using TradeSpendDashboard.Models.Entity.Transaction;

namespace TradeSpendDashboard.Data.Repository
{
    public class ReportRepository : BaseRepository<Entity>, IReportRepository
    {
        private readonly AppHelper appHelper;

        public ReportRepository(TradeSpendDashboardContext context, AppHelper appHelper) : base(context)
        {
            this.appHelper = appHelper;
        }

        public async Task<List<dynamic>> FN_Get_MTD_Actual_ProfitCenter(string year, string month, long snapshotID)
        {
            var param = new Dictionary<string, object>();

            if (snapshotID == 0)
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_MTD_Actual_ProfitCenter]( '{year}', '{month}')", param).ToList();
                return dataDynamic;
            }
            else
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"select * from dbo.FN_Get_MTD_Actual_ProfitCenter_Snapshot( '{snapshotID}')", param).ToList();
                return dataDynamic;
            }


        }

        public async Task<List<dynamic>> FN_Get_MTD_Budget_ProfitCenter(string year, long snapshotID)
        {
            var param = new Dictionary<string, object>();


            if (snapshotID == 0)
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_MTD_Budget_ProfitCenter]( '{year}')", param).ToList();
                return dataDynamic;
            }
            else
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"select * from dbo.FN_Get_MTD_Budget_ProfitCenter_Snapshot( '{snapshotID}')", param).ToList();
                return dataDynamic;
            }
        }

        public async Task<List<dynamic>> FN_Get_MTD_Outlook_ProfitCenter(string year, string month, long snapshotID)
        {
            var param = new Dictionary<string, object>();


            if (snapshotID == 0)
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_MTD_Outlook_ProfitCenter]( '{year}', '{month}')", param).ToList();
                return dataDynamic;
            }
            else
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"select * from dbo.FN_Get_MTD_Outlook_ProfitCenter_Snapshot('{snapshotID}')", param).ToList();
                return dataDynamic;
            }
        }

        public async Task<List<dynamic>> FN_Get_MTD_Variance_BudgetActual(string year, string month, string yearBudget, long snapshotID)
        {
            var param = new Dictionary<string, object>();


            if (snapshotID == 0)
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_MTD_Variance_BudgetActual]( '{year}', '{month}','{yearBudget}')", param).ToList();
                return dataDynamic;
            }
            else
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"select * from dbo.[FN_Get_MTD_Variance_BudgetActual_Snapshot]( '{snapshotID}')", param).ToList();
                return dataDynamic;
            }


        }

        public async Task<List<dynamic>> FN_Get_MTD_Variance_OutlookActual(string year, string month, string yearOutlook, string monthOutlook, long snapshotID)
        {
            var param = new Dictionary<string, object>();


            if (snapshotID == 0)
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_MTD_Variance_OutlookActual]( '{year}', '{month}','{yearOutlook}', '{monthOutlook}')", param).ToList();

                return dataDynamic;
            }
            else
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"select * from dbo.[FN_Get_MTD_Variance_OutlookActual_Snapshot]( '{snapshotID}')", param).ToList();

                return dataDynamic;
            }

        }

        public async Task<List<dynamic>> spGetMTDActualSummaryBudget(string year, string month, string yearBudget, long snapshotID)
        {
            var param = new Dictionary<string, object>();


            if (snapshotID == 0)
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"EXEC dbo.[spGetMTDActualSummaryBudget] '{year}', '{month}', '{yearBudget}','',0", param).ToList();
                return dataDynamic;
            }
            else
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"select * from dbo.[fnGetMTDActualSummaryBudgetSnapshot]('{snapshotID}')", param).ToList();
                return dataDynamic;
            }

        }

        public async Task<List<dynamic>> spGetMTDActualSummaryOutlook(string year, string month, string yearOutlook, string monthOutlook, long snapshotID)
        {
            var param = new Dictionary<string, object>();


            if (snapshotID == 0)
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"EXEC dbo.[spGetMTDActualSummaryOutlook] '{year}', '{month}','{yearOutlook}', '{monthOutlook}','',0", param).ToList();
                return dataDynamic;
            }
            else
            {
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"select * from dbo.[fnGetMTDActualSummaryOutlookSnapshot]('{snapshotID}')", param).ToList();
                return dataDynamic;
            }
        }

        public async Task<string> spSnapshotMTDActual(SnapshotParams sp)
        {
            var param = new Dictionary<string, object>();
            TradeSpendDashboardContext.CollectionFromSql($"EXEC dbo.[spSnapshotMTDActual] '{sp.Name}','{sp.YearActual}', '{sp.MonthActual}','{sp.YearOutlook}', '{sp.MonthOutlook}','{sp.YearBudget}','{appHelper.UserName}'", param).ToList();
            return string.Empty;
        }

        public async Task<List<dynamic>> fnGetSnapshotHistory()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"select * from dbo.fnGetSnapshotHistory()", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> spGetFC_vs_Budget(string profitCenter, string source1, string source2, string source1Year, string source1Month, string source2Year, string source2month)
        {
            var param = new Dictionary<string, object>();


            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"EXEC [dbo].[spGetFC_vs_Budget] '{profitCenter}', '{source1}','{source2}', '{source1Year}','{source1Month}', '{source2Year}','{source2month}'", param).ToList();
            return dataDynamic;

        }

        public async Task<List<dynamic>> fnGetMTDColNameList(string profitCenter, string source1, string source2)
        {
            var param = new Dictionary<string, object>();


              
                var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"select * from DBO.fnGetMTDColNameList('{profitCenter}','Source1','Source2')", param).ToList();
                return dataDynamic;
            
        }
    }
}
