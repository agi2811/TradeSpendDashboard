using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterChannelService : IService<MasterChannelDTO>
    {
        Task<dynamic> GetByKey(string key);
        Task<List<MasterChannel>> GetByAllField(string channel);
    }
}
