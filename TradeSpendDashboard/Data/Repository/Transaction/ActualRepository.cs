using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Data.Repository.Interface.Transaction;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.TradeSpendDashboard.Transaction.Actual;
using TradeSpendDashboard.Models.Entity.Transaction;

namespace TradeSpendDashboard.Data.Repository.Transaction
{
    public class ActualRepository : BaseRepository<ActualOutlook>, IActualRepository
    {
        public ActualRepository(TradeSpendDashboardContext context) : base(context) { }

        public async Task<List<dynamic>> GetPrimarySalesActual(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            param.Add("@UserLoginID", userLoginID);
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_PrimarySales]('Actual', '{userLoginID}', '{year}', '{month}') ORDER BY Id ASC, Year DESC, Channel, ProfitCenter, Category ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetSecondarySalesActual(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            param.Add("@UserLoginID", userLoginID);
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_SecondarySales]('Actual', '{userLoginID}', '{year}', '{month}') ORDER BY Id ASC, Year DESC, NewChannel, OldChannel, Customer, Category ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetSpendingPhasingActual(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            param.Add("@UserLoginID", userLoginID);
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[FN_Get_SpendingPhasing]('Actual', '{userLoginID}', '{year}', '{month}') ORDER BY Id ASC, Year DESC, BudgetOwner, ProfitCenter, Category, GLCode, GLDesc, GLType, NewChannel, Customer, OldChannel, MG3, MG4, Activity ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetTempMappingPrimarySalesActual(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeTempPrimarySalesActual] WHERE InsertedBy='{userLoginID}' AND YearPeriod = '{year}' AND MonthPeriod = '{month}' ORDER BY Id ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetTempMappingSecondarySalesActual(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeTempSecondarySalesActual] WHERE InsertedBy='{userLoginID}' AND YearPeriod = '{year}' AND MonthPeriod = '{month}' ORDER BY Id ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetTempMappingSpendingPhasingActual(string userLoginID, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[TradeTempSpendingActual] WHERE InsertedBy='{userLoginID}' AND YearPeriod = '{year}' AND MonthPeriod = '{month}' ORDER BY Id ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetYearsPrimarySalesActual()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT DISTINCT CAST(Years AS VARCHAR) Years FROM (SELECT Years FROM [dbo].[FN_Get_Years]() UNION ALL SELECT Year Years FROM [dbo].[TradeHeadPrimarySalesActual]) tbYears ORDER BY 1 DESC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetYearsSecondarySalesActual()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT DISTINCT CAST(Years AS VARCHAR) Years FROM (SELECT Years FROM [dbo].[FN_Get_Years]() UNION ALL SELECT Year Years FROM [dbo].[TradeHeadSecondarySalesActual]) tbYears ORDER BY 1 DESC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetYearsSpendingPhasingActual()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT DISTINCT CAST(Years AS VARCHAR) Years FROM (SELECT Years FROM [dbo].[FN_Get_Years]() UNION ALL SELECT Year Years FROM [dbo].[TradeHeadSpendingActual]) tbYears ORDER BY 1 DESC", param).ToList();
            return dataDynamic;
        }

        public List<dynamic> TruncateTempMappingPrimarySales(string usercode, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"DELETE FROM [dbo].[TradeTempPrimarySalesActual] WHERE YearPeriod = '{year}' AND MonthPeriod = '{month}' AND InsertedBy = '{usercode}'", param).ToList();
            return dataDynamic;
        }

        public List<dynamic> TruncateTempMappingSecondarySales(string usercode, string year, string month)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"DELETE FROM [dbo].[TradeTempSecondarySalesActual] WHERE YearPeriod = '{year}' AND MonthPeriod = '{month}' AND InsertedBy = '{usercode}'", param).ToList();
            return dataDynamic;
        }

        public List<ErrorMessage> SpImportPrimarySalesActual(string usercode, string filename, string year, string month)
        {
            var result = TradeSpendDashboardContext.GetDataFromSqlToList<ErrorMessage>($"exec [dbo].[SP_Import_Primary_Sales_Actual] '{usercode}', '{filename}', '{year}', '{month}'");
            return result;
        }

        public List<ErrorMessage> SpImportSecondarySalesActual(string usercode, string filename, string year, string month)
        {
            var result = TradeSpendDashboardContext.GetDataFromSqlToList<ErrorMessage>($"exec [dbo].[SP_Import_Secondary_Sales_Actual] '{usercode}', '{filename}', '{year}', '{month}'");
            return result;
        }

        public List<ErrorMessage> SpInsertSpendingPhasingActual(string usercode, string year, string month, int plus)
        {
            var result = TradeSpendDashboardContext.GetDataFromSqlToList<ErrorMessage>($"exec [dbo].[SP_Insert_Spending_Phasing_Temp_Actual] 'SpendingPhasing', '{usercode}', '{year}', '{month}', '{plus}'");
            return result;
        }

        public List<ErrorMessage> SpInterfaceSpendingPhasingActual(string usercode, string year, string month)
        {
            var result = TradeSpendDashboardContext.GetDataFromSqlToList<ErrorMessage>($"exec [dbo].[SP_Interface_Spending_Phasing_Actual] '{usercode}', '{year}', '{month}'");
            return result;
        }
    }
}
