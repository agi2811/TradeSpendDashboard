using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TradeSpendDashboard.Data.Repository.Interface.Transaction;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.TradeSpendDashboard.Transaction.Update;
using TradeSpendDashboard.Models.Entity.Transaction;

namespace TradeSpendDashboard.Data.Repository.Transaction
{
    public class UpdateRepository : BaseRepository<UpdateOutlook>, IUpdateRepository
    {
        public UpdateRepository(TradeSpendDashboardContext context) : base(context) { }

        public async Task<List<dynamic>> GetPrimarySalesUpdate(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            param.Add("@UserLoginID", userLoginID);
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_PrimarySales]('Update', '{userLoginID}', '{year}', '{month}') ORDER BY Id ASC, Year DESC, Channel, ProfitCenter, Category ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetSecondarySalesUpdate(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            param.Add("@UserLoginID", userLoginID);
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_SecondarySales]('Update', '{userLoginID}', '{year}', '{month}') ORDER BY Id ASC, Year DESC, NewChannel, OldChannel, Customer, Category ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetSpendingPhasingUpdate(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            param.Add("@UserLoginID", userLoginID);
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_SpendingPhasing]('Update', '{userLoginID}', '{year}', '{month}') ORDER BY Id ASC, Year DESC, BudgetOwner, ProfitCenter, Category, GLCode, GLDesc, GLType, NewChannel, Customer, OldChannel, MG3, MG4, Activity ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetTempMappingPrimarySalesUpdate(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeTempPrimarySalesUpdate] WHERE InsertedBy='{userLoginID}' AND YearPeriod = '{year}' AND MonthPeriod = '{month}' ORDER BY Id ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetTempMappingSecondarySalesUpdate(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeTempSecondarySalesUpdate] WHERE InsertedBy='{userLoginID}' AND YearPeriod = '{year}' AND MonthPeriod = '{month}' ORDER BY Id ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetTempMappingSpendingPhasingUpdate(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeTempSpendingUpdate] WHERE InsertedBy='{userLoginID}' AND YearPeriod = '{year}' AND MonthPeriod = '{month}' ORDER BY Id ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetYearsPrimarySalesUpdate()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT DISTINCT CAST(Years AS VARCHAR) Years FROM (SELECT Years FROM [dbo].[FN_Get_Years]() UNION ALL SELECT Year Years FROM [dbo].[TradeHeadPrimarySalesUpdate]) tbYears ORDER BY 1 DESC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetYearsSecondarySalesUpdate()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT DISTINCT CAST(Years AS VARCHAR) Years FROM (SELECT Years FROM [dbo].[FN_Get_Years]() UNION ALL SELECT Year Years FROM [dbo].[TradeHeadSecondarySalesUpdate]) tbYears ORDER BY 1 DESC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetYearsSpendingPhasingUpdate()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT DISTINCT CAST(Years AS VARCHAR) Years FROM (SELECT Years FROM [dbo].[FN_Get_Years]() UNION ALL SELECT Year Years FROM [dbo].[TradeHeadSpendingUpdate]) tbYears ORDER BY 1 DESC", param).ToList();
            return dataDynamic;
        }

        public async Task<TradeHeadPrimarySalesUpdate> GetPrimarySalesHeadUpdate(string year, string month)
        {
            return await TradeSpendDashboardContext.Set<TradeHeadPrimarySalesUpdate>().Where(w => w.IsActive && w.Year == year && w.Month == month).FirstOrDefaultAsync();
        }

        public async Task<TradeHeadSecondarySalesUpdate> GetSecondarySalesHeadUpdate(string year, string month)
        {
            return await TradeSpendDashboardContext.Set<TradeHeadSecondarySalesUpdate>().Where(w => w.IsActive && w.Year == year && w.Month == month).FirstOrDefaultAsync();
        }

        public async Task<TradeHeadSpendingUpdate> GetSpendingPhasingHeadUpdate(string year, string month)
        {
            return await TradeSpendDashboardContext.Set<TradeHeadSpendingUpdate>().Where(w => w.IsActive && w.Year == year && w.Month == month).FirstOrDefaultAsync();
        }

