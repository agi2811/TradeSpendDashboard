using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.ExceptionHandler
{
    public class ValidationModelError
    {
        public string Name { get; set; }
        public string Reason { get; set; }
    }
}
