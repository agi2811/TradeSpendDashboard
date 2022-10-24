using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace TradeSpendDashboard.Controllers
{
    public class MasterMenuController : BaseController
    {
        private readonly ILogger<MasterMenuController> _logger;
        AppHelper _app;
        IMasterMenuService _service;

        public MasterMenuController(
            ILogger<MasterMenuController> logger,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu,
            IMasterMenuService service
        ) : base(logger, app, environment, masterMenu)
        {
            _logger = logger;
            _app = app;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetData(long roleId = 0)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _service.GetAllDynamic("");
                if (roleId != 0)
                {
                    data = _service.GetAllByRoleDynamic(roleId);
                }
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> SaveData(MasterMenu param)
        {
            try
            {
                if (param.Id != 0)
                {
                    var cekData = await _service.Get(param.Id);
                    if (cekData != null)
                    {
                        if (param.Id != 0)
                        {
                            var updated = await _service.Update(param);
                            return Ok(new { status = "success", msg = "Update data successfully", result = updated });
                        }
                        return Ok(new { status = "error", result = "User already exists.", data = cekData });
                    }
                    else
                    {
                        var data = await _service.SaveData(param);
                        return Ok(new { status = "success", msg = "Add New User Successfully", result = data });
                    }
                }
                return BadRequest(new { status = "error", result = "Failed to Insert Data", msg = "Empty Usercode." });
            }
            catch (Exception ex)
            {
                throw new Exception("error get all TypeContent.", ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MasterMenu param)
        {
            try
            {
                if (param.Id != 0)
                {
                    var updated = await _service.Delete(param.Id);
                    return Ok(new { status = "success", result = "Delete Data Berhasil" });
                }
                else
                {
                    return BadRequest(new { status = "error", result = "Tidak diizinkan" });
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
                var data = await _service.GetAll();
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> SetMenu(string param)
        {
            try
            {
                List<MasterMenuRole> detail = JsonConvert.DeserializeObject<List<MasterMenuRole>>(param);
                if (detail.Count != 0)
                {
                    var data = _service.SetMenuRole(detail);
                    return Ok(new { status = "success", msg = "Add New User Successfully", result = "" });
                }
                return BadRequest(new { status = "error", result = "Failed to Insert Data", msg = "Empty Usercode." });
            }
            catch (Exception ex)
            {
                throw new Exception("error get all TypeContent.", ex);
            }
        }
    }
}
