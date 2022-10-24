using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Transaction
{
    public class LongIDModel
    {
        public long? ID { get; set; }
    }
    public class IntIDModel
    {
        public int? ID { get; set; }
    }

    public class StringIDModel
    {
        public string? ID { get; set; }
    }

}
