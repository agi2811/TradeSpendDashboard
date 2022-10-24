using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterMenuRepository : IRepository<MasterMenu>
    {
        List<dynamic> GetAllDynamic();

        List<dynamic> GetAllByRoleDynamic(long roleId);

        bool DeleteMenuRoleByRoleId(long roleId);

        bool DeleteMenuRoleByRoleMenuId(long roleId, long menuId);

        bool AddMenuRole(MasterMenuRole param);

        Task<bool> DeleteMenu(long id);

        string GenerateMenuHTML(List<MasterMenu> dataMenu = null, long Parent = 0, string menu = "", bool haschild = false, long roleId = 0, string BaseUrl = "");
    }
}
