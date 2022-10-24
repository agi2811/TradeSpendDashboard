using TradeSpendDashboard.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Models.DTO
{
    public class PaginatedResultDTO<T>
    {
        private const int defaultPageSize = 10;
        private const int maxPageSize = 50;

        public int totalCount { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public bool hasNext => currentPage < totalPages;
        public bool hasPrevious => currentPage > 1;
        private string orderBy => sorts != null ? string.Join(",", sorts.Select(s => s.orderBy).ToArray()) : "";
        private List<PaginationSort> sorts { get; set; }
        private List<PaginationFilter> filter { get; set; }
        public List<T> result { get; set; }

        internal PaginatedResultDTO(PaginationParam param)
        {
            pageSize = param.pageSize;
            currentPage = param.pageNumber <= 0 ? 1 : param.pageNumber;
            sorts = param.sort;
            filter = param.filter;

            if (param.pageSize <= 0 || param.pageSize > maxPageSize)
            {
                pageSize = defaultPageSize;
            }
        }

        internal async Task<PaginatedResultDTO<T>> paginate(IQueryable<T> queryable)
        {
            queryable = BindFilter(queryable, filter);

            totalCount = queryable.Count();

            totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                queryable = queryable.OrderBy(orderBy);
            }

            result = await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            return this;
        }

        private IQueryable<T> BindFilter(IQueryable<T> queryable, List<PaginationFilter> filter)
        {
            if (filter != null && filter.Count() > 0)
            {
                foreach (var data in filter)
                {
                    if (!string.IsNullOrWhiteSpace(data.filetrField) && !string.IsNullOrWhiteSpace(data.filterOperator))
                    {
                        switch (data.filterOperator.ToLower())
                        {
                            case "startswith":
                                queryable = queryable.Where($"{data.filetrField}.StartsWith(@0)", data.filterValue);
                                break;
                            case "endswith":
                                queryable = queryable.Where($"{data.filetrField}.EndsWith(@0)", data.filterValue);
                                break;
                            case "contains":
                                queryable = queryable.Where($"{data.filetrField}.Contains(@0)", data.filterValue);
                                break;
                            case "notcontains":
                                queryable = queryable.Where($"!{data.filetrField}.Contains(@0)", data.filterValue);
                                break;
                            default:
                                queryable = queryable.Where($"{data.filetrField} {data.filterOperator} @0", data.filterValue);
                                break;
                        }
                    }
                }
            }

            return queryable;
        }
    }
}