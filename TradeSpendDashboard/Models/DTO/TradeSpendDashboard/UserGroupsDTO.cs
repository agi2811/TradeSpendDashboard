using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Model.DTO
{
    public class UserGroupsDTO : BaseDTO2
    {
        public string GroupID { get; set; }
        public string UserCode { get; set; }
    }
}
