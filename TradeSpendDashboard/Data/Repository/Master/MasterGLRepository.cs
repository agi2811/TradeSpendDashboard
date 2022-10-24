using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterGLRepository : BaseRepository<MasterGL>, IMasterGLRepository
    {
        public MasterGLRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterGL WHERE GLCode = '" + key + "'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> GetByGLDesc(string desc)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT TOP 1 * FROM dbo.MasterGL WHERE GLDesc LIKE '%" + desc + "%'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> GetByGLDescCode(string desc, string code)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT TOP 1 * FROM dbo.MasterGL WHERE GLDesc LIKE '%" + desc + "%' AND GLCode LIKE '%" + code + "%'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<dynamic>> GetGLTypeOption(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM dbo.MasterGLType WHERE Type LIKE '%{search}%' AND IsActive=1 ORDER BY Type", param).ToList();
            return dataDynamic;
        }

        public async Task<dynamic> GetGLTypeOptionById(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM dbo.MasterGLType WHERE Id='{search}' AND IsActive=1", param).ToList();
            return dataDynamic;
        }

        public async Task<List<MasterGL>> GetByAllField(string code)
        {
            var data = TradeSpendDashboardContext.GetDataFromSqlToList<MasterGL>($"SELECT * FROM dbo.MasterGL WHERE GLCode='{code}' AND IsActive=1");
            return data;
        }
    }
}
