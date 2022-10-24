using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.DTO
{
    public class ValidationDTO
    {
        public bool Result { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Message { get; set; }
        public long ID { get; set; }

    }
}