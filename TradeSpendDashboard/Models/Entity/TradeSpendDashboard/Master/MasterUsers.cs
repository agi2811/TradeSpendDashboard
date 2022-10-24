using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Master
{
    public class MasterUsers : Entity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public string UserCode { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(200)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Email { get; set; }
    }
}
