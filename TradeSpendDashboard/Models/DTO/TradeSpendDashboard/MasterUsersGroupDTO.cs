using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Model.DTO
{
    public class MasterUsersGroupDTO
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DNARole { get; set; }
        public long GroupID { get; set; }
        public string GroupName { get; set; }
    }
}
