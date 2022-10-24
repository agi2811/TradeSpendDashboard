using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Master
{
    public class MasterCustomerMap : Entity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Customer { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string CustomerMap { get; set; }
        [Required]
        public long OldChannelId { get; set; }
        [Required]
        public long NewChannelId { get; set; }
    }
}
