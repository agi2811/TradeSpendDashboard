using System.ComponentModel.DataAnnotations;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Model.DTO
{
    public class GlobalDTO : BaseDTO
    {
        public string Name { get; set; }

        public string CodeFormat { get; set; }

        public string RequestType { get; set; }

    }
}
