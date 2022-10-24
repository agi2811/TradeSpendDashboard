using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using TradeSpendDashboard.Models;
using TradeSpendDashboard.Models.DTO;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterProfitCenterRepository : IRepository<MasterProfitCenter>
    {
        Task<dynamic> GetByKey(string key);
        Task<dynamic> GetByProfitCenter(string profitCenter);
        Task<List<MasterProfitCenter>> GetByAllField(string profitCenter);
    }
}
