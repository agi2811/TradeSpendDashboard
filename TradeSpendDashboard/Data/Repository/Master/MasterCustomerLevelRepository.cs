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
    public class MasterCustomerLevelRepository : BaseRepository<MasterCustomerLevel>, IMasterCustomerLevelRepository
    {
        public MasterCustomerLevelRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<dynamic> GetByCode(string code)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterCustomerLevel where Id = '" + code + "'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<dynamic> GetByCustomerLevel(string customerLevel)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM dbo.MasterCustomerLevel where CustomerLevel1 like '%" + customerLevel + "%'", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public async Task<List<dynamic>> GetAllDynamic(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT top 100 * FROM dbo.MasterCustomerLevel where CustomerLevel1 like '%" + search + "%'", param).ToList();
            var data = dataDynamic.ToList();
            return data;
        }

        public async Task<List<dynamic>> GetAllCustomerLevel()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT MCL.*, MCM.OldChannelId, MCM.NewChannelId FROM dbo.MasterCustomerLevel MCL INNER JOIN MasterCustomerMap MCM ON MCL.CustomerId = MCM.Id WHERE MCL.IsActive=1", param).ToList();
            return dataDynamic;
        }

        public async Task<List<MasterCustomerLevel>> GetByAllField(string customerLevel1)
        {
            var data = TradeSpendDashboardContext.GetDataFromSqlToList<MasterCustomerLevel>($"SELECT * FROM dbo.MasterCustomerLevel WHERE CustomerLevel1='{customerLevel1}' AND IsActive=1");
            return data;
        }

        public async Task<MasterCustomerLevel> GetAllById(long id)
        {
            return await TradeSpendDashboardContext.Set<MasterCustomerLevel>().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<dynamic>> GetCustomerOption(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[MasterCustomerMap] WHERE Customer LIKE '%{search}%' AND IsActive=1 ORDER BY Customer", param).ToList();
            return dataDynamic;
        }

        public async Task<dynamic> GetCustomerOptionById(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM [dbo].[MasterCustomerMap] WHERE Id='{search}' AND IsActive=1", param).FirstOrDefault();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetOldChannelOptionByCustomerId(long customerId)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT MCM.*, MC.Channel FROM dbo.MasterCustomerMap MCM INNER JOIN dbo.MasterChannel MC ON MCM.OldChannelId = MC.Id WHERE MCM.customer in (SELECT customer from dbo.MasterCustomerMap WHERE id = {customerId})", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetNewChannelOptionByCustomerId(long customerId)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT MCM.*, MC.Channel FROM dbo.MasterCustomerMap MCM INNER JOIN dbo.MasterChannel MC ON MCM.NewChannelId = MC.Id WHERE MCM.customer in (SELECT customer from dbo.MasterCustomerMap WHERE id = {customerId})", param).ToList();
            return dataDynamic;
        }
    }
}
