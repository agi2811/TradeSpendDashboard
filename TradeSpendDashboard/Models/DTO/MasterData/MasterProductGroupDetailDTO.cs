using System;

namespace TradeSpendDashboard.Models.DTO.MasterData
{
    public class MasterProductGroupDetailDTO
    {
        public long ProductGroupId { get; set; }
        public string ProductCode { get; set; }
        public string ProductId { get; set; }
        public string ParentProductName { get; set; }
        public string PriceGroup { get; set; }
        public string ProductName { get; set; }
        public string ProductCode2 { get; set; }

    }
}
