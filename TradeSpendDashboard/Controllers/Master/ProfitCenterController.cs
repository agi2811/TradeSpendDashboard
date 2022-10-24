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
    public class ProfitCenterController : BaseController
    {
        private readonly ILogger<ProfitCenterController> _logger;
        AppHelper _app;
        IMasterProfitCenterService _masterProfitCenterService;
        IGlobalService _globalService;
        public static string currentColumn = "";

        public ProfitCenterController(
            ILogger<ProfitCenterController> logger,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu,
            IMasterProfitCenterService masterProfitCenterService,
            IGlobalService globalService
        ) : base(logger, app, environment, masterMenu)
        {
            _logger = logger;
            _app = app;
            _masterProfitCenterService = masterProfitCenterService;
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
                var data = _masterProfitCenterService.GetAll();
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
        public async Task<IActionResult> Insert(MasterProfitCenterDTO model)
        {
            try
            {
                var cekData = await _masterProfitCenterService.GetByAllField(model.ProfitCenter);
                if (cekData.Any())
                {
                    if (model.Id != 0)
                    {
                        var updated = await _masterProfitCenterService.Update(model.Id, model);
                        return Ok(new { status = "success", msg = "Update data successfully", result = updated });
                    }
                    return Ok(new { status = "error", result = "Data already exists.", data = cekData });
                }
                else
                {
                    var result = await _masterProfitCenterService.Add(model);
                    return Json(new { status = "success", result = result, message = "Success", });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Save Data : " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, MasterProfitCenterDTO model)
        {
            try
            {
                var cekData = await _masterProfitCenterService.GetByAllField(model.ProfitCenter);
                var existId = cekData.Any(o => o.Id != id);
                if (!existId)
                {
                    var result = await _masterProfitCenterService.Update(id, model);
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
                var result = await _masterProfitCenterService.Delete(id);

                return Json(new { status = "success", result = result, message = "Success", });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Delete Data : " + ex.Message });
            }
        }
    }
}
