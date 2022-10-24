namespace TradeSpendDashboard.Models.DTO.MasterData
{
    public class MasterCustomerMapDTO : BaseDTO
    {
        public string Customer { get; set; }
        public string CustomerMap { get; set; }
        public long OldChannelId { get; set; }
        public long NewChannelId { get; set; }
    }
}
