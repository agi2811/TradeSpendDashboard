using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterCustomerLevelService : IService<MasterCustomerLevelDTO>
    {
        Task<dynamic> GetByCode(string code);
        Task<PaginatedResult<MasterCustomerLevel>> GetAllCustom(PaginationParam param);
        Task<List<dynamic>> GetAllCustomOption(string search);
        Task<List<dynamic>> GetAllCustomerLevel();
        Task<List<MasterCustomerLevel>> GetByAllField(string customerLevel1);
        Task<List<dynamic>> GetCustomerOption(string search);
        Task<dynamic> GetCustomerOptionById(string search);
        Task<List<dynamic>> GetOldChannelOptionByCustomerId(long customerId);
        Task<List<dynamic>> GetNewChannelOptionByCustomerId(long customerId);
    }
}
