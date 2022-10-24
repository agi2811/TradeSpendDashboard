using TradeSpendDashboard.Models.DTO;
using TradeSpendDashboard.Models.Entity.Transaction;
using TradeSpendDashboard.Models.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IService<TEntity>
    {
        Task<List<TEntity>> GetAll();
        Task<PaginatedResult<TEntity>> GetAll(PaginationParam param);
        Task<TEntity> Get(long id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(long id, TEntity entity);
        Task<TEntity> Delete(long id);
    }
}
