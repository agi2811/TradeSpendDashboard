using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterRoleRepository : BaseRepository<MasterRole>, IMasterRoleRepository
    {
        public MasterRoleRepository(TradeSpendDashboardContext context) : base(context)
        {
        }

        public async Task<List<dynamic>> GetAllDynamic()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"
                    SELECT 
                            Id RoleId
                            , Name RoleName
                            , Code
                            , IsActive
                            , CreatedBy
                            , CreatedDate
                            , UpdatedBy
                            , UpdatedDate  
                    FROM dbo.MasterRole
                    WHERE IsActive=1
                    ORDER BY Name ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<dynamic> GetRoleOptionById(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"
                    SELECT 
                            Id RoleId
                            , Name RoleName
                            , Code
                            , IsActive
                            , CreatedBy
                            , CreatedDate
                            , UpdatedBy
                            , UpdatedDate  
                    FROM dbo.MasterRole
                    WHERE Id=" + search + @"
                      AND IsActive=1", param).ToList();
            var data = dataDynamic.FirstOrDefault();
            return data;
        }

        public MasterRole GetById(long Id)
        {
            try
            {
                var data = TradeSpendDashboardContext.MasterRole.Where(a => a.Id.Equals(Id)).FirstOrDefault();
                return data;
            }
            catch (System.Exception err)
            {
                return null;
                throw;
            }
        }
    }
}
