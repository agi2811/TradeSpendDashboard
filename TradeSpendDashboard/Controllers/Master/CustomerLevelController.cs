using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Controllers.Master
{
    public class CustomerLevelController : BaseController
    {
        private readonly ILogger<CustomerLevelController> _logger;
        AppHelper _app;
        IMasterCustomerLevelService _masterCustomerLevelService;
        IMasterCustomerService _masterCustomerService;

        public CustomerLevelController(
            ILogger<CustomerLevelController> logger,
            IMasterCustomerLevelService masterCustomerLevelService,
            IMasterCustomerService masterCustomerService,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu
        ) : base(logger, app, environment, masterMenu)
        {
            _logger = logger;
            _app = app;
            _masterCustomerLevelService = masterCustomerLevelService;
            _masterCustomerService = masterCustomerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetData()
        {
            try
            {
                this._logger.LogInformation("Get Data :");
                var data = _masterCustomerLevelService.GetAllCustomerLevel();
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error : ", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(MasterCustomerLevelDTO model)
        {
            try
            {
                var cekData = await _masterCustomerLevelService.GetByAllField(model.CustomerLevel1);
                if (cekData.Any())
                {
                    if (model.Id != 0)
                    {
                        var updated = await _masterCustomerLevelService.Update(model.Id, model);
                        return Ok(new { status = "success", msg = "Update data successfully", result = updated });
                    }
                    return Ok(new { status = "error", result = "Data already exists.", data = cekData });
                }
                else
                {
                    var result = await _masterCustomerLevelService.Add(model);
                    return Json(new { status = "success", result = result, message = "Success", });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Save Data : " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, MasterCustomerLevelDTO model)
        {
            try
            {
                var cekData = await _masterCustomerLevelService.GetByAllField(model.CustomerLevel1);
                var existId = cekData.Any(o => o.Id != id);
                if (!existId)
                {
                    var result = await _masterCustomerLevelService.Update(id, model);
                    return Json(new { status = "success", result = result, message = "Success", });
                }

                return Ok(new { status = "error", result = "Data already exists.", data = cekData });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Save Data : " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await _masterCustomerLevelService.Delete(id);

                return Json(new { status = "success", result = result, message = "Success", });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Delete Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetCustomerOption(string search = "")
        {
            try
            {
                var data = await _masterCustomerLevelService.GetCustomerOption(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetCustomerOptionById(string search)
        {
            try
            {
                var data = await _masterCustomerLevelService.GetCustomerOptionById(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetOldChannelOptionByCustomerId(string search, long customerId = 0)
        {
            try
            {
                if (customerId == 0)
                {
                    var data = await _masterCustomerService.GetChannelOption(search);
                    return Ok(new { status = "success", result = data });
                }
                else
                {
                    var data = await _masterCustomerLevelService.GetOldChannelOptionByCustomerId(customerId);
                    return Ok(new { status = "success", result = data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetNewChannelOptionByCustomerId(string search, long customerId = 0)
        {
            try
            {
                if (customerId == 0)
                {
                    var data = await _masterCustomerService.GetChannelOption(search);
                    return Ok(new { status = "success", result = data });
                }
                else
                {
                    var data = await _masterCustomerLevelService.GetNewChannelOptionByCustomerId(customerId);
                    return Ok(new { status = "success", result = data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }
    }
}
