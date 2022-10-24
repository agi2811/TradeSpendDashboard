using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterRoleBudgetOwnerRepository : ICustomRepository<MasterRoleBudgetOwner>
    {
        Task<MasterRoleBudgetOwner> GetDataById(long Id);
        Task<MasterRoleBudgetOwner> GetDataByRoleBOId(long roleId, long budgetOwnerId);
        Task<MasterRoleBudgetOwner> Add(MasterRoleBudgetOwner param);
        bool DeleteByUserCode(long Id);
    }
}
