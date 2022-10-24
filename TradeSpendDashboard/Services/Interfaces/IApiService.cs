using TradeSpendDashboard.Models.Pagination;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Services
{
    public interface IApiService<TModel>
    {
        Task<RequestResponse<TModel>> GetAsync();
        Task<PaginatedResponse<TModel>> GetAsync(PaginationParam paginationParam);
        Task<RequestResponse<TModel>> GetAsync(long id);
        Task<RequestResponse<TModel>> PostAsync(TModel model);
        Task<RequestResponse<TModel>> PutAsync(long id, TModel model);
        Task<RequestResponse<TModel>> DeleteAsync(long id);
    }
}