using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.DTO;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterUsersSpendingRepository : CustomRepository<MasterUsersSpending>, IMasterUsersSpendingRepository
    {
        public MasterUsersSpendingRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<dynamic> GetBOByUserCode(string userCode)
        {
            var param = new Dictionary<string, object>();
            var sql = $"SELECT DISTINCT BudgetOwner FROM [dbo].[FN_Get_UserSpending]('{userCode}')";
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(sql, param).FirstOrDefault();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetByUserCode(string userCode)
        {
            var param = new Dictionary<string, object>();
            var sql = $"SELECT * FROM [dbo].[FN_Get_UserSpending]('{userCode}')";
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(sql, param).ToList();
            return dataDynamic;
        }

        public async Task<MasterUsersSpending> Add(MasterUsersSpending param)
        {
            var data = await TradeSpendDashboardContext.MasterUsersSpending.AddAsync(param);
            TradeSpendDashboardContext.SaveChanges();
            return param;
        }

        public ValidationDTO SaveUsersSpending(string usercode, long budgetOwnerId, List<int> categoryList, List<int> profitCenterList)
        {
            var categoryListString = string.Join(",", categoryList.Select(x => x.ToString()).ToArray());
            var profitCenterListString = string.Join(",", profitCenterList.Select(x => x.ToString()).ToArray());
            var sql = $"exec [dbo].[SP_Update_Users_Spending] '{usercode}', {budgetOwnerId}, '{categoryListString}', '{profitCenterListString}'";
            var dataDynamic = TradeSpendDashboardContext.GetDataFromSqlToSingle<ValidationDTO>(sql);
            return dataDynamic;
        }

        public bool DeleteByUserCode(string userCode)
        {
            var param = new Dictionary<string, object>();
            var sql = $"DELETE FROM MasterUsersSpending WHERE UserCode = '{userCode}'";
            TradeSpendDashboardContext.CollectionFromSql(sql, param).ToList();
            return true;
        }
    }
}
