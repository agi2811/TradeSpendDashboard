using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterMenuService : IService<MasterMenu>
    {
        Task<MasterMenu> SaveData(MasterMenu param);

        Task<List<MasterMenu>> GetAll(string search);

        Task<List<MasterMenu>> GetAll(PaginationParam paging);

        Task<MasterMenu> Get(long Id);

        Task<bool> DeleteMenu(long Id);

        Task<MasterMenu> Update(MasterMenu model);

        public List<dynamic> GetAllDynamic(string search);

        public Task<MasterMenu> Update(long id, MasterMenu entity);

        public List<dynamic> GetAllByRoleDynamic(long roleId);

        public bool SetMenuRole(List<MasterMenuRole> param);

        public string GetMenuByRole(long roleId);
    }
}
