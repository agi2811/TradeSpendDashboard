namespace TradeSpendDashboard.Models.DTO.Identity
{
    public class IdentityServerMenu
    {
        public long Id { get; set; }

        public long ParentId { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }
    }
}