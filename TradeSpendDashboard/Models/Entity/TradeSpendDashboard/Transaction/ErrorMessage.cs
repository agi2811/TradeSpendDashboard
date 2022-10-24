using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Transaction
{
    public class ErrorMessage
    {

        public string Status { get; set; }
        public string Message { get; set; }
        public long ID { get; set; }
    }
}
