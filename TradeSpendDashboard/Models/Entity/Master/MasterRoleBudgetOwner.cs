using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeSpendDashboard.Models.Entity.Master
{
    public class MasterRoleBudgetOwner
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long RoleId { get; set; }

        [Required]
        public long BudgetOwnerId { get; set; }
        public bool IsCategory { get; set; }
        public bool IsProfitCenter { get; set; }
    }
}
