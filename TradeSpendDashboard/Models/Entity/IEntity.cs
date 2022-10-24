using System;

namespace TradeSpendDashboard.Models.Entity
{
    public interface IEntity
    {
        long Id { get; set; }
        bool IsActive { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string UpdatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