        public async Task<TradeHeadPrimarySalesUpdate> AddPrimarySalesHeadUpdate(TradeHeadPrimarySalesUpdate entity)
        {
            TradeSpendDashboardContext.Set<TradeHeadPrimarySalesUpdate>().Add(entity);
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TradeHeadSecondarySalesUpdate> AddSecondarySalesHeadUpdate(TradeHeadSecondarySalesUpdate entity)
        {
            TradeSpendDashboardContext.Set<TradeHeadSecondarySalesUpdate>().Add(entity);
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TradeHeadSpendingUpdate> AddSpendingPhasingHeadUpdate(TradeHeadSpendingUpdate entity)
        {
            TradeSpendDashboardContext.Set<TradeHeadSpendingUpdate>().Add(entity);
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TradeHeadPrimarySalesUpdate> UpdatePrimarySalesHeadUpdate(TradeHeadPrimarySalesUpdate entity)
        {
            TradeSpendDashboardContext.Entry(entity).State = EntityState.Modified;
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TradeHeadSecondarySalesUpdate> UpdateSecondarySalesHeadUpdate(TradeHeadSecondarySalesUpdate entity)
        {
            TradeSpendDashboardContext.Entry(entity).State = EntityState.Modified;
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TradeHeadSpendingUpdate> UpdateSpendingPhasingHeadUpdate(TradeHeadSpendingUpdate entity)
        {
            TradeSpendDashboardContext.Entry(entity).State = EntityState.Modified;
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<dynamic> CheckPrimarySalesHeadUpdate(string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeHeadPrimarySalesUpdate] WHERE Year = '{year}' AND Month = '{month}'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> CheckSecondarySalesHeadUpdate(string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeHeadSecondarySalesUpdate] WHERE Year = '{year}' AND Month = '{month}'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> CheckSpendingPhasingHeadUpdate(string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeHeadSpendingUpdate] WHERE Year = '{year}' AND Month = '{month}'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public List<dynamic> TruncateTempMappingPrimarySales(string usercode, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"DELETE FROM [dbo].[TradeTempPrimarySalesUpdate] WHERE YearPeriod = '{year}' AND MonthPeriod = '{month}' AND InsertedBy = '{usercode}'", param).ToList();
            return dataDynamic;
        }

        public List<dynamic> TruncateTempMappingSecondarySales(string usercode, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"DELETE FROM [dbo].[TradeTempSecondarySalesUpdate] WHERE YearPeriod = '{year}' AND MonthPeriod = '{month}' AND InsertedBy = '{usercode}'", param).ToList();
            return dataDynamic;
        }

        public List<dynamic> TruncateTempMappingSpendingPhasing(string usercode, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"DELETE FROM [dbo].[TradeTempSpendingUpdate] WHERE YearPeriod = '{year}' AND MonthPeriod = '{month}' AND InsertedBy = '{usercode}'", param).ToList();
            return dataDynamic;
        }

        public List<ErrorMessage> SpImportPrimarySalesUpdate(string usercode, string filename, string year, string month)
        {
            var result = TradeSpendDashboardContext.GetDataFromSqlToList<ErrorMessage>($"exec [dbo].[SP_Import_Primary_Sales_Update] '{usercode}', '{filename}', '{year}', '{month}'");
            return result;
        }

        public List<ErrorMessage> SpImportSecondarySalesUpdate(string usercode, string filename, string year, string month)
        {
            var result = TradeSpendDashboardContext.GetDataFromSqlToList<ErrorMessage>($"exec [dbo].[SP_Import_Secondary_Sales_Update] '{usercode}', '{filename}', '{year}', '{month}'");
            return result;
        }

        public List<ErrorMessage> SpImportSpendingPhasingUpdate(string usercode, string filename, string year, string month)
        {
            var result = TradeSpendDashboardContext.GetDataFromSqlToList<ErrorMessage>($"exec [dbo].[SP_Import_Spending_Phasing_Update] '{usercode}', '{filename}', '{year}', '{month}'");
            return result;
        }

        public async Task<List<dynamic>> DownloadSpendingPhasingUpdate(string userLoginID, string year, string month, string budgetOwner, List<string> categoryList, List<string> profitCenterList)
        {
            var param = new Dictionary<string, object>();
            param.Add("@UserLoginID", userLoginID);
            param.Add("@Year", year);
            param.Add("@Month", month);
            param.Add("@BudgetOwner", budgetOwner);

            var categoryListString = categoryList[0] != null ? string.Join(",", categoryList.Select(x => x.ToString()).ToArray()) : "";
            var profitCenterListString = profitCenterList[0] != null ? string.Join(",", profitCenterList.Select(x => x.ToString()).ToArray()) : "";
            var query = @"SELECT * FROM [dbo].[FN_Get_SpendingPhasing]('Update', @UserLoginID, @Year, @Month)
                          WHERE BudgetOwner = @BudgetOwner
                          AND (Category IN (SELECT value FROM STRING_SPLIT('" + categoryListString + @"', ','))
                          OR ProfitCenter IN (SELECT value FROM STRING_SPLIT('" + profitCenterListString + @"', ',')))
                          ORDER BY Id ASC
						          , Year DESC
								  , BudgetOwner
								  , ProfitCenter
								  , Category
								  , GLCode
								  , GLDesc
								  , GLType
								  , NewChannel
								  , Customer
								  , OldChannel
								  , MG3
								  , MG4
								  , Activity ASC";
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(query, param).ToList();
            return dataDynamic;
        }
    }
}
