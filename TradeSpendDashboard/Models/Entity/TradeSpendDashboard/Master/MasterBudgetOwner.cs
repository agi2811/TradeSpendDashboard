using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity
{
    public class MasterBudgetOwner : Entity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(200)")]
        public string BudgetOwner { get; set; }
    }
}
