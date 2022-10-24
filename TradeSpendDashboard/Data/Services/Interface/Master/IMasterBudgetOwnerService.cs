using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterBudgetOwnerService : IService<MasterBudgetOwnerDTO>
    {
        Task<dynamic> GetByKey(string key);
        Task<List<MasterBudgetOwner>> GetByAllField(string budgetOwner);
    }
}
