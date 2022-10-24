using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Extensions;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public readonly TradeSpendDashboardContext TradeSpendDashboardContext;

        public BaseRepository(TradeSpendDashboardContext context)
        {
            this.TradeSpendDashboardContext = context;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            TradeSpendDashboardContext.Set<TEntity>().Add(entity);
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(long id)
        {
            var entity = await TradeSpendDashboardContext.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            entity.IsActive = false;
            TradeSpendDashboardContext.Entry(entity).State = EntityState.Modified;
            await TradeSpendDashboardContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Get(long id)
        {
            return await TradeSpendDashboardContext.Set<TEntity>().Where(w => w.IsActive && w.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return TradeSpendDashboardContext.Set<TEntity>().Where(w => w.IsActive == true).AsNoTracking();
        }

        public async Task<PaginatedResult<TEntity>> GetAll(PaginationParam param)
        {
            return await TradeSpendDashboardContext.Set<TEntity>().Where(w => w.IsActive == true).AsNoTracking().paginate(param);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            TradeSpendDashboardContext.Entry(entity).State = EntityState.Modified;
            await TradeSpendDashboardContext.SaveChangesAsync();
            return entity;
        }
    }
}
