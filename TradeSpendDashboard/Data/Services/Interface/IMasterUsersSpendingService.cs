using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IMasterUsersSpendingService
    {
        Task<dynamic> GetBOByUserCode(string userCode);
        Task<List<dynamic>> GetByUserCode(string userCode);
        ValidationDTO SaveUsersSpending(string userCode, long budgetOwnerId, List<int> categoryList, List<int> profitCenterList);
    }
}
