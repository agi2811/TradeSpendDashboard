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
    public class GLTypeController : BaseController
    {
        private readonly ILogger<GLTypeController> _logger;
        AppHelper _app;
        IMasterGLTypeService _masterGLTypeService;
        IGlobalService _globalService;
        public static string currentColumn = "";

        public GLTypeController(
            ILogger<GLTypeController> logger,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu,
            IMasterGLTypeService masterGLTypeService,
            IGlobalService globalService
        ) : base(logger, app, environment, masterMenu)
        {
            _logger = logger;
            _app = app;
            _masterGLTypeService = masterGLTypeService;
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
                var data = _masterGLTypeService.GetAll();
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
        public async Task<IActionResult> Insert(MasterGLTypeDTO model)
        {
            try
            {
                var cekData = await _masterGLTypeService.GetByAllField(model.Type);
                if (cekData.Any())
                {
                    if (model.Id != 0)
                    {
                        var updated = await _masterGLTypeService.Update(model.Id, model);
                        return Ok(new { status = "success", msg = "Update data successfully", result = updated });
                    }
                    return Ok(new { status = "error", result = "Data already exists.", data = cekData });
                }
                else
                {
                    var result = await _masterGLTypeService.Add(model);
                    return Json(new { status = "success", result = result, message = "Success", });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Save Data : " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(long id, MasterGLTypeDTO model)
        {
            try
            {
                var cekData = await _masterGLTypeService.GetByAllField(model.Type);
                var existId = cekData.Any(o => o.Id != id);
                if (!existId)
                {
                    var result = await _masterGLTypeService.Update(id, model);
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
                var result = await _masterGLTypeService.Delete(id);

                return Json(new { status = "success", result = result, message = "Success", });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Delete Data : " + ex.Message });
            }
        }
    }
}
