using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterCategoryRepository : BaseRepository<MasterCategory>, IMasterCategoryRepository
    {
        public MasterCategoryRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterCategory WHERE Id = '" + key + "'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> GetByCategory(string category)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT TOP 1 * FROM dbo.MasterCategory WHERE Category LIKE '%" + category + "%'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<dynamic>> GetProfitCenterOption(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM dbo.MasterProfitCenter WHERE ProfitCenter LIKE '%{search}%' AND IsActive=1 ORDER BY ProfitCenter", param).ToList();
            return dataDynamic;
        }

        public async Task<dynamic> GetProfitCenterOptionById(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM dbo.MasterProfitCenter WHERE Id='{search}' AND IsActive=1", param).ToList();
            return dataDynamic;
        }

        public async Task<List<MasterCategory>> GetByAllField(string category)
        {
            var data = TradeSpendDashboardContext.GetDataFromSqlToList<MasterCategory>($"SELECT * FROM dbo.MasterCategory WHERE Category='{category}' AND IsActive=1");
            return data;
        }
    }
}
