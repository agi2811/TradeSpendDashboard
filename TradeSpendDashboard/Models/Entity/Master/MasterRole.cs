using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Master
{
    public class MasterRole : Entity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public string Name { get; set; }

        [Column(TypeName = "NVARCHAR(10)")]
        public string Code { get; set; }
    }
}
