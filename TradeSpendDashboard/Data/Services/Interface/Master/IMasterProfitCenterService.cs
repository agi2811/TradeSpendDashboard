using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterProfitCenterService : IService<MasterProfitCenterDTO>
    {
        Task<dynamic> GetByKey(string key);
        Task<List<MasterProfitCenter>> GetByAllField(string profitCenter);
    }
}
