using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterBudgetOwnerMapRepository : BaseRepository<MasterBudgetOwnerMap>, IMasterBudgetOwnerMapRepository
    {
        public MasterBudgetOwnerMapRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterBudgetOwnerMap WHERE Id = '" + key + "'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> GetByValueTradeSpend(string valueTradeSpend)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT TOP 1 * FROM dbo.MasterBudgetOwnerMap WHERE ValueTradeSpend LIKE '%" + valueTradeSpend + "%'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<dynamic>> GetBudgetOwnerOption(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM dbo.MasterBudgetOwner WHERE BudgetOwner LIKE '%{search}%' AND IsActive=1 ORDER BY BudgetOwner", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetBudgetOwnerOptionByRoleId(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT MBO.* FROM dbo.MasterBudgetOwner MBO INNER JOIN dbo.MasterRoleBudgetOwner MRB ON MBO.Id = MRB.BudgetOwnerId WHERE MRB.RoleId = '{search}' AND IsActive=1 ORDER BY MBO.BudgetOwner", param).ToList();
            return dataDynamic;
        }

        public async Task<dynamic> GetBudgetOwnerOptionById(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM dbo.MasterBudgetOwner WHERE Id='{search}' AND IsActive=1", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<MasterBudgetOwnerMap>> GetByAllField(string valueTradeSpend)
        {
            var data = TradeSpendDashboardContext.GetDataFromSqlToList<MasterBudgetOwnerMap>($"SELECT * FROM dbo.MasterBudgetOwnerMap WHERE ValueTradeSpend='{valueTradeSpend}' AND IsActive=1");
            return data;
        }
    }
}
