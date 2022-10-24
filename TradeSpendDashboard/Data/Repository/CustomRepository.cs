using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public abstract class CustomRepository<TEntity> : ICustomRepository<TEntity> 
    {
        public readonly TradeSpendDashboardContext TradeSpendDashboardContext;

        public CustomRepository(TradeSpendDashboardContext context)
        {   
            this.TradeSpendDashboardContext = context;
        }

    }
}
