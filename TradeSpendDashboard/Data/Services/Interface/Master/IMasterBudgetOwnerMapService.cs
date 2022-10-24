using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterBudgetOwnerMapService : IService<MasterBudgetOwnerMapDTO>
    {
        Task<dynamic> GetByKey(string key);
        Task<List<dynamic>> GetBudgetOwnerOption(string search);
        Task<List<dynamic>> GetBudgetOwnerOptionByRoleId(string search);
        Task<dynamic> GetBudgetOwnerOptionById(string search);
        Task<List<MasterBudgetOwnerMap>> GetByAllField(string valueTradeSpend);
    }
}
