using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterCategoryMapRepository : BaseRepository<MasterCategoryMap>, IMasterCategoryMapRepository
    {
        public MasterCategoryMapRepository(TradeSpendDashboardContext context) : base(context)
        {
        }
        public async Task<List<dynamic>> GetAllData()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT MCM.*, MC.ProfitCenterId FROM dbo.MasterCategoryMap MCM INNER JOIN MasterCategory MC ON MCM.CategoryId = MC.Id WHERE MCM.IsActive=1", param).ToList();
            return dataDynamic;
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterCategoryMap WHERE Id = '" + key + "'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> GetByCategoryWeb(string category)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT TOP 1 * FROM dbo.MasterCategoryMap WHERE CategoryWeb LIKE '%" + category + "%'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<dynamic>> GetCategoryOption(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT MC.*, MPC.ProfitCenter FROM dbo.MasterCategory MC INNER JOIN dbo.MasterProfitCenter MPC ON MC.ProfitCenterId = MPC.Id WHERE MC.Category LIKE '%{search}%' AND MC.IsActive=1 ORDER BY MC.Category, MPC.ProfitCenter", param).ToList();
            return dataDynamic;
        }

        public async Task<dynamic> GetCategoryOptionById(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT MC.*, MPC.ProfitCenter FROM dbo.MasterCategory MC INNER JOIN dbo.MasterProfitCenter MPC ON MC.ProfitCenterId = MPC.Id WHERE MC.Id='{search}' AND MC.IsActive=1", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<dynamic>> GetProfitCenterOptionByCategoryId(long categoryId)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT MC.*, MP.ProfitCenter FROM dbo.MasterCategory MC INNER JOIN dbo.MasterProfitCenter MP ON MC.ProfitCenterId = MP.Id WHERE MC.Category in (SELECT Category from dbo.MasterCategory WHERE id = {categoryId})", param).ToList();
            return dataDynamic;
        }

        public async Task<List<MasterCategoryMap>> GetByAllField(string categoryWeb)
        {
            var data = TradeSpendDashboardContext.GetDataFromSqlToList<MasterCategoryMap>($"SELECT * FROM dbo.MasterCategoryMap WHERE CategoryWeb='{categoryWeb}' AND IsActive=1");
            return data;
        }
    }
}
