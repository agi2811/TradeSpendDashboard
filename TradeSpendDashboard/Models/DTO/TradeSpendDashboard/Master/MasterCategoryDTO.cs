﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TradeSpendDashboard.Models.DTO;

namespace TradeSpendDashboard.Model.DTO
{
    public class MasterCategoryDTO : BaseDTO
    {
        public string Category { get; set; }
        public long ProfitCenterId { get; set; }
    }
}