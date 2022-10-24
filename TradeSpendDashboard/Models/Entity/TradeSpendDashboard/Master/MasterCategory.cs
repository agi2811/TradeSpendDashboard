using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity
{
    public class MasterCategory : Entity
    {
        public string Category { get; set; }
        public long ProfitCenterId { get; set; }
    }
}
