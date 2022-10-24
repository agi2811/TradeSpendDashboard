using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Models.DTO.MasterData
{
    public class MasterUsersSpendingDTO
    {
        public string UserCode { get; set; }
        public long BudgetOwnerId { get; set; }
        public long CategoryId { get; set; }
        public long ProfitCenterId { get; set; }
    }
}
