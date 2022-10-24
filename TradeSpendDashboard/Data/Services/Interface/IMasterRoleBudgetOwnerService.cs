using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterRoleBudgetOwnerService
    {
        Task<MasterRoleBudgetOwner> GetDataById(long Id);
        Task<MasterRoleBudgetOwner> GetDataByRoleBOId(long roleId, long budgetOwnerId);
    }
}
