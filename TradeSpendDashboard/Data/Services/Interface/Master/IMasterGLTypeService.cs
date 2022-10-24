using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterGLTypeService : IService<MasterGLTypeDTO>
    {
        Task<dynamic> GetByKey(string key);
        Task<List<MasterGLType>> GetByAllField(string channel);
    }
}
