using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models;

namespace TradeSpendDashboard.Controllers.Report
{
    public class ReportMovement : BaseController
    {
        private readonly ILogger<ReportMovement> _logger;   
        private readonly IMasterMenuService _masterMenuService;
        private readonly IGlobalService _globalService;
        private readonly IReportRepository _reportRepository;
        private readonly IGlobalRepository _globalRepository;
        AppHelper _app;

        public ReportMovement(
            ILogger<ReportMovement> logger,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu,
            IGlobalRepository globalRepository,
            IGlobalService globalService,
            IReportRepository reportRepository
        ) : base(logger, app, environment, masterMenu)
        {
            this._logger = logger;
            this._app = app;
            this._globalService = globalService;
            this._reportRepository = reportRepository;
            this._globalRepository = globalRepository;
        }

        public IActionResult MTDActualMovement()
        {
            var dataCurrentMonth = _globalService.GetCurrentMonth();
            var dataCurrentYear = _globalService.GetCurrentYear();

            ViewBag.CurrentMonth = dataCurrentMonth.Month;
            ViewBag.CurrentYear = dataCurrentYear.Year;

            return View();
        }

        public IActionResult MTDActualMovementFC()
        {
            var dataCurrentMonth = _globalService.GetCurrentMonth();
            var dataCurrentYear = _globalService.GetCurrentYear();

            ViewBag.CurrentMonth = dataCurrentMonth.Month;
            ViewBag.CurrentYear = dataCurrentYear.Year;

            return View();
        }

        public IActionResult Get_MTD_Actual_ProfitCenter(string year, string month, long snapshotID)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _reportRepository.FN_Get_MTD_Actual_ProfitCenter(year, month, snapshotID);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public IActionResult Get_MTD_Budget_ProfitCenter(string year,long snapshotID)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _reportRepository.FN_Get_MTD_Budget_ProfitCenter(year, snapshotID);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public IActionResult  Get_MTD_Outlook_ProfitCenter(string year, string month, long snapshotID)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _reportRepository.FN_Get_MTD_Outlook_ProfitCenter(year, month, snapshotID);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public IActionResult Get_MTD_Variance_BudgetActual(string year, string month, string yearBudget, long snapshotID)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _reportRepository.FN_Get_MTD_Variance_BudgetActual(year, month, yearBudget, snapshotID);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public IActionResult Get_MTD_Variance_OutlookActual(string year, string month, string yearOutlook, string monthOutlook, long snapshotID)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _reportRepository.FN_Get_MTD_Variance_OutlookActual(year, month, yearOutlook, monthOutlook, snapshotID);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public IActionResult GetMTDActualSummaryBudget(string year, string month, string yearBudget, long snapshotID)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _reportRepository.spGetMTDActualSummaryBudget(year, month, yearBudget, snapshotID);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public IActionResult GetMTDActualSummaryOutlook(string year, string month, string yearOutlook, string monthOutlook, long snapshotID)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _reportRepository.spGetMTDActualSummaryOutlook(year, month,yearOutlook,monthOutlook, snapshotID);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public IActionResult SnapshotMTDActual(SnapshotParams sp)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _reportRepository.spSnapshotMTDActual(sp);
                return Ok(new { status = "success", result = "Successfully snapshotted" });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public IActionResult GetFC_vs_Budget(string profitCenter, string source1, string source2, string source1Year, string source1Month, string source2Year, string source2month)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _reportRepository.spGetFC_vs_Budget( profitCenter,  source1, source2, source1Year, source1Month, source2Year, source2month);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public IActionResult GetMTDColNameList(string profitCenter, string source1, string source2)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _reportRepository.fnGetMTDColNameList(profitCenter, source1, source2);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetSnapshotHistory()
        {
            try
            {
                var data = await _reportRepository.fnGetSnapshotHistory();
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        
    }
}
