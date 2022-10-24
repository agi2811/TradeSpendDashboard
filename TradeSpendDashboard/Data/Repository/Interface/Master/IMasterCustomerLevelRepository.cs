using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterCustomerLevelRepository : IRepository<MasterCustomerLevel>
    {
        Task<dynamic> GetByCode(string code);
        Task<dynamic> GetByCustomerLevel(string customerLevel);
        Task<List<dynamic>> GetAllDynamic(string search);
        Task<MasterCustomerLevel> GetAllById(long id);
        Task<List<dynamic>> GetAllCustomerLevel();
        Task<List<MasterCustomerLevel>> GetByAllField(string customerLevel1);
        Task<List<dynamic>> GetCustomerOption(string search);
        Task<dynamic> GetCustomerOptionById(string search);
        Task<List<dynamic>> GetOldChannelOptionByCustomerId(long customerId);
        Task<List<dynamic>> GetNewChannelOptionByCustomerId(long customerId);
    }
}
