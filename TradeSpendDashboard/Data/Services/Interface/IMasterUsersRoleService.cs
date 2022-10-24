using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.DTO.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterUsersRoleService : IService<MasterUsersRoleDTO>
    {
        MasterUsersRoleDTO GetByUserCode(string usercode);
    }
}
