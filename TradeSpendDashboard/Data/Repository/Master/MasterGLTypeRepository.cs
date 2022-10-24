using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterGLTypeRepository : BaseRepository<MasterGLType>, IMasterGLTypeRepository
    {
        public MasterGLTypeRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterGLType WHERE Id = '" + key + "'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> GetByType(string type)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT TOP 1 * FROM dbo.MasterGLType WHERE Type LIKE '%" + type + "%'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<MasterGLType>> GetByAllField(string type)
        {
            var data = TradeSpendDashboardContext.GetDataFromSqlToList<MasterGLType>($"SELECT * FROM dbo.MasterGLType WHERE Type='{type}' AND IsActive=1");
            return data;
        }
    }
}
