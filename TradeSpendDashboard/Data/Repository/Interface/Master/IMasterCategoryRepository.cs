using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using TradeSpendDashboard.Models;
using TradeSpendDashboard.Models.DTO;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterCategoryRepository : IRepository<MasterCategory>
    {
        Task<dynamic> GetByKey(string key);
        Task<dynamic> GetByCategory(string category);
        Task<List<dynamic>> GetProfitCenterOption(string search);
        Task<dynamic> GetProfitCenterOptionById(string search);
        Task<List<MasterCategory>> GetByAllField(string category);
    }
}
