using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services
{
    public class MasterMenuService : IMasterMenuService
    {
        private readonly ILogger _logger;
        private readonly AppHelper appHelper;
        private readonly IMasterMenuRepository repository;
        private readonly IMapper mapper;

        public MasterMenuService(
            ILogger<MasterMenuService> logger,
            AppHelper appHelper,
            IMasterMenuRepository repository,
            IMapper mapper
        )
        {
            this._logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterMenu, MasterMenu>();
            });
            mapper = new Mapper(config);

            this.mapper = mapper;
        }

        public async Task<MasterMenu> SaveData(MasterMenu entity)
        {
            try
            {
                var param = entity;
                param.CreatedBy = appHelper.UserName;
                param.CreatedDate = DateTime.Now;
                param.UpdatedBy = appHelper.UserName;
                param.UpdatedDate = DateTime.Now;
                var data = await repository.Add(param);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error create MasterUsers", ex);
            }
        }

        public async Task<List<MasterMenu>> GetAll(string search)
        {
            try
            {
                var data = repository.GetAll();
                var datasearch = data.Where(a => a.Name.ToLower().Contains(search.ToLower())).ToList();
                return datasearch;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterUsers.", ex);
            }
        }

        public async Task<List<MasterMenu>> GetAll(PaginationParam paging)
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterUsers.", ex);
            }
        }

        public async Task<MasterMenu> Get(long Id)
        {
            try
            {
                var data = await repository.Get(Id);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterUsers.", ex);
            }
        }

        public async Task<bool> DeleteMenu(long Id)
        {
            try
            {
                var data = await repository.DeleteMenu(Id);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterUsers.", ex);
            }
        }

        public async Task<MasterMenu> Update(MasterMenu model)
        {
            try
            {
                var existingData = await repository.Get(model.Id);
                if (existingData == null)
                {
                    var data = new MasterMenu();
                    data.Name = model.Name;
                    data.IdParent = model.IdParent;
                    data.Icon = model.Icon;
                    data.IsActive = model.IsActive;
                    data.Sort = model.Sort;
                    data.Url = model.Url;
                    data.CreatedBy = appHelper.UserName;
                    data.CreatedDate = DateTime.Now;
                    data.UpdatedBy = appHelper.UserName;
                    data.UpdatedDate = DateTime.Now;
                    var entity = await repository.Add(data);
                    return entity;
                }
                else
                {
                    var data = existingData;
                    data.Name = model.Name;
                    data.IdParent = model.IdParent;
                    data.Icon = model.Icon;
                    data.IsActive = model.IsActive;
                    data.Sort = model.Sort;
                    data.Url = model.Url;
                    data.UpdatedBy = appHelper.UserName;
                    data.UpdatedDate = DateTime.Now;
                    var entity = await repository.Update(data);
                    return entity;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error update MasterUsers.", ex);
            }
        }

        public List<dynamic> GetAllDynamic(string search)
        {
            var data = repository.GetAllDynamic();
            return data;
        }

        public List<dynamic> GetAllDynamic()
        {
            var data = repository.GetAllDynamic();
            return data;
        }

        Task<PaginatedResult<MasterMenu>> IService<MasterMenu>.GetAll(PaginationParam param)
        {
            throw new NotImplementedException();
        }

        public Task<MasterMenu> Update(long id, MasterMenu entity)
        {
            throw new NotImplementedException();
        }

        public List<dynamic> GetAllByRoleDynamic(long roleId)
        {
            var data = repository.GetAllByRoleDynamic(roleId);
            return data;
        }

        public bool SetMenuRole(List<MasterMenuRole> param)
        {
            try
            {
                var roleId = param.FirstOrDefault().RoleId;               
                foreach (var item in param)
                {
                    item.Id = 0;
                    item.UpdatedBy = appHelper.UserName;
                    item.UpdatedDate = DateTime.Now;
                    item.CreatedBy = appHelper.UserName;
                    item.CreatedDate = DateTime.Now;
                    item.IsActive = item.Create == false && item.Read == false && item.Update == false && item.Delete == false ? false : true;
                    repository.DeleteMenuRoleByRoleMenuId(roleId, item.MenuId);
                    repository.AddMenuRole(item);                   
                }
                return true;
            }
            catch (Exception err)
            {
                return false;
                throw;
            }
        }

        public string GetMenuByRole(long roleId)
        {
            var baseUrl = appHelper.BaseUrl;
            var menu = repository.GenerateMenuHTML(null, 0, "", false, roleId, baseUrl);
            return menu;
        }

        Task<List<MasterMenu>> IService<MasterMenu>.GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<MasterMenu> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<MasterMenu> Add(MasterMenu entity)
        {
            var param = entity;
            param.CreatedBy = appHelper.UserName;
            param.CreatedDate = DateTime.Now;
            param.UpdatedBy = appHelper.UserName;
            param.UpdatedDate = DateTime.Now;
            var data = await repository.Add(param);
            return data;
        }
    }
}
