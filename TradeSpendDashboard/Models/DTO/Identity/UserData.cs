using System;

namespace TradeSpendDashboard.Models.DTO.Identity
{
    public class UserData
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool IsDeleted { get; set; }
        public string LineManager { get; set; }
        public string Flag { get; set; }
    }
}