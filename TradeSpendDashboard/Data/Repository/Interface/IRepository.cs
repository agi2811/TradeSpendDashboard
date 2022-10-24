using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Pagination;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();
        Task<PaginatedResult<TEntity>> GetAll(PaginationParam param);
        Task<TEntity> Get(long id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(long id);
    }
}
