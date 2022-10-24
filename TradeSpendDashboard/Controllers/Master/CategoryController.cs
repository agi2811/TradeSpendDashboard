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
    public class CategoryController : BaseController
    {
        private readonly ILogger<CategoryController> _logger;
        AppHelper _app;
        IMasterCategoryService _masterCategoryService;
        IGlobalService _globalService;
        public static string currentColumn = "";

        public CategoryController(
            ILogger<CategoryController> logger,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu,
            IMasterCategoryService masterCategoryService,
            IGlobalService globalService
        ) : base(logger, app, environment, masterMenu)
        {
            _logger = logger;
            _app = app;
            _masterCategoryService = masterCategoryService;
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
                var data = _masterCategoryService.GetAll();
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
        public async Task<IActionResult> Insert(MasterCategoryDTO model)
        {
            try
            {
                var cekData = await _masterCategoryService.GetByAllField(model.Category);
                if (cekData.Any())
                {
                    if (model.Id != 0)
                    {
                        var updated = await _masterCategoryService.Update(model.Id, model);
                        return Ok(new { status = "success", msg = "Update data successfully", result = updated });
                    }
                    return Ok(new { status = "error", result = "Data already exists.", data = cekData });
                }
                else
                {
                    var result = await _masterCategoryService.Add(model);
                    return Json(new { status = "success", result = result, message = "Success", });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Save Data : " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, MasterCategoryDTO model)
        {
            try
            {
                var cekData = await _masterCategoryService.GetByAllField(model.Category);
                var existId = cekData.Any(o => o.Id != id);
                if (!existId)
                {
                    var result = await _masterCategoryService.Update(id, model);
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
                var result = await _masterCategoryService.Delete(id);

                return Json(new { status = "success", result = result, message = "Success", });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Delete Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetProfitCenterOption(string search = "")
        {
            try
            {
                var data = await _masterCategoryService.GetProfitCenterOption(search);
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
                var data = await _masterCategoryService.GetProfitCenterOptionById(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }
    }
}
