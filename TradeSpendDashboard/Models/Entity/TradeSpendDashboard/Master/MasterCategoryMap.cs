using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity
{
    public class MasterCategoryMap : Entity
    {
        public string CategoryWeb { get; set; }
        public long CategoryId { get; set; }
    }
}
