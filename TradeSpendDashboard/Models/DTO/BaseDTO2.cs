using System;

namespace TradeSpendDashboard.Models.DTO
{
    public class BaseDTO2
    {
        public long ID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsActive { get; set; }
    }
}