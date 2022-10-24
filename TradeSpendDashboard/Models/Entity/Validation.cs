using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity
{
    public class Validation
    {
        public bool Result { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Message { get; set; }
    }
}
