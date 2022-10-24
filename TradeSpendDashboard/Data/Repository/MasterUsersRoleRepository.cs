using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterUsersRoleRepository : BaseRepository<MasterUsersRole>, IMasterUsersRoleRepository
    {
        public MasterUsersRoleRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public MasterUsersRole Get(string userCode)
        {
            var data = TradeSpendDashboardContext.MasterUsersRole.Where(a => a.UserCode.Equals(userCode)).FirstOrDefault();
            return data;
        }

        public async Task<MasterUsersRole> GetAsync(string UserCode)
        {
            var data = await TradeSpendDashboardContext.MasterUsersRole.Where(a => a.UserCode.Equals(UserCode)).FirstOrDefaultAsync();
            return data;
        }
    }
}
