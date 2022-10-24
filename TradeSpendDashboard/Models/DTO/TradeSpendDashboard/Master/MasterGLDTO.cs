using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Model.DTO
{
    public class MasterGLDTO : BaseDTO
    {
        public string GLCode { get; set; }
        public string GLName { get; set; }
        public string GLDescription { get; set; }
        public long TypeId { get; set; }
    }
}