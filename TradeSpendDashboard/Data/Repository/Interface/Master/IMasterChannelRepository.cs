using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using TradeSpendDashboard.Models;
using TradeSpendDashboard.Models.DTO;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterChannelRepository : IRepository<MasterChannel>
    {
        Task<dynamic> GetByKey(string key);
        Task<dynamic> GetByChannel(string channel);
        Task<List<MasterChannel>> GetByAllField(string channel);
    }
}
