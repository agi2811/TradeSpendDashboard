using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterGLService : IService<MasterGLDTO>
    {
        Task<dynamic> GetByKey(string key);
        Task<List<dynamic>> GetGLTypeOption(string search);
        Task<dynamic> GetGLTypeOptionById(string search);
        Task<List<MasterGL>> GetByAllField(string code);
    }
}
