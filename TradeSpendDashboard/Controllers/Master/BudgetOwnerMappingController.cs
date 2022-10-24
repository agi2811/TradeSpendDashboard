using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TradeSpendDashboard.Model.DTO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace TradeSpendDashboard.Controllers.Master
{
    public class BudgetOwnerMappingController : BaseController
    {
        private readonly ILogger<BudgetOwnerMappingController> _logger;
        AppHelper _app;
        IMasterBudgetOwnerMapService _masterBudgetOwnerMapService;
        IGlobalService _globalService;
        public static string currentColumn = "";

        public BudgetOwnerMappingController(
            ILogger<BudgetOwnerMappingController> logger,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu,
            IMasterBudgetOwnerMapService masterBudgetOwnerMapService,
            IGlobalService globalService
        ) : base(logger, app, environment, masterMenu)
        {
            _logger = logger;
            _app = app;
            _masterBudgetOwnerMapService = masterBudgetOwnerMapService;
            _globalService = globalService;
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
                var data = _masterBudgetOwnerMapService.GetAll();
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Insert(MasterBudgetOwnerMapDTO model)
        {
            try
            {
                var cekData = await _masterBudgetOwnerMapService.GetByAllField(model.ValueTradeSpend);
                if (cekData.Any())
                {
                    if (model.Id != 0)
                    {
                        var updated = await _masterBudgetOwnerMapService.Update(model.Id, model);
                        return Ok(new { status = "success", msg = "Update data successfully", result = updated });
                    }
                    return Ok(new { status = "error", result = "Data already exists.", data = cekData });
                }
                else
                {
                    var result = await _masterBudgetOwnerMapService.Add(model);
                    return Json(new { status = "success", result = result, message = "Success", });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Save Data : " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, MasterBudgetOwnerMapDTO model)
        {
            try
            {
                var cekData = await _masterBudgetOwnerMapService.GetByAllField(model.ValueTradeSpend);
                var existId = cekData.Any(o => o.Id != id);
                if (!existId)
                {
                    var result = await _masterBudgetOwnerMapService.Update(id, model);
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
                var result = await _masterBudgetOwnerMapService.Delete(id);

                return Json(new { status = "success", result = result, message = "Success", });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Delete Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetBudgetOwnerOption(string search = "")
        {
            try
            {
                var data = await _masterBudgetOwnerMapService.GetBudgetOwnerOption(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetBudgetOwnerOptionByRoleId(string search = "")
        {
            try
            {
                var data = await _masterBudgetOwnerMapService.GetBudgetOwnerOptionByRoleId(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetBudgetOwnerOptionById(string search)
        {
            try
            {
                var data = await _masterBudgetOwnerMapService.GetBudgetOwnerOptionById(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }
    }
}
