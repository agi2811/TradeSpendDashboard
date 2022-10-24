using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using TradeSpendDashboard.Models;
using TradeSpendDashboard.Models.DTO;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterBudgetOwnerRepository : IRepository<MasterBudgetOwner>
    {
        Task<dynamic> GetByKey(string key);
        Task<dynamic> GetByBudgetOwner(string budgetOwner);
        Task<List<MasterBudgetOwner>> GetByAllField(string budgetOwner);
    }
}
