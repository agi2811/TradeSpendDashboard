using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterCategoryMapService : IService<MasterCategoryMapDTO>
    {
        Task<List<dynamic>> GetAllData();
        Task<dynamic> GetByKey(string key);
        Task<List<dynamic>> GetCategoryOption(string search);
        Task<dynamic> GetCategoryOptionById(string search);
        Task<List<dynamic>> GetProfitCenterOptionByCategoryId(long categoryId);
        Task<List<MasterCategoryMap>> GetByAllField(string categoryWeb);
    }
}
