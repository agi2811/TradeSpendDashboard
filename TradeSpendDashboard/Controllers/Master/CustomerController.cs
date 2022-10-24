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
    public class CustomerController : BaseController
    {
        private readonly ILogger<CustomerController> _logger;
        AppHelper _app;
        IMasterCustomerService _masterCustomerService;

        public CustomerController(
            ILogger<CustomerController> logger,
            IMasterCustomerService masterCustomerService,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu
        ) : base(logger, app, environment, masterMenu)
        {
            _logger = logger;
            _app = app;
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
                var data = _masterCustomerService.GetAllCustomer();
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
        public async Task<IActionResult> Insert(MasterCustomerMapDTO model)
        {
            try
            {
                var cekData = await _masterCustomerService.GetByAllField(model.Customer);
                if (cekData.Any())
                {
                    if (model.Id != 0)
                    {
                        var updated = await _masterCustomerService.Update(model.Id, model);
                        return Ok(new { status = "success", msg = "Update data successfully", result = updated });
                    }
                    return Ok(new { status = "error", result = "Data already exists.", data = cekData });
                }
                else
                {
                    var result = await _masterCustomerService.Add(model);
                    return Json(new { status = "success", result = result, message = "Success", });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Save Data : " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, MasterCustomerMapDTO model)
        {
            try
            {
                var cekData = await _masterCustomerService.GetByAllField(model.Customer);
                var existId = cekData.Any(o => o.Id != id);
                if (!existId)
                {
                    var result = await _masterCustomerService.Update(id, model);
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
        public async Task<IActionResult> ActivateData(long id)
        {
            try
            {
                var result = await _masterCustomerService.Activate(id);

                return Json(new { status = "success", result = result, message = "Success", });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Activate Data : " + ex.Message });
            }

        }

        public async Task<IActionResult> GetChannelOption(string search = "")
        {
            try
            {
                var data = await _masterCustomerService.GetChannelOption(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetChannelOptionById(string search)
        {
            try
            {
                var data = await _masterCustomerService.GetChannelOptionById(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }
    }
}
