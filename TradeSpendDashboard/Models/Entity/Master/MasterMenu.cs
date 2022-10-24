using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Master
{
    public class MasterMenu : Entity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public long IdParent { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }
        [Column(TypeName = "NVARCHAR(200)")]
        public string Url { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Icon { get; set; }
        public int Sort { get; set; }

    }
}