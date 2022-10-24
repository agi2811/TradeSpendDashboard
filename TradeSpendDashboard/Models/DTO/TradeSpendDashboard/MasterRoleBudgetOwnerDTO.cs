using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Model.DTO
{
    public class MasterRoleBudgetOwnerDTO
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long BudgetOwnerId { get; set; }
        public bool IsCategory { get; set; }
        public bool IsProfitCenter { get; set; }
    }
}
