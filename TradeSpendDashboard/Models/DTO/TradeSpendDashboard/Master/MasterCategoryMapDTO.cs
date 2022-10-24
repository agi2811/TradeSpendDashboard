using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Model.DTO
{
    public class MasterCategoryMapDTO : BaseDTO
    {
        public string CategoryWeb { get; set; }
        public long CategoryId { get; set; }
    }
}