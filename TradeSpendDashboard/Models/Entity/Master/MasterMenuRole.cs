using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Master
{
    public class MasterMenuRole : Entity
    {
        [Required]
        public long RoleId { get; set; }

        [Required]
        public long MenuId { get; set; }

        public bool Read { get; set; }

        public bool Create { get; set; }

        public bool Update { get; set; }

        public bool Delete { get; set; }
    }
}
