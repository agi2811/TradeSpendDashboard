using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.DTO;
using TradeSpendDashboard.Models.Entity.Master;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IMasterUsersSpendingRepository : ICustomRepository<MasterUsersSpending>
    {
        Task<dynamic> GetBOByUserCode(string userCode);
        Task<List<dynamic>> GetByUserCode(string userCode);
        Task<MasterUsersSpending> Add(MasterUsersSpending param);
        ValidationDTO SaveUsersSpending(string usercode, long budgetOwnerId, List<int> categoryList, List<int> profitCenterList);
        bool DeleteByUserCode(string userCode);
    }
}
