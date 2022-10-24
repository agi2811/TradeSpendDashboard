using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterBudgetOwnerRepository : BaseRepository<MasterBudgetOwner>, IMasterBudgetOwnerRepository
    {
        public MasterBudgetOwnerRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterBudgetOwner WHERE Id = '" + key + "'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> GetByBudgetOwner(string budgetOwner)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT TOP 1 * FROM dbo.MasterBudgetOwner WHERE BudgetOwner LIKE '%" + budgetOwner + "%'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<MasterBudgetOwner>> GetByAllField(string budgetOwner)
        {
            var data = TradeSpendDashboardContext.GetDataFromSqlToList<MasterBudgetOwner>($"SELECT * FROM dbo.MasterBudgetOwner WHERE BudgetOwner='{budgetOwner}' AND IsActive=1");
            return data;
        }
    }
}
