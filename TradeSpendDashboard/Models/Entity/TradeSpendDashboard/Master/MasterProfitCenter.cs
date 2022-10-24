using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity
{
    public class MasterProfitCenter : Entity
    {
        public string ProfitCenter { get; set; }
    }
}
