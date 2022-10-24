using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity
{
    public class MasterGL : Entity
    {
        public string GLCode { get; set; }
        public string GLName { get; set; }
        public string GLDescription { get; set; }
        public long TypeId { get; set; }
    }
}
