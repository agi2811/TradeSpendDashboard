using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity
{
    public class MasterBudgetOwnerMap : Entity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string ValueTradeSpend { get; set; }

        [Required]
        public long BudgetOwnerId { get; set; }
    }
}
