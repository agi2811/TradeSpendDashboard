using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Services.MasterData.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using TradeSpendDashboard.Models.Pagination;
using System.Collections.Generic;
using System.Net;

namespace TradeSpendDashboard.Controllers.Master
{
    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        AppHelper _app;
        IUserServices _userServices;
        IMasterUsersService _masterUserServices;
        IMasterUsersRoleService _masterUserRoleServices;
        IMasterUsersSpendingService _masterUsersSpendingServices;

        public UsersController(
            ILogger<UsersController> logger,
            IUserServices userServices,
            IMasterUsersService masterUserServices,
            IMasterUsersRoleService masterUserRoleServices,
            IMasterUsersSpendingService masterUsersSpendingServices,
            IMasterMenuService masterMenu,
            AppHelper app,
            IWebHostEnvironment environment
        ) : base(logger, app, environment, masterMenu)
        {
            _logger = logger;
            _app = app;
            _userServices = userServices;
            _masterUserServices = masterUserServices;
            _masterUserRoleServices = masterUserRoleServices;
            _masterUsersSpendingServices = masterUsersSpendingServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetData()
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _masterUserServices.GetAllDynamic();
                return Ok(new { title = "Success", status = "success", result = data });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { title = "Error", status = "error", result = "Cannot Get Data : " + ex.Message });
            }

        }

        public async Task<IActionResult> GetByUserCode(string search)
        {
            try
            {
                var data = _masterUserServices.GetByUserCode(search);
                return Ok(new { title = "Success", status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { title = "Error", status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveData(MasterUsersRoleDTO param, long budgetOwnerId, List<int> categoryIdList = null, List<int> profitCenterIdList = null)
        {
            try
            {
                if (budgetOwnerId != 0)
                {
                    if (categoryIdList.Count == 0 && profitCenterIdList.Count == 0)
                    {
                        return Ok(new { title = "Error", status = "error", result = "Category Or Profit Center Data Cannot Be Empty." });
                        //return BadRequest(ModelState);
                    }
                }

                if (param.UserCode != "")
                {
                    var cekData = await _masterUserServices.GetByUserCode(param.UserCode);
                    if (cekData != null)
                    {
                        if (param.Id != 0)
                        {
                            var updated = await _masterUserServices.Update(param);

                            if (categoryIdList.Count > 0 || profitCenterIdList.Count > 0)
                            {
                                _masterUsersSpendingServices.SaveUsersSpending(param.UserCode, budgetOwnerId, categoryIdList, profitCenterIdList);
                            }

                            return Ok(new { title = "Success", status = "success", result = "Update Data Successfully", data = updated });
                        }

                        return Ok(new { title = "Error", status = "error", result = "User Already Exists.", data = cekData });
                    }
                    else
                    {
                        var data = await _masterUserServices.SaveData(param);

                        if (categoryIdList.Count > 0 || profitCenterIdList.Count > 0)
                        {
                            _masterUsersSpendingServices.SaveUsersSpending(param.UserCode, budgetOwnerId, categoryIdList, profitCenterIdList);
                        }

                        return Ok(new { title = "Success", status = "success", result = "Add New User Successfully", data = data });
                    }
                }
                return BadRequest(new { title = "Error", status = "error", result = "Failed to Insert Data", msg = "Empty Usercode." });
            }
            catch (Exception ex)
            {
                throw new Exception("Error get all TypeContent.", ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MasterUsersDTO param)
        {
            try
            {
                if (param.Id != 0)
                {
                    var updated = await _masterUserServices.Delete(param.Id);
                    return Ok(new { title = "Success", status = "success", result = "Delete Data Successfully" });
                }
                else
                {
                    return BadRequest(new { title = "Error", status = "error", result = "Not Allowed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error get all TypeContent.", ex);
            }
        }

        public async Task<IActionResult> GetDataRole()
        {
            try
            {
                var data = await _masterUserServices.GetAll();
                return Ok(new { title = "Success", status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { title = "Error", status = "error", result = "Cannot Get Data : " + ex.Message });
            }

        }

        public async Task<IActionResult> GetUsersSpending(string usercode)
        {
            try
            {
                var data = await _masterUsersSpendingServices.GetByUserCode(usercode);
                return Ok(new { title = "Success", status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { title = "Error", status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        //public async Task<IActionResult> SyncronizeUserDistributor(string usercode)
        //{
        //    try
        //    {
        //        var data = await _masterUsersDistributorServices.GenerateMappingUserDistributor(usercode);
        //        return Ok(new { title = "Success", status = "success", result = "Success Update Mapping Distributor User" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { title = "Error", status = "error", result = "Cannot Get Data : " + ex.Message });
        //    }
        //}

        public async Task<IActionResult> SyncronizeAllUserData()
        {
            try
            {
                var data = await _masterUserServices.GenerateMappingAllUserData();
                if (data == true)
                {
                    return Ok(new { title = "Success", status = "success", result = "Success Update Mapping User" });
                }
                else
                {
                    return BadRequest(new { title = "Error", status = "error", result = "Cannot Update Mapping User" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { title = "Error", status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetCategoryOption(string search = "")
        {
            try
            {
                var data = await _masterUserServices.GetCategoryOption(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetCategoryOptionById(string search)
        {
            try
            {
                var data = await _masterUserServices.GetCategoryOptionById(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetProfitCenterOption(string search = "")
        {
            try
            {
                var data = await _masterUserServices.GetProfitCenterOption(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetProfitCenterOptionById(string search)
        {
            try
            {
                var data = await _masterUserServices.GetProfitCenterOptionById(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }
    }
}
