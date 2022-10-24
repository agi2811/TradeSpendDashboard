using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Master
{
    public class MasterUsersRole : Entity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public string UserCode { get; set; }

        [Required]
        public long RoleId { get; set; }
    }
}
