using Microsoft.EntityFrameworkCore;
using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TradeSpendDashboard.Data.Repository
{
    public class MasterCustomerRepository : BaseRepository<MasterCustomerMap>, IMasterCustomerRepository
    {
        public MasterCustomerRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<dynamic> GetByCode(string code)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterCustomerMap where Id = '" + code + "'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> GetByName(string name)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterCustomerMap where Customer like '%" + name + "%'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<dynamic>> GetAllDynamic(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT top 100 * FROM dbo.MasterCustomerMap where Customer like '%" + search + "%' OR CustomerMap like '%" + search + "%'", param).ToList();
            var data = dataDynamic.ToList();
            return data;
        }

        public IQueryable<MasterCustomerMap> GetAllCustomer()
        {
            return TradeSpendDashboardContext.Set<MasterCustomerMap>().AsNoTracking();
        }

        public async Task<List<MasterCustomerMap>> GetByAllField(string customer)
        {
            var data = TradeSpendDashboardContext.GetDataFromSqlToList<MasterCustomerMap>($"SELECT * FROM dbo.MasterCustomerMap WHERE Customer='{customer}' AND IsActive=1");
            return data;
        }

        public async Task<MasterCustomerMap> GetAllById(long id)
        {
            return await TradeSpendDashboardContext.Set<MasterCustomerMap>().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<dynamic>> GetChannelOption(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[MasterChannel] WHERE Channel LIKE '%{search}%' AND IsActive=1 ORDER BY Channel", param).ToList();
            return dataDynamic;
        }

        public async Task<dynamic> GetChannelOptionById(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[MasterChannel] WHERE Id='{search}' AND IsActive=1", param).FirstOrDefault();
            return dataDynamic;
        }
    }
}
