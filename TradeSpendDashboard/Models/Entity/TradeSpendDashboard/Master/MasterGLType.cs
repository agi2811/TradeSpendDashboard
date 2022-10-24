using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity
{
    public class MasterGLType : Entity
    {
        public string Type { get; set; }
    }
}
