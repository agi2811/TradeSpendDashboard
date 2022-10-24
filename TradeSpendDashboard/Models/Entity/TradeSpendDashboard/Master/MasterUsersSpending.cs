using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Models.Entity.Master
{
    public class MasterUsersSpending
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public string UserCode { get; set; }

        [Required]
        public long BudgetOwnerId { get; set; }

        [Required]
        public long CategoryId { get; set; }

        [Required]
        public long ProfitCenterId { get; set; }
    }
}
