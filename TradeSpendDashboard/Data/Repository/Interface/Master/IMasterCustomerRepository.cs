using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterCustomerRepository : IRepository<MasterCustomerMap>
    {
        Task<dynamic> GetByCode(string code);
        Task<dynamic> GetByName(string name);
        Task<List<dynamic>> GetAllDynamic(string search);
        Task<MasterCustomerMap> GetAllById(long id);
        IQueryable<MasterCustomerMap> GetAllCustomer();
        Task<List<MasterCustomerMap>> GetByAllField(string customer);
        Task<List<dynamic>> GetChannelOption(string search);
        Task<dynamic> GetChannelOptionById(string search);
    }
}
