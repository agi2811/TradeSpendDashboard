using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterUsersRepository : IRepository<MasterUsers>
    {
        Task<MasterUsers> Get(string userCode);
        Task<List<MasterUsers>> GetUsersSync();
        Task<MasterUsersDTO> GetByUserCode(string UserCode);
        List<dynamic> GetUsersAll();
        Task<List<dynamic>> GetCategoryOption(string search);
        Task<List<dynamic>> GetCategoryOptionById(string search);
        Task<List<dynamic>> GetProfitCenterOption(string search);
        Task<List<dynamic>> GetProfitCenterOptionById(string search);
        Task<MasterUsers> UpdateSync(MasterUsers entity);
    }
}
