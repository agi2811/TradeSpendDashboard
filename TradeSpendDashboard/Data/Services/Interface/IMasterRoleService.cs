using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterRoleService : IService<MasterRoleDTO>
    {
        public Task<List<MasterRoleDTO>> GetAll(string key);
        public Task<List<dynamic>> GetAllDynamic();
        Task<dynamic> GetRoleOptionById(string search);
        public MasterRole GetById(long Id);
    }
}
