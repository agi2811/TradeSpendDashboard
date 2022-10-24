using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Models
{
    public class ContractHeader
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string RequestNumber { get; set; }
        public string DistributorCode { get; set; }
        public string OutletCode { get; set; }
        public long SubChannelId { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public string Status { get; set; }
        public bool isfollow { get; set; }
        public string PengelolaOutlet { get; set; }
        public string groupOutletName { get; set; }
        public int TotalOutlet { get; set; }
        public string TermOfPayment { get; set; }
        public string Notes { get; set; }
        public string DistributorName { get; set; }
        public string OutletName { get; set; }
        public string SubChannelName { get; set; }
    } 
}
