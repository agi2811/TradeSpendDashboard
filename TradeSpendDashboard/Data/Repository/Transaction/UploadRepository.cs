using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TradeSpendDashboard.Data.Repository.Interface.Transaction;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.TradeSpendDashboard.Transaction.Upload;
using TradeSpendDashboard.Models.Entity.Transaction;

namespace TradeSpendDashboard.Data.Repository.Transaction
{
    public class UploadRepository : BaseRepository<UploadOutlook>, IUploadRepository
    {
        public UploadRepository(TradeSpendDashboardContext context) : base(context) { }

        public async Task<List<dynamic>> GetPrimarySalesUpload(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            param.Add("@UserLoginID", userLoginID);
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_PrimarySales]('Upload', '{userLoginID}', '{year}', '{month}') ORDER BY Id ASC, Year DESC, Channel, ProfitCenter, Category ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetSecondarySalesUpload(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            param.Add("@UserLoginID", userLoginID);
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_SecondarySales]('Upload', '{userLoginID}', '{year}', '{month}') ORDER BY Id ASC, Year DESC, NewChannel, OldChannel, Customer, Category ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetSpendingPhasingUpload(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            param.Add("@UserLoginID", userLoginID);
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_SpendingPhasing]('Upload', '{userLoginID}', '{year}', '{month}') ORDER BY Id ASC, Year DESC, BudgetOwner, ProfitCenter, Category, GLCode, GLDesc, GLType, NewChannel, Customer, OldChannel, MG3, MG4, Activity ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetTempMappingPrimarySalesUpload(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeTempPrimarySalesOutlook] WHERE InsertedBy='{userLoginID}' AND YearPeriod = '{year}' AND MonthPeriod = '{month}' ORDER BY Id ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetTempMappingSecondarySalesUpload(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeTempSecondarySalesOutlook] WHERE InsertedBy='{userLoginID}' AND YearPeriod = '{year}' AND MonthPeriod = '{month}' ORDER BY Id ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetTempMappingSpendingPhasingUpload(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeTempSpendingOutlook] WHERE InsertedBy='{userLoginID}' AND YearPeriod = '{year}' AND MonthPeriod = '{month}' ORDER BY Id ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetYearsPrimarySalesUpload()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT DISTINCT CAST(Years AS VARCHAR) Years FROM (SELECT Years FROM [dbo].[FN_Get_Years]() UNION ALL SELECT Year Years FROM [dbo].[TradeHeadPrimarySalesOutlook]) tbYears ORDER BY 1 DESC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetYearsSecondarySalesUpload()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT DISTINCT CAST(Years AS VARCHAR) Years FROM (SELECT Years FROM [dbo].[FN_Get_Years]() UNION ALL SELECT Year Years FROM [dbo].[TradeHeadSecondarySalesOutlook]) tbYears ORDER BY 1 DESC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetYearsSpendingPhasingUpload()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT DISTINCT CAST(Years AS VARCHAR) Years FROM (SELECT Years FROM [dbo].[FN_Get_Years]() UNION ALL SELECT Year Years FROM [dbo].[TradeHeadSpendingOutlook]) tbYears ORDER BY 1 DESC", param).ToList();
            return dataDynamic;
        }

        public async Task<TradeHeadPrimarySalesOutlook> GetPrimarySalesHeadUpload(string year, string month)
        {
            return await TradeSpendDashboardContext.Set<TradeHeadPrimarySalesOutlook>().Where(w => w.IsActive && w.Year == year && w.Month == month).FirstOrDefaultAsync();
        }

        public async Task<TradeHeadSecondarySalesOutlook> GetSecondarySalesHeadUpload(string year, string month)
        {
            return await TradeSpendDashboardContext.Set<TradeHeadSecondarySalesOutlook>().Where(w => w.IsActive && w.Year == year && w.Month == month).FirstOrDefaultAsync();
        }

        public async Task<TradeHeadSpendingOutlook> GetSpendingPhasingHeadUpload(string year, string month)
        {
            return await TradeSpendDashboardContext.Set<TradeHeadSpendingOutlook>().Where(w => w.IsActive && w.Year == year && w.Month == month).FirstOrDefaultAsync();
        }

