using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.ExceptionHandler
{
    public class ResponseUnprocessableEntity
    {
        public int StatusCode { get; set; }
        public List<ValidationModelError> ValidationErrors { get; set; }
        public string StackTrace { get; set; }
        public string Message { get; set; }
    }
}
