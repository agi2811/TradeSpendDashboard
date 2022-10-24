using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Master
{
    public class MasterCustomerLevel : Entity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string CustomerLevel1 { get; set; }
        [Required]
        public long CustomerId { get; set; }
    }
}
