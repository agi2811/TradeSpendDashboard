using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using TradeSpendDashboard.Models;
using TradeSpendDashboard.Models.DTO;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Model.DTO;
using System.Linq;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterCategoryMapRepository : IRepository<MasterCategoryMap>
    {
        Task<List<dynamic>> GetAllData();
        Task<dynamic> GetByKey(string key);
        Task<dynamic> GetByCategoryWeb(string category);
        Task<List<dynamic>> GetCategoryOption(string search);
        Task<dynamic> GetCategoryOptionById(string search);
        Task<List<dynamic>> GetProfitCenterOptionByCategoryId(long categoryId);
        Task<List<MasterCategoryMap>> GetByAllField(string categoryWeb);
    }
}
