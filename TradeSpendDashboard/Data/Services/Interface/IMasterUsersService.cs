using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterUsersService : IService<MasterUsersDTO>
    {
        Task<MasterUsersDTO> SaveData(MasterUsersRoleDTO param);
        List<dynamic> GetAllDynamic();
        Task<MasterUsersRoleDTO> Update(MasterUsersRoleDTO entity);
        Task<MasterUsersDTO> GetByUserCode(string UserCode);
        Task<MasterUsersDTO> Delete(long Id);
        Task<bool> GenerateMappingAllUserData();
        Task<List<dynamic>> GetCategoryOption(string search);
        Task<dynamic> GetCategoryOptionById(string search);
        Task<List<dynamic>> GetProfitCenterOption(string search);
        Task<dynamic> GetProfitCenterOptionById(string search);
    }
}
