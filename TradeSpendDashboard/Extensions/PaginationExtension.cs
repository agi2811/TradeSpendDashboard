using TradeSpendDashboard.Models.Pagination;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Extensions
{
    public static class PaginationExtension
    {
        public static async Task<PaginatedResult<T>> paginate<T>(this IQueryable<T> source,
                                                PaginationParam param)
        {
            return await new PaginatedResult<T>(param).paginate(source);
        }
    }
}
