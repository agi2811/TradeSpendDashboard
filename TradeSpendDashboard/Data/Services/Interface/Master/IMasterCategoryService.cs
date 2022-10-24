using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterCategoryService : IService<MasterCategoryDTO>
    {
        Task<dynamic> GetByKey(string key);
        Task<List<dynamic>> GetProfitCenterOption(string search);
        Task<dynamic> GetProfitCenterOptionById(string search);
        Task<List<MasterCategory>> GetByAllField(string category);
    }
}
