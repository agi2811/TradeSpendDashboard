using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using TradeSpendDashboard.Helper;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterUsersRepository : BaseRepository<MasterUsers>, IMasterUsersRepository
    {
        public MasterUsersRepository(TradeSpendDashboardContext context, IMapper mapper) : base(context)
        {
        }

        public async Task<MasterUsers> Get(string userCode)
        {
            return await TradeSpendDashboardContext.Set<MasterUsers>().Where(w => w.IsActive && w.UserCode == userCode).FirstOrDefaultAsync();
        }

        public List<dynamic> GetUsersAll()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM [dbo].[FN_Get_UserRole]()", param).ToList();
            return dataDynamic;
        }

        public async Task<MasterUsers> Delete(long Id)
        {
            var user = TradeSpendDashboardContext.MasterUsers.Where(a => a.Id.Equals(Id)).FirstOrDefault();
            if (user != null)
            {
                //var listDistributor = await TradeSpendDashboardContext.MasterUsersDistributor.Where(a => a.UserCode.Equals(user.UserCode)).ToListAsync();
                var role = await TradeSpendDashboardContext.MasterUsersRole.Where(a => a.UserCode.Equals(user.UserCode)).FirstOrDefaultAsync();
                TradeSpendDashboardContext.MasterUsers.Remove(user);
                if (role != null) TradeSpendDashboardContext.MasterUsersRole.Remove(role);
                //if (listDistributor != null) TradeSpendDashboardContext.MasterUsersDistributor.RemoveRange(listDistributor);
                TradeSpendDashboardContext.SaveChanges();
            }
            return user;
        }

        public async Task<MasterUsersDTO> GetByUserCode(string UserCode)
        {
            return await TradeSpendDashboardContext.Set<MasterUsersDTO>().Where(w => w.IsActive && w.UserCode == UserCode).FirstOrDefaultAsync();
        }

        public async Task<List<dynamic>> GetCategoryOption(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM dbo.MasterCategory WHERE Category LIKE '%{search}%' AND IsActive=1 ORDER BY Category", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetCategoryOptionById(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM dbo.MasterCategory WHERE Category IN (SELECT TRIM(Value) Category FROM STRING_SPLIT('{search}', ',')) AND IsActive=1", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetProfitCenterOption(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM dbo.MasterProfitCenter WHERE ProfitCenter LIKE '%{search}%' AND IsActive=1 ORDER BY ProfitCenter", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetProfitCenterOptionById(string search)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT * FROM dbo.MasterProfitCenter WHERE ProfitCenter IN (SELECT TRIM(Value) ProfitCenter FROM STRING_SPLIT('{search}', ',')) AND IsActive=1", param).ToList();
            return dataDynamic;
        }

        public async Task<MasterUsers> UpdateSync(MasterUsers entity)
        {
            var dateNow = DateTime.Now;
            var param = new Dictionary<string, object>();
            var request = await TradeSpendDashboardContext.MasterUsers.Where(w => w.UserCode == entity.UserCode).FirstOrDefaultAsync();
            if (request != null)
            {
                request.Id = request.Id;
                request.UserCode = entity.UserCode;
                request.UserName = entity.UserName;
                request.Name = entity.Name;
                request.Email = entity.Email;
                request.CreatedDate = request.CreatedDate;
                request.CreatedBy = request.CreatedBy;
                request.UpdatedDate = dateNow;
                request.UpdatedBy = request.UpdatedBy;
                TradeSpendDashboardContext.MasterUsers.Update(request);
                TradeSpendDashboardContext.SaveChanges();
            }
            return request;
        }

        public async Task<List<MasterUsers>> GetUsersSync()
        {
            return await TradeSpendDashboardContext.Set<MasterUsers>().Where(w => w.IsActive).ToListAsync();
        }
    }
}
