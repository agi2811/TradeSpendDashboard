using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Models.Pagination
{
    public class PaginationApiParam
    {
        public string search { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public int totalRows { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public bool requireTotalCount { get; set; } = false;
        public List<PaginationSort> sort { get; set; }
        public List<PaginationFilter> filter { get; set; }
    }
}
