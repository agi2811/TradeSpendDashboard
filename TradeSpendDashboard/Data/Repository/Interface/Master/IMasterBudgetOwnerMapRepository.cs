using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using TradeSpendDashboard.Models;
using TradeSpendDashboard.Models.DTO;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterBudgetOwnerMapRepository : IRepository<MasterBudgetOwnerMap>
    {
        Task<dynamic> GetByKey(string key);
        Task<dynamic> GetByValueTradeSpend(string valueTradeSpend);
        Task<List<dynamic>> GetBudgetOwnerOption(string search);
        Task<List<dynamic>> GetBudgetOwnerOptionByRoleId(string search);
        Task<dynamic> GetBudgetOwnerOptionById(string search);
        Task<List<MasterBudgetOwnerMap>> GetByAllField(string valueTradeSpend);
    }
}
