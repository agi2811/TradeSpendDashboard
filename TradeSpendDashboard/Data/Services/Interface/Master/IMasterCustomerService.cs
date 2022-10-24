using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterCustomerService : IService<MasterCustomerMapDTO>
    {
        Task<dynamic> GetByCode(string code);
        Task<PaginatedResult<MasterCustomerMap>> GetAllCustom(PaginationParam param);
        Task<List<dynamic>> GetAllCustomOption(string search);
        Task<MasterCustomerMapDTO> Activate(long id);
        Task<List<MasterCustomerMap>> GetAllCustomer();
        Task<List<MasterCustomerMap>> GetByAllField(string customer);
        Task<List<dynamic>> GetChannelOption(string search);
        Task<dynamic> GetChannelOptionById(string search);
    }
}
