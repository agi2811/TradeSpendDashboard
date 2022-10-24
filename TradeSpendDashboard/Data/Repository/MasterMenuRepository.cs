using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Helper;

namespace TradeSpendDashboard.Data.Repository
{
    public class MasterMenuRepository : BaseRepository<MasterUsers>, IMasterMenuRepository
    {
        AppHelper _app;

        public MasterMenuRepository(
            TradeSpendDashboardContext context, 
            IMapper mapper
        ) : base(context)
        {
        }

        public async Task<MasterMenu> Get(long Id)
        {
            return await TradeSpendDashboardContext.Set<MasterMenu>().Where(w => w.IsActive && w.Id == Id).FirstOrDefaultAsync();
        }

        public List<dynamic> GetUsersAll()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql(@"SELECT * FROM [dbo].[VW_Get_User_Role]", param).ToList();
            return dataDynamic;
        }

        public async Task<bool> DeleteMenu(long Id)
        {
            var menu = TradeSpendDashboardContext.MasterMenu.Where(a => a.Id == Id).FirstOrDefault();
            if (menu != null)
            {
                var remove = TradeSpendDashboardContext.MasterMenu.Remove(menu);
                TradeSpendDashboardContext.SaveChanges();
                return true;
            }
            return false;
        }

        IQueryable<MasterMenu> IRepository<MasterMenu>.GetAll()
        {
            return TradeSpendDashboardContext.MasterMenu.ToList().AsQueryable();
        }

        Task<PaginatedResult<MasterMenu>> IRepository<MasterMenu>.GetAll(PaginationParam param)
        {
            throw new System.NotImplementedException();
        }

        Task<MasterMenu> IRepository<MasterMenu>.Get(long id)
        {
            var data = TradeSpendDashboardContext.MasterMenu.Where(a => a.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public Task<MasterMenu> Add(MasterMenu entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<MasterMenu> Update(MasterMenu entity)
        {
            var data = TradeSpendDashboardContext.MasterMenu.Update(entity);
            TradeSpendDashboardContext.SaveChanges();
            var existing = TradeSpendDashboardContext.MasterMenu.Where(a => a.Id == entity.Id).FirstOrDefaultAsync();
            return existing;
        }

        Task<MasterMenu> IRepository<MasterMenu>.Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public List<dynamic> GetAllDynamic()
        {
            var param = new Dictionary<string, object>();
            var data = TradeSpendDashboardContext.CollectionFromSql("Exec [dbo].[SP_Get_Menu_List] 0", param).ToList();
            return data;
        }

        public List<dynamic> GetAllDynamicSearchable()
        {
            var param = new Dictionary<string, object>();
            var data = TradeSpendDashboardContext.CollectionFromSql("Exec [dbo].[SP_Get_Menu_List] 0", param).ToList();
            return data;
        }


        public List<dynamic> GetAllByRoleDynamic(long roleId = 0)
        {
            var param = new Dictionary<string, object>();
            var data = TradeSpendDashboardContext.CollectionFromSql("Exec [dbo].[SP_Get_Menu_List] " + roleId.ToString(), param).ToList();
            return data;
        }

        public bool DeleteMenuRoleByRoleId(long roleId)
        {
            try
            {
                var param = new Dictionary<string, object>();
                var data = TradeSpendDashboardContext.CollectionFromSql("Exec [dbo].[SP_Delete_Menu_Role] " + roleId, param);
                return true;
            }
            catch (Exception err)
            {
                return false;
                throw;
            }
        }

        public bool DeleteMenuRoleByRoleMenuId(long roleId, long menuId)
        {
            try
            {
                var param = new Dictionary<string, object>();
                var data = TradeSpendDashboardContext.CollectionFromSql($"DELETE FROM [dbo].[MasterMenuRole] WHERE RoleId = {roleId} AND MenuId = {menuId}", param);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddMenuRole(MasterMenuRole param)
        {
            try
            {
                if (param.MenuId != 0)
                {
                    var exists = TradeSpendDashboardContext.MasterMenuRole.Where(a => a.MenuId.Equals(param.MenuId) && a.RoleId.Equals(param.RoleId)).FirstOrDefault();
                    if (exists != null)
                    {
                        exists.Read = param.Read;
                        exists.Update = param.Update;
                        exists.Create = param.Create;
                        exists.Delete = param.Delete;
                        exists.UpdatedBy = param.UpdatedBy;
                        exists.UpdatedDate = param.UpdatedDate;
                        var data = TradeSpendDashboardContext.MasterMenuRole.Update(exists);
                    }
                    else
                    {
                        var data = TradeSpendDashboardContext.MasterMenuRole.Add(param);
                    }
                    TradeSpendDashboardContext.SaveChanges();
                    return true;
                }
                else
                {
                    return true;
                    //throw new InvalidOperationException("Data Not Found");
                }
            }
            catch (Exception err)
            {
                return false;
                throw;
            }
        }


        public string GenerateMenuHTML(List<MasterMenu> dataMenu, long Parent = 0, string menu = "", bool haschild = false, long roleId = 0, string baseUrl = "")
        {
            string htm = "";
            //string baseUrl = "";
            if (dataMenu == null)
            {
                var sql = @"Exec [dbo].[SP_Get_Menu_List_By_Role] " + roleId;
                dataMenu = TradeSpendDashboardContext.GetDataFromSqlToList<MasterMenu>(sql);
            }

            if (dataMenu.Count() > 0)
            {
                var dataCurrent = dataMenu.Where(a => a.IdParent == Parent).ToList();
                htm += (Parent == 0) ? "<ul class='sidebar-nav'>" : " <ul id='menu-" + Parent + "' class='sidebar-dropdown list-unstyled collapse'>";
                htm += (Parent == 0) ? "<li class='sidebar-header'>Main Menu</li>" : "";
                foreach (var item in dataCurrent)
                {
                    var child = dataMenu.Where(a => a.IdParent == item.Id).ToList(); //ProductReturnContext.MasterMenu.Where(a => a.IdParent == item.Id).ToList();
                    if (child.Count > 0)
                        haschild = true;
                    else
                        haschild = false;

                    htm += haschild ? "<li class='sidebar-item'>" : "<li>";
                    htm += haschild ? "<a href='#menu-" + item.Id + "' data-toggle='collapse' class='sidebar-link collapsed'>" : "<a href='" + baseUrl + item.Url + "' class='sidebar-link'>";
                    htm += "<i class='" + item.Icon + "'></i>";
                    htm += "<span>" + item.Name + "</span>";
                    //htm += (haschild ? "<span class='align-middle'>" + item.Name + "</span>" : "");
                    htm += "</a>";
                    htm += GenerateMenuHTML(dataMenu, item.Id, htm, haschild, roleId, baseUrl);
                    htm += "</li>";
                }
                htm += (Parent != 0) ? "</ul>" : "";
            }

            return htm;
        }
    }
}
