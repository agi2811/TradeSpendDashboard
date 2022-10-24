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
    public class CategoryMappingController : BaseController
    {
        private readonly ILogger<CategoryMappingController> _logger;
        AppHelper _app;
        IMasterCategoryMapService _masterCategoryMapService;
        IMasterCategoryService _masterCategoryService;
        IGlobalService _globalService;
        public static string currentColumn = "";

        public CategoryMappingController(
            ILogger<CategoryMappingController> logger,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu,
            IMasterCategoryMapService masterCategoryMapService,
            IMasterCategoryService masterCategoryService,
            IGlobalService globalService
        ) : base(logger, app, environment, masterMenu)
        {
            _logger = logger;
            _app = app;
            _masterCategoryMapService = masterCategoryMapService;
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
                var data = _masterCategoryMapService.GetAllData();
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
        public async Task<IActionResult> Insert(MasterCategoryMapDTO model)
        {
            try
            {
                var cekData = await _masterCategoryMapService.GetByAllField(model.CategoryWeb);
                if (cekData.Any())
                {
                    if (model.Id != 0)
                    {
                        var updated = await _masterCategoryMapService.Update(model.Id, model);
                        return Ok(new { status = "success", msg = "Update data successfully", result = updated });
                    }
                    return Ok(new { status = "error", result = "Data already exists.", data = cekData });
                }
                else
                {
                    var result = await _masterCategoryMapService.Add(model);
                    return Json(new { status = "success", result = result, message = "Success", });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Save Data : " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, MasterCategoryMapDTO model)
        {
            try
            {
                var cekData = await _masterCategoryMapService.GetByAllField(model.CategoryWeb);
                var existId = cekData.Any(o => o.Id != id);
                if (!existId)
                {
                    var result = await _masterCategoryMapService.Update(id, model);
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
                var result = await _masterCategoryMapService.Delete(id);

                return Json(new { status = "success", result = result, message = "Success", });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Delete Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetCategoryOption(string search = "")
        {
            try
            {
                var data = await _masterCategoryMapService.GetCategoryOption(search);
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
                var data = await _masterCategoryMapService.GetCategoryOptionById(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetProfitCenterOptionByCategoryId(string search, long categoryId = 0)
        {
            try
            {
                if (categoryId == 0)
                {
                    var data = await _masterCategoryService.GetProfitCenterOption(search);
                    return Ok(new { status = "success", result = data });
                }
                else
                {
                    var data = await _masterCategoryMapService.GetProfitCenterOptionByCategoryId(categoryId);
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
