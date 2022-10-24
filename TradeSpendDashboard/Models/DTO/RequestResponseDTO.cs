﻿using System;
using System.Collections.Generic;

namespace TradeSpendDashboard.Models.DTO
{
    public class RequestResponseDTO<T>
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public bool isError { get; set; }
        public string responseException { get; set; }
        public List<T> result { get; set; }
    }
}