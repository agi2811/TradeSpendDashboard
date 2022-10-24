using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.DTO
{
    public class BaseDTO
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}