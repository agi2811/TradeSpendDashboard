using TradeSpendDashboard.Models.Entity.Master;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterUsersRoleRepository : IRepository<MasterUsersRole>
    {
        Task<MasterUsersRole> GetAsync(string userCode);

        MasterUsersRole Get(string userCode);
    }
}
