using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterRoleBudgetOwnerRepository : CustomRepository<MasterRoleBudgetOwner>, IMasterRoleBudgetOwnerRepository
    {
        public MasterRoleBudgetOwnerRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<MasterRoleBudgetOwner> GetDataById(long Id)
        {
            try
            {
                var data = TradeSpendDashboardContext.MasterRoleBudgetOwner.Where(a => a.Id.Equals(Id)).FirstOrDefault();
                return data;
            }
            catch (System.Exception err)
            {
                return null;
                throw;
            }
        }

        public async Task<MasterRoleBudgetOwner> GetDataByRoleBOId(long roleId, long budgetOwnerId)
        {
            try
            {
                var data = TradeSpendDashboardContext.MasterRoleBudgetOwner.Where(a => a.RoleId.Equals(roleId) && a.BudgetOwnerId.Equals(budgetOwnerId)).FirstOrDefault();
                return data;
            }
            catch (System.Exception err)
            {
                return null;
                throw;
            }
        }

        public async Task<MasterRoleBudgetOwner> Add(MasterRoleBudgetOwner param)
        {
            var data = await TradeSpendDashboardContext.MasterRoleBudgetOwner.AddAsync(param);
            TradeSpendDashboardContext.SaveChanges();
            return param;
        }

        public bool DeleteByUserCode(long Id)
        {
            var param = new Dictionary<string, object>();
            var sql = $"DELETE FROM MasterRoleBudgetOwner WHERE Id = '{Id}'";
            TradeSpendDashboardContext.CollectionFromSql(sql, param).ToList();
            return true;
        }
    }
}