        public async Task<TradeHeadPrimarySalesOutlook> AddPrimarySalesHeadUpload(TradeHeadPrimarySalesOutlook entity)
        {
            TradeSpendDashboardContext.Set<TradeHeadPrimarySalesOutlook>().Add(entity);
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TradeHeadSecondarySalesOutlook> AddSecondarySalesHeadUpload(TradeHeadSecondarySalesOutlook entity)
        {
            TradeSpendDashboardContext.Set<TradeHeadSecondarySalesOutlook>().Add(entity);
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TradeHeadSpendingOutlook> AddSpendingPhasingHeadUpload(TradeHeadSpendingOutlook entity)
        {
            TradeSpendDashboardContext.Set<TradeHeadSpendingOutlook>().Add(entity);
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TradeHeadPrimarySalesOutlook> UpdatePrimarySalesHeadUpload(TradeHeadPrimarySalesOutlook entity)
        {
            TradeSpendDashboardContext.Entry(entity).State = EntityState.Modified;
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TradeHeadSecondarySalesOutlook> UpdateSecondarySalesHeadUpload(TradeHeadSecondarySalesOutlook entity)
        {
            TradeSpendDashboardContext.Entry(entity).State = EntityState.Modified;
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TradeHeadSpendingOutlook> UpdateSpendingPhasingHeadUpload(TradeHeadSpendingOutlook entity)
        {
            TradeSpendDashboardContext.Entry(entity).State = EntityState.Modified;
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<dynamic> CheckPrimarySalesHeadUpload(string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeHeadPrimarySalesOutlook] WHERE Year = '{year}' AND Month = '{month}'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> CheckSecondarySalesHeadUpload(string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeHeadSecondarySalesOutlook] WHERE Year = '{year}' AND Month = '{month}'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> CheckSpendingPhasingHeadUpload(string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeHeadSpendingOutlook] WHERE Year = '{year}' AND Month = '{month}'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public List<dynamic> TruncateTempMappingPrimarySales(string usercode, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"DELETE FROM [dbo].[TradeTempPrimarySalesOutlook] WHERE YearPeriod = '{year}' AND MonthPeriod = '{month}' AND InsertedBy = '{usercode}'", param).ToList();
            return dataDynamic;
        }

        public List<dynamic> TruncateTempMappingSecondarySales(string usercode, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"DELETE FROM [dbo].[TradeTempSecondarySalesOutlook] WHERE YearPeriod = '{year}' AND MonthPeriod = '{month}' AND InsertedBy = '{usercode}'", param).ToList();
            return dataDynamic;
        }

        public List<dynamic> TruncateTempMappingSpendingPhasing(string usercode, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"DELETE FROM [dbo].[TradeTempSpendingOutlook] WHERE YearPeriod = '{year}' AND MonthPeriod = '{month}' AND InsertedBy = '{usercode}'", param).ToList();
            return dataDynamic;
        }

        public List<ErrorMessage> SpImportPrimarySalesUpload(string usercode, string filename, string year, string month)
        {
            var result = TradeSpendDashboardContext.GetDataFromSqlToList<ErrorMessage>($"exec [dbo].[SP_Import_Primary_Sales_Upload] '{usercode}', '{filename}', '{year}', '{month}'");
            return result;
        }

        public List<ErrorMessage> SpImportSecondarySalesUpload(string usercode, string filename, string year, string month)
        {
            var result = TradeSpendDashboardContext.GetDataFromSqlToList<ErrorMessage>($"exec [dbo].[SP_Import_Secondary_Sales_Upload] '{usercode}', '{filename}', '{year}', '{month}'");
            return result;
        }

        public List<ErrorMessage> SpImportSpendingPhasingUpload(string usercode, string filename, string year, string month)
        {
            var result = TradeSpendDashboardContext.GetDataFromSqlToList<ErrorMessage>($"exec [dbo].[SP_Import_Spending_Phasing_Upload] '{usercode}', '{filename}', '{year}', '{month}'");
            return result;
        }
    }
}
