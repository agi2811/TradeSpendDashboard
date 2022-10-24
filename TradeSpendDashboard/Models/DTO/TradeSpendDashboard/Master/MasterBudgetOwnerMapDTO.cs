using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Model.DTO
{
    public class MasterBudgetOwnerMapDTO : BaseDTO
    {
        public string ValueTradeSpend { get; set; }
        public long BudgetOwnerId { get; set; }
    }
}