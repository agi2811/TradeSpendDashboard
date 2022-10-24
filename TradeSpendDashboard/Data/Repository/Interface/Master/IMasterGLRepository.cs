using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using TradeSpendDashboard.Models;
using TradeSpendDashboard.Models.DTO;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterGLRepository : IRepository<MasterGL>
    {
        Task<dynamic> GetByKey(string key);
        Task<dynamic> GetByGLDesc(string desc);
        Task<dynamic> GetByGLDescCode(string desc, string code);
        Task<List<dynamic>> GetGLTypeOption(string search);
        Task<dynamic> GetGLTypeOptionById(string search);
        Task<List<MasterGL>> GetByAllField(string code);
    }
}
