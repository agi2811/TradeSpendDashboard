using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterChannelRepository : BaseRepository<MasterChannel>, IMasterChannelRepository
    {
        public MasterChannelRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterChannel WHERE Id = '" + key + "'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> GetByChannel(string channel)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT TOP 1 * FROM dbo.MasterChannel WHERE Channel LIKE '%" + channel + "%'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<MasterChannel>> GetByAllField(string channel)
        {
            var data = TradeSpendDashboardContext.GetDataFromSqlToList<MasterChannel>($"SELECT * FROM dbo.MasterChannel WHERE Channel='{channel}' AND IsActive=1");
            return data;
        }
    }
}
