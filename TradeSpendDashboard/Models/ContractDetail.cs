using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Models
{
    public class ContractDetail
    {
        public long RequestContractId { get; set; }
        public long ProductGroupId { get; set; }
        public string ProductCode { get; set; }
        public string ProductCategory { get; set; }
        public string ProductName { get; set; }
        public decimal PriceBeforeTax { get; set; }
        public decimal PriceAfterTax { get; set; }
        public decimal DirectDiscount { get; set; }
        public decimal DirectReward { get; set; }
        public decimal QuarterRewardPerPeriod { get; set; }
        public decimal SalesPerMonth { get; set; }
        public decimal SalesPerPeriod { get; set; }
        public decimal GuidlineDirectReward { get; set; }
        public decimal QuartertRewardPerMonth { get; set; }
        public decimal PricePerPcs { get; set; }
        public decimal Cr { get; set; }
        public decimal Price { get; set; }
        public decimal GrossProfit { get; set; }
        public decimal QtyPerMonth { get; set; }
        public decimal QtyPer3Month { get; set; }
        public decimal DirectDiscountPerMonth { get; set; }
        public decimal DirectDiscountPerPeriod { get; set; }
        public int PcsPerCtn { get; set; }
        public string ProductGroupName { get; set; }
        public decimal GuidelineDirectDiscount { get; set; }
        public decimal C0001 { get; set; }
        public decimal C0002 { get; set; }
        public decimal C0003 { get; set; }
        public decimal C0004 { get; set; }
        public decimal C0005 { get; set; }
        public decimal C0006 { get; set; }
    }
}
