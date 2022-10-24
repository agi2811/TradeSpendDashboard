using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterRoleRepository : IRepository<MasterRole>
    {
        Task<List<dynamic>> GetAllDynamic();
        Task<dynamic> GetRoleOptionById(string search);
        MasterRole GetById(long Id);
    }
}
