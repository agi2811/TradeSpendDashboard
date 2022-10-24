using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Models.Entity.TradeSpendDashboard.Transaction.Update
{
    public class TradeHeadSpendingUpdate : Entity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(4)")]
        public string Year { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public string Month { get; set; }

        [Required]
        public bool IsLocked { get; set; }
    }
}
