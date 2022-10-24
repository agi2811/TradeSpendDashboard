using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterProfitCenterRepository : BaseRepository<MasterProfitCenter>, IMasterProfitCenterRepository
    {
        public MasterProfitCenterRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterProfitCenter WHERE Id = '" + key + "'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> GetByProfitCenter(string profitCenter)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT TOP 1 * FROM dbo.MasterProfitCenter WHERE ProfitCenter LIKE '%" + profitCenter + "%'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<MasterProfitCenter>> GetByAllField(string profitCenter)
        {
            var data = TradeSpendDashboardContext.GetDataFromSqlToList<MasterProfitCenter>($"SELECT * FROM dbo.MasterProfitCenter WHERE ProfitCenter='{profitCenter}' AND IsActive=1");
            return data;
        }
    }
}
