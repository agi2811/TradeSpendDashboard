using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Data.Repository.Interface.Transaction;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Data.Services.Interface.Transaction;
using TradeSpendDashboard.Helper;

namespace TradeSpendDashboard.Controllers.Transaction
{
    public class UpdateController : BaseController
    {
        private readonly ILogger<UpdateController> _logger;
        private readonly ExcelHelper _excelHelper;
        private readonly IGlobalService _globalService;
        private readonly IUpdateService _updateService;
        private readonly IUpdateRepository _updateRepository;
        private readonly IMasterUsersSpendingService _masterUsersSpendingService;
        AppHelper _app;
        public static string currentColumn = "";

        public UpdateController(
            ILogger<UpdateController> logger,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu,
            IGlobalService globalService,
            IUpdateService updateService,
            IUpdateRepository updateRepository,
            IMasterUsersSpendingService masterUsersSpendingService,
            ExcelHelper excelHelper
        ) : base(logger, app, environment, masterMenu)
        {
            this._logger = logger;
            this._app = app;
            this._excelHelper = excelHelper;
            this._globalService = globalService;
            this._updateService = updateService;
            this._updateRepository = updateRepository;
            this._masterUsersSpendingService = masterUsersSpendingService;
        }

        public IActionResult PrimarySalesUpdate()
        {
            var dataCurrentMonth = _globalService.GetCurrentMonth();
            var dataCurrentYear = _globalService.GetCurrentYear();

            ViewBag.CurrentMonth = dataCurrentMonth.Month;
            ViewBag.CurrentYear = dataCurrentYear.Year;

            return View();
        }

        public IActionResult SecondarySalesUpdate()
        {
            var dataCurrentMonth = _globalService.GetCurrentMonth();
            var dataCurrentYear = _globalService.GetCurrentYear();

            ViewBag.CurrentMonth = dataCurrentMonth.Month;
            ViewBag.CurrentYear = dataCurrentYear.Year;

            return View();
        }

        public async Task<IActionResult> SpendingPhasingUpdate()
        {
            var dataCurrentMonth = _globalService.GetCurrentMonth();
            var dataCurrentYear = _globalService.GetCurrentYear();
            var dataUsersBudgetOwner = await _masterUsersSpendingService.GetBOByUserCode(_app.UserName);
            var dataUsersSpending = await _masterUsersSpendingService.GetByUserCode(_app.UserName);

            ViewBag.CurrentMonth = dataCurrentMonth.Month;
            ViewBag.CurrentYear = dataCurrentYear.Year;

            if (dataUsersSpending.Count != 0)
            {
                ViewBag.BudgetOwner = dataUsersBudgetOwner.BudgetOwner;
                ViewBag.SpendingList = dataUsersSpending;
            }

            return View();
        }

        public IActionResult GetDataPrimarySales(string year, string month)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _updateService.GetPrimarySalesUpdate(year, month);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public IActionResult GetDataSecondarySales(string year, string month)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _updateService.GetSecondarySalesUpdate(year, month);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public IActionResult GetDataSpendingPhasing(string year, string month)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _updateService.GetSpendingPhasingUpdate(year, month);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetDataLockPrimarySales(string year, string month)
        {
            try
            {
                var dataLock = await _updateService.CheckPrimarySalesHeadUpdate(year, month);
                return Ok(new { status = "success", result = dataLock });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetDataLockSecondarySales(string year, string month)
        {
            try
            {
                var dataLock = await _updateService.CheckSecondarySalesHeadUpdate(year, month);
                return Ok(new { status = "success", result = dataLock });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetDataLockSpendingPhasing(string year, string month)
        {
            try
            {
                var dataLock = await _updateService.CheckSpendingPhasingHeadUpdate(year, month);
                return Ok(new { status = "success", result = dataLock });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<FileResult> DownloadTemplatePrimarySales(bool isBulk, string year = "", string month = "")
        {
            try
            {
                ExcelHelper excelHelper = new ExcelHelper();
                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1;

                var dataCurrentDate = _globalService.GetCurrentDate();
                var Period = Convert.ToDateTime(dataCurrentDate.Date);

                sheet1 = workbook.CreateSheet("PrimarySales");
                var header1 = sheet1.CreateRow(0);
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "Period", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Profit Center", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(3), "string", "Category", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(4), "string", "Jan", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(5), "string", "Feb", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(6), "string", "Mar", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(7), "string", "Apr", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(8), "string", "May", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(9), "string", "Jun", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(10), "string", "Jul", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(11), "string", "Aug", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(12), "string", "Sep", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(13), "string", "Oct", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(14), "string", "Nov", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(15), "string", "Dec", "#DCE6F1", true, "center");

                var cellStyleForLocked = workbook.CreateCellStyle();
                cellStyleForLocked.IsLocked = false;

                var dataMappUpdate = await _updateService.GetPrimarySalesUpdate(year, month);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataMappUpdate.Count > 0)
                {
                    foreach (var row in dataMappUpdate)
                    {
                        var rows1 = sheet1.CreateRow(startnum1);
                        rows1.CreateCell(0).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Period") ? row.Period : "");
                        rows1.CreateCell(1).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Channel") ? row.Channel : "");
                        rows1.CreateCell(2).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "ProfitCenter") ? row.ProfitCenter : "");
                        rows1.CreateCell(3).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Category") ? row.Category : "");
                        rows1.CreateCell(4).CellStyle = cellStyleForLocked; rows1.GetCell(4).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Jan") ? Convert.ToString(row.Jan) : 0)));
                        rows1.CreateCell(5).CellStyle = cellStyleForLocked; rows1.GetCell(5).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Feb") ? Convert.ToString(row.Feb) : 0)));
                        rows1.CreateCell(6).CellStyle = cellStyleForLocked; rows1.GetCell(6).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Mar") ? Convert.ToString(row.Mar) : 0)));
                        rows1.CreateCell(7).CellStyle = cellStyleForLocked; rows1.GetCell(7).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Apr") ? Convert.ToString(row.Apr) : 0)));
                        rows1.CreateCell(8).CellStyle = cellStyleForLocked; rows1.GetCell(8).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "May") ? Convert.ToString(row.May) : 0)));
                        rows1.CreateCell(9).CellStyle = cellStyleForLocked; rows1.GetCell(9).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Jun") ? Convert.ToString(row.Jun) : 0)));
                        rows1.CreateCell(10).CellStyle = cellStyleForLocked; rows1.GetCell(10).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Jul") ? Convert.ToString(row.Jul) : 0)));
                        rows1.CreateCell(11).CellStyle = cellStyleForLocked; rows1.GetCell(11).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Aug") ? Convert.ToString(row.Aug) : 0)));
                        rows1.CreateCell(12).CellStyle = cellStyleForLocked; rows1.GetCell(12).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Sep") ? Convert.ToString(row.Sep) : 0)));
                        rows1.CreateCell(13).CellStyle = cellStyleForLocked; rows1.GetCell(13).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Oct") ? Convert.ToString(row.Oct) : 0)));
                        rows1.CreateCell(14).CellStyle = cellStyleForLocked; rows1.GetCell(14).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Nov") ? Convert.ToString(row.Nov) : 0)));
                        rows1.CreateCell(15).CellStyle = cellStyleForLocked; rows1.GetCell(15).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Dec") ? Convert.ToString(row.Dec) : 0)));

                        ICellStyle cellStyle = workbook.CreateCellStyle();
                        cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
                        cellStyle.FillPattern = FillPattern.SparseDots;
                        rows1.GetCell(0).CellStyle = cellStyle;
                        rows1.GetCell(1).CellStyle = cellStyle;
                        rows1.GetCell(2).CellStyle = cellStyle;
                        rows1.GetCell(3).CellStyle = cellStyle;
                        startnum1++;
                    }
                }

                sheet1.SetColumnWidth(0, 4000);
                sheet1.SetColumnWidth(1, 8000);
                sheet1.SetColumnWidth(2, 3800);
                sheet1.SetColumnWidth(3, 3800);

                for (int i = 4; i <= 15; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                sheet1.ProtectSheet("P@ssw0rd123!");
                workbook.LockStructure();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "PrimarySales_Update " + Period.ToString("yyyy-MMM-dd") + ".xlsx");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<FileResult> DownloadTemplatePrimarySalesFailed(string year = "", string month = "")
        {
            try
            {
                var usercode = _app.UserName;
                ExcelHelper excelHelper = new ExcelHelper();
                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1;

                var dataCurrentDate = _globalService.GetCurrentDate();
                var Period = Convert.ToDateTime(dataCurrentDate.Date);

                sheet1 = workbook.CreateSheet("PrimarySales");
                var header1 = sheet1.CreateRow(0);
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "Period", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Profit Center", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(3), "string", "Category", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(4), "string", "Jan", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(5), "string", "Feb", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(6), "string", "Mar", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(7), "string", "Apr", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(8), "string", "May", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(9), "string", "Jun", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(10), "string", "Jul", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(11), "string", "Aug", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(12), "string", "Sep", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(13), "string", "Oct", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(14), "string", "Nov", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(15), "string", "Dec", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(16), "string", "Status", "#DCE6F1", true, "center");

                var cellStyleForLocked = workbook.CreateCellStyle();
                cellStyleForLocked.IsLocked = false;

                var dataTempUpdate = await _updateRepository.GetTempMappingPrimarySalesUpdate(usercode, year, month);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataTempUpdate.Count > 0)
                {
                    foreach (var row in dataTempUpdate)
                    {
                        var rows1 = sheet1.CreateRow(startnum1);
                        rows1.CreateCell(0).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "YearPeriod") ? row.YearPeriod + " - " + row.MonthPeriod : "");
                        rows1.CreateCell(1).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Channel") ? row.Channel : "");
                        rows1.CreateCell(2).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "ProfitCenter") ? row.ProfitCenter : "");
                        rows1.CreateCell(3).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Category") ? row.Category : "");
                        rows1.CreateCell(4).CellStyle = cellStyleForLocked; rows1.GetCell(4).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jan") ? float.Parse(row.Jan) : 0);
                        rows1.CreateCell(5).CellStyle = cellStyleForLocked; rows1.GetCell(5).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Feb") ? float.Parse(row.Feb) : 0);
                        rows1.CreateCell(6).CellStyle = cellStyleForLocked; rows1.GetCell(6).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Mar") ? float.Parse(row.Mar) : 0);
                        rows1.CreateCell(7).CellStyle = cellStyleForLocked; rows1.GetCell(7).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Apr") ? float.Parse(row.Apr) : 0);
                        rows1.CreateCell(8).CellStyle = cellStyleForLocked; rows1.GetCell(8).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "May") ? float.Parse(row.May) : 0);
                        rows1.CreateCell(9).CellStyle = cellStyleForLocked; rows1.GetCell(9).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jun") ? float.Parse(row.Jun) : 0);
                        rows1.CreateCell(10).CellStyle = cellStyleForLocked; rows1.GetCell(10).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jul") ? float.Parse(row.Jul) : 0);
                        rows1.CreateCell(11).CellStyle = cellStyleForLocked; rows1.GetCell(11).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Aug") ? float.Parse(row.Aug) : 0);
                        rows1.CreateCell(12).CellStyle = cellStyleForLocked; rows1.GetCell(12).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Sep") ? float.Parse(row.Sep) : 0);
                        rows1.CreateCell(13).CellStyle = cellStyleForLocked; rows1.GetCell(13).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Oct") ? float.Parse(row.Oct) : 0);
                        rows1.CreateCell(14).CellStyle = cellStyleForLocked; rows1.GetCell(14).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Nov") ? float.Parse(row.Nov) : 0);
                        rows1.CreateCell(15).CellStyle = cellStyleForLocked; rows1.GetCell(15).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Dec") ? float.Parse(row.Dec) : 0);
                        rows1.CreateCell(16).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Status") ? row.Status : "");

                        ICellStyle cellStyle = workbook.CreateCellStyle();
                        cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
                        cellStyle.FillPattern = FillPattern.SparseDots;
                        rows1.GetCell(0).CellStyle = cellStyle;
                        rows1.GetCell(1).CellStyle = cellStyle;
                        rows1.GetCell(2).CellStyle = cellStyle;
                        rows1.GetCell(3).CellStyle = cellStyle;
                        rows1.GetCell(16).CellStyle = cellStyle;
                        startnum1++;
                    }
                }

                sheet1.SetColumnWidth(0, 4000);
                sheet1.SetColumnWidth(1, 8000);
                sheet1.SetColumnWidth(2, 3800);
                sheet1.SetColumnWidth(3, 3800);
                sheet1.SetColumnWidth(16, 3800);

                for (int i = 4; i <= 15; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                sheet1.ProtectSheet("P@ssw0rd123!");
                workbook.LockStructure();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "PrimarySales_Update " + Period.ToString("yyyy-MMM-dd") + "_Failed.xlsx");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<FileResult> DownloadTemplateSecondarySales(bool isBulk, string year = "", string month = "")
        {
            try
            {
                ExcelHelper excelHelper = new ExcelHelper();
                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1;

                var dataCurrentDate = _globalService.GetCurrentDate();
                var Period = Convert.ToDateTime(dataCurrentDate.Date);

                sheet1 = workbook.CreateSheet("SecondarySales");
                var header1 = sheet1.CreateRow(0);
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "Period", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "New Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Old Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(3), "string", "Customer", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(4), "string", "Category", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(5), "string", "Jan", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(6), "string", "Feb", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(7), "string", "Mar", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(8), "string", "Apr", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(9), "string", "May", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(10), "string", "Jun", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(11), "string", "Jul", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(12), "string", "Aug", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(13), "string", "Sep", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(14), "string", "Oct", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(15), "string", "Nov", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(16), "string", "Dec", "#DCE6F1", true, "center");

                var cellStyleForLocked = workbook.CreateCellStyle();
                cellStyleForLocked.IsLocked = false;

                var dataMappUpdate = await _updateService.GetSecondarySalesUpdate(year, month);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataMappUpdate.Count > 0)
                {
                    foreach (var row in dataMappUpdate)
                    {
                        var rows1 = sheet1.CreateRow(startnum1);
                        rows1.CreateCell(0).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Period") ? row.Period : "");
                        rows1.CreateCell(1).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "NewChannel") ? row.NewChannel : "");
                        rows1.CreateCell(2).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "OldChannel") ? row.OldChannel : "");
                        rows1.CreateCell(3).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Customer") ? row.Customer : "");
                        rows1.CreateCell(4).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Category") ? row.Category : "");
                        rows1.CreateCell(5).CellStyle = cellStyleForLocked; rows1.GetCell(5).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Jan") ? Convert.ToString(row.Jan) : 0)));
                        rows1.CreateCell(6).CellStyle = cellStyleForLocked; rows1.GetCell(6).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Feb") ? Convert.ToString(row.Feb) : 0)));
                        rows1.CreateCell(7).CellStyle = cellStyleForLocked; rows1.GetCell(7).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Mar") ? Convert.ToString(row.Mar) : 0)));
                        rows1.CreateCell(8).CellStyle = cellStyleForLocked; rows1.GetCell(8).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Apr") ? Convert.ToString(row.Apr) : 0)));
                        rows1.CreateCell(9).CellStyle = cellStyleForLocked; rows1.GetCell(9).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "May") ? Convert.ToString(row.May) : 0)));
                        rows1.CreateCell(10).CellStyle = cellStyleForLocked; rows1.GetCell(10).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Jun") ? Convert.ToString(row.Jun) : 0)));
                        rows1.CreateCell(11).CellStyle = cellStyleForLocked; rows1.GetCell(11).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Jul") ? Convert.ToString(row.Jul) : 0)));
                        rows1.CreateCell(12).CellStyle = cellStyleForLocked; rows1.GetCell(12).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Aug") ? Convert.ToString(row.Aug) : 0)));
                        rows1.CreateCell(13).CellStyle = cellStyleForLocked; rows1.GetCell(13).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Sep") ? Convert.ToString(row.Sep) : 0)));
                        rows1.CreateCell(14).CellStyle = cellStyleForLocked; rows1.GetCell(14).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Oct") ? Convert.ToString(row.Oct) : 0)));
                        rows1.CreateCell(15).CellStyle = cellStyleForLocked; rows1.GetCell(15).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Nov") ? Convert.ToString(row.Nov) : 0)));
                        rows1.CreateCell(16).CellStyle = cellStyleForLocked; rows1.GetCell(16).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Dec") ? Convert.ToString(row.Dec) : 0)));

                        ICellStyle cellStyle = workbook.CreateCellStyle();
                        cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
                        cellStyle.FillPattern = FillPattern.SparseDots;
                        rows1.GetCell(0).CellStyle = cellStyle;
                        rows1.GetCell(1).CellStyle = cellStyle;
                        rows1.GetCell(2).CellStyle = cellStyle;
                        rows1.GetCell(3).CellStyle = cellStyle;
                        rows1.GetCell(4).CellStyle = cellStyle;
                        startnum1++;
                    }
                }

                sheet1.SetColumnWidth(0, 4000);
                sheet1.SetColumnWidth(1, 8000);
                sheet1.SetColumnWidth(2, 4500);
                sheet1.SetColumnWidth(3, 4000);
                sheet1.SetColumnWidth(4, 3800);

                for (int i = 5; i <= 16; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                sheet1.ProtectSheet("P@ssw0rd123!");
                workbook.LockStructure();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "SecondarySales_Update " + Period.ToString("yyyy-MMM-dd") + ".xlsx");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<FileResult> DownloadTemplateSecondarySalesFailed(string year = "", string month = "")
        {
            try
            {
                var usercode = _app.UserName;
                ExcelHelper excelHelper = new ExcelHelper();
                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1;

                var dataCurrentDate = _globalService.GetCurrentDate();
                var Period = Convert.ToDateTime(dataCurrentDate.Date);

                sheet1 = workbook.CreateSheet("SecondarySales");
                var header1 = sheet1.CreateRow(0);
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "Period", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "New Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Old Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(3), "string", "Customer", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(4), "string", "Category", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(5), "string", "Jan", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(6), "string", "Feb", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(7), "string", "Mar", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(8), "string", "Apr", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(9), "string", "May", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(10), "string", "Jun", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(11), "string", "Jul", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(12), "string", "Aug", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(13), "string", "Sep", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(14), "string", "Oct", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(15), "string", "Nov", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(16), "string", "Dec", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(17), "string", "Status", "#DCE6F1", true, "center");

                var cellStyleForLocked = workbook.CreateCellStyle();
                cellStyleForLocked.IsLocked = false;

                var dataTempUpdate = await _updateRepository.GetTempMappingSecondarySalesUpdate(usercode, year, month);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataTempUpdate.Count > 0)
                {
                    foreach (var row in dataTempUpdate)
                    {
                        var rows1 = sheet1.CreateRow(startnum1);
                        rows1.CreateCell(0).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "YearPeriod") ? row.YearPeriod + " - " + row.MonthPeriod : "");
                        rows1.CreateCell(1).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "NewChannel") ? row.NewChannel : "");
                        rows1.CreateCell(2).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "OldChannel") ? row.OldChannel : "");
                        rows1.CreateCell(3).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Customer") ? row.Customer : "");
                        rows1.CreateCell(4).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Category") ? row.Category : "");
                        rows1.CreateCell(5).CellStyle = cellStyleForLocked; rows1.GetCell(5).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jan") ? float.Parse(row.Jan) : 0);
                        rows1.CreateCell(6).CellStyle = cellStyleForLocked; rows1.GetCell(6).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Feb") ? float.Parse(row.Feb) : 0);
                        rows1.CreateCell(7).CellStyle = cellStyleForLocked; rows1.GetCell(7).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Mar") ? float.Parse(row.Mar) : 0);
                        rows1.CreateCell(8).CellStyle = cellStyleForLocked; rows1.GetCell(8).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Apr") ? float.Parse(row.Apr) : 0);
                        rows1.CreateCell(9).CellStyle = cellStyleForLocked; rows1.GetCell(9).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "May") ? float.Parse(row.May) : 0);
                        rows1.CreateCell(10).CellStyle = cellStyleForLocked; rows1.GetCell(10).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jun") ? float.Parse(row.Jun) : 0);
                        rows1.CreateCell(11).CellStyle = cellStyleForLocked; rows1.GetCell(11).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jul") ? float.Parse(row.Jul) : 0);
                        rows1.CreateCell(12).CellStyle = cellStyleForLocked; rows1.GetCell(12).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Aug") ? float.Parse(row.Aug) : 0);
                        rows1.CreateCell(13).CellStyle = cellStyleForLocked; rows1.GetCell(13).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Sep") ? float.Parse(row.Sep) : 0);
                        rows1.CreateCell(14).CellStyle = cellStyleForLocked; rows1.GetCell(14).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Oct") ? float.Parse(row.Oct) : 0);
                        rows1.CreateCell(15).CellStyle = cellStyleForLocked; rows1.GetCell(15).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Nov") ? float.Parse(row.Nov) : 0);
                        rows1.CreateCell(16).CellStyle = cellStyleForLocked; rows1.GetCell(16).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Dec") ? float.Parse(row.Dec) : 0);
                        rows1.CreateCell(17).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Status") ? row.Status : "");

                        ICellStyle cellStyle = workbook.CreateCellStyle();
                        cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
                        cellStyle.FillPattern = FillPattern.SparseDots;
                        rows1.GetCell(0).CellStyle = cellStyle;
                        rows1.GetCell(1).CellStyle = cellStyle;
                        rows1.GetCell(2).CellStyle = cellStyle;
                        rows1.GetCell(3).CellStyle = cellStyle;
                        rows1.GetCell(4).CellStyle = cellStyle;
                        rows1.GetCell(17).CellStyle = cellStyle;
                        startnum1++;
                    }
                }

                sheet1.SetColumnWidth(0, 4000);
                sheet1.SetColumnWidth(1, 8000);
                sheet1.SetColumnWidth(2, 4500);
                sheet1.SetColumnWidth(3, 4000);
                sheet1.SetColumnWidth(4, 3800);
                sheet1.SetColumnWidth(17, 3800);

                for (int i = 5; i <= 16; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                sheet1.ProtectSheet("P@ssw0rd123!");
                workbook.LockStructure();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "SecondarySales_Update " + Period.ToString("yyyy-MMM-dd") + "_Failed.xlsx");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<FileResult> DownloadTemplateSpendingPhasing(bool isBulk, string year = "", string month = "",
            string budgetOwner = "", List<string> categoryList = null, List<string> profitCenterList = null)
        {
            try
            {
                ExcelHelper excelHelper = new ExcelHelper();
                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1;

                var dataCurrentDate = _globalService.GetCurrentDate();
                var Period = Convert.ToDateTime(dataCurrentDate.Date);

                sheet1 = workbook.CreateSheet("SpendingPhasing");
                var header1 = sheet1.CreateRow(0);
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "Period", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "Year", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Budget Owner", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(3), "string", "Profit Center", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(4), "string", "Category", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(5), "string", "GL", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(6), "string", "GL Desc", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(7), "string", "GL Type", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(8), "string", "New Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(9), "string", "Customer", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(10), "string", "Old Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(11), "string", "MG3*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(12), "string", "MG4*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(13), "string", "Activity*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(14), "string", "Jan", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(15), "string", "Feb", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(16), "string", "Mar", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(17), "string", "Apr", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(18), "string", "May", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(19), "string", "Jun", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(20), "string", "Jul", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(21), "string", "Aug", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(22), "string", "Sep", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(23), "string", "Oct", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(24), "string", "Nov", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(25), "string", "Dec", "#DCE6F1", true, "center");

                var cellStyleForLocked = workbook.CreateCellStyle();
                cellStyleForLocked.IsLocked = false;

                var dataMappUpdate = await _updateService.DownloadSpendingPhasingUpdate(year, month, budgetOwner, categoryList, profitCenterList);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataMappUpdate.Count > 0)
                {
                    foreach (var row in dataMappUpdate)
                    {
                        var rows1 = sheet1.CreateRow(startnum1);
                        rows1.CreateCell(0).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Period") ? row.Period : "");
                        rows1.CreateCell(1).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Year") ? row.Year : "");
                        rows1.CreateCell(2).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "BudgetOwner") ? row.BudgetOwner : "");
                        rows1.CreateCell(3).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "ProfitCenter") ? row.ProfitCenter : "");
                        rows1.CreateCell(4).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Category") ? row.Category : "");
                        rows1.CreateCell(5).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "GLCode") ? row.GLCode : "");
                        rows1.CreateCell(6).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "GLDesc") ? row.GLDesc : "");
                        rows1.CreateCell(7).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "GLType") ? row.GLType : "");
                        rows1.CreateCell(8).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "NewChannel") ? row.NewChannel : "");
                        rows1.CreateCell(9).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Customer") ? row.Customer : "");
                        rows1.CreateCell(10).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "OldChannel") ? row.OldChannel : "");
                        rows1.CreateCell(11).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "MG3") ? row.MG3 : "");
                        rows1.CreateCell(12).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "MG4") ? row.MG4 : "");
                        rows1.CreateCell(13).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Activity") ? row.Activity : "");
                        rows1.CreateCell(14).CellStyle = cellStyleForLocked; rows1.GetCell(14).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Jan") ? Convert.ToString(row.Jan) : 0)));
                        rows1.CreateCell(15).CellStyle = cellStyleForLocked; rows1.GetCell(15).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Feb") ? Convert.ToString(row.Feb) : 0)));
                        rows1.CreateCell(16).CellStyle = cellStyleForLocked; rows1.GetCell(16).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Mar") ? Convert.ToString(row.Mar) : 0)));
                        rows1.CreateCell(17).CellStyle = cellStyleForLocked; rows1.GetCell(17).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Apr") ? Convert.ToString(row.Apr) : 0)));
                        rows1.CreateCell(18).CellStyle = cellStyleForLocked; rows1.GetCell(18).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "May") ? Convert.ToString(row.May) : 0)));
                        rows1.CreateCell(19).CellStyle = cellStyleForLocked; rows1.GetCell(19).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Jun") ? Convert.ToString(row.Jun) : 0)));
                        rows1.CreateCell(20).CellStyle = cellStyleForLocked; rows1.GetCell(20).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Jul") ? Convert.ToString(row.Jul) : 0)));
                        rows1.CreateCell(21).CellStyle = cellStyleForLocked; rows1.GetCell(21).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Aug") ? Convert.ToString(row.Aug) : 0)));
                        rows1.CreateCell(22).CellStyle = cellStyleForLocked; rows1.GetCell(22).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Sep") ? Convert.ToString(row.Sep) : 0)));
                        rows1.CreateCell(23).CellStyle = cellStyleForLocked; rows1.GetCell(23).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Oct") ? Convert.ToString(row.Oct) : 0)));
                        rows1.CreateCell(24).CellStyle = cellStyleForLocked; rows1.GetCell(24).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Nov") ? Convert.ToString(row.Nov) : 0)));
                        rows1.CreateCell(25).CellStyle = cellStyleForLocked; rows1.GetCell(25).SetCellValue(float.Parse((string)(DynamicCollectionHelper.CheckVariable(row, "Dec") ? Convert.ToString(row.Dec) : 0)));

                        ICellStyle cellStyle = workbook.CreateCellStyle();
                        cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
                        cellStyle.FillPattern = FillPattern.SparseDots;
                        rows1.GetCell(0).CellStyle = cellStyle;
                        rows1.GetCell(1).CellStyle = cellStyle;
                        rows1.GetCell(2).CellStyle = cellStyle;
                        rows1.GetCell(3).CellStyle = cellStyle;
                        rows1.GetCell(4).CellStyle = cellStyle;
                        rows1.GetCell(5).CellStyle = cellStyle;
                        rows1.GetCell(6).CellStyle = cellStyle;
                        rows1.GetCell(7).CellStyle = cellStyle;
                        rows1.GetCell(8).CellStyle = cellStyle;
                        rows1.GetCell(9).CellStyle = cellStyle;
                        rows1.GetCell(10).CellStyle = cellStyle;
                        rows1.GetCell(11).CellStyle = cellStyle;
                        rows1.GetCell(12).CellStyle = cellStyle;
                        rows1.GetCell(13).CellStyle = cellStyle;
                        startnum1++;
                    }
                }

                sheet1.SetColumnWidth(0, 4000);
                sheet1.SetColumnWidth(1, 3000);
                sheet1.SetColumnWidth(2, 3800);
                sheet1.SetColumnWidth(3, 3800);
                sheet1.SetColumnWidth(4, 3800);
                sheet1.SetColumnWidth(5, 3500);
                sheet1.SetColumnWidth(6, 8000);
                sheet1.SetColumnWidth(7, 3500);
                sheet1.SetColumnWidth(8, 8000);
                sheet1.SetColumnWidth(9, 4000);
                sheet1.SetColumnWidth(10, 4500);
                sheet1.SetColumnWidth(11, 3500);
                sheet1.SetColumnWidth(12, 3500);
                sheet1.SetColumnWidth(13, 3500);

                for (int i = 14; i <= 25; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                sheet1.ProtectSheet("P@ssw0rd123!");
                workbook.LockStructure();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "SpendingPhasing_Update " + Period.ToString("yyyy-MMM-dd") + ".xlsx");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<FileResult> DownloadTemplateSpendingPhasingFailed(string year = "", string month = "")
        {
            try
            {
                var usercode = _app.UserName;
                ExcelHelper excelHelper = new ExcelHelper();
                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1;

                var dataCurrentDate = _globalService.GetCurrentDate();
                var Period = Convert.ToDateTime(dataCurrentDate.Date);

                sheet1 = workbook.CreateSheet("SpendingPhasing");
                var header1 = sheet1.CreateRow(0);
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "Period", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "Year", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Budget Owner", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(3), "string", "Profit Center", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(4), "string", "Category", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(5), "string", "GL", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(6), "string", "GL Desc", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(7), "string", "GL Type", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(8), "string", "New Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(9), "string", "Customer", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(10), "string", "Old Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(11), "string", "MG3*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(12), "string", "MG4*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(13), "string", "Activity*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(14), "string", "Jan", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(15), "string", "Feb", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(16), "string", "Mar", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(17), "string", "Apr", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(18), "string", "May", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(19), "string", "Jun", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(20), "string", "Jul", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(21), "string", "Aug", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(22), "string", "Sep", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(23), "string", "Oct", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(24), "string", "Nov", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(25), "string", "Dec", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(26), "string", "Status", "#DCE6F1", true, "center");

                var cellStyleForLocked = workbook.CreateCellStyle();
                cellStyleForLocked.IsLocked = false;

                var dataTempUpdate = await _updateRepository.GetTempMappingSpendingPhasingUpdate(usercode, year, month);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataTempUpdate.Count > 0)
                {
                    foreach (var row in dataTempUpdate)
                    {
                        var rows1 = sheet1.CreateRow(startnum1);
                        rows1.CreateCell(0).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "YearPeriod") ? row.YearPeriod + " - " + row.MonthPeriod : "");
                        rows1.CreateCell(1).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Year") ? row.Year : "");
                        rows1.CreateCell(2).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "BudgetOwner") ? row.BudgetOwner : "");
                        rows1.CreateCell(3).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "ProfitCenter") ? row.ProfitCenter : "");
                        rows1.CreateCell(4).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Category") ? row.Category : "");
                        rows1.CreateCell(5).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "GL") ? row.GL : "");
                        rows1.CreateCell(6).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "GLDesc") ? row.GLDesc : "");
                        rows1.CreateCell(7).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "GLType") ? row.GLType : "");
                        rows1.CreateCell(8).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "NewChannel") ? row.NewChannel : "");
                        rows1.CreateCell(9).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Customer") ? row.Customer : "");
                        rows1.CreateCell(10).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "OldChannel") ? row.OldChannel : "");
                        rows1.CreateCell(11).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "MG3") ? row.MG3 : "");
                        rows1.CreateCell(12).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "MG4") ? row.MG4 : "");
                        rows1.CreateCell(13).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Activity") ? row.Activity : "");
                        rows1.CreateCell(14).CellStyle = cellStyleForLocked; rows1.GetCell(14).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jan") ? float.Parse(row.Jan) : 0);
                        rows1.CreateCell(15).CellStyle = cellStyleForLocked; rows1.GetCell(15).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Feb") ? float.Parse(row.Feb) : 0);
                        rows1.CreateCell(16).CellStyle = cellStyleForLocked; rows1.GetCell(16).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Mar") ? float.Parse(row.Mar) : 0);
                        rows1.CreateCell(17).CellStyle = cellStyleForLocked; rows1.GetCell(17).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Apr") ? float.Parse(row.Apr) : 0);
                        rows1.CreateCell(18).CellStyle = cellStyleForLocked; rows1.GetCell(18).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "May") ? float.Parse(row.May) : 0);
                        rows1.CreateCell(19).CellStyle = cellStyleForLocked; rows1.GetCell(19).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jun") ? float.Parse(row.Jun) : 0);
                        rows1.CreateCell(20).CellStyle = cellStyleForLocked; rows1.GetCell(20).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jul") ? float.Parse(row.Jul) : 0);
                        rows1.CreateCell(21).CellStyle = cellStyleForLocked; rows1.GetCell(21).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Aug") ? float.Parse(row.Aug) : 0);
                        rows1.CreateCell(22).CellStyle = cellStyleForLocked; rows1.GetCell(22).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Sep") ? float.Parse(row.Sep) : 0);
                        rows1.CreateCell(23).CellStyle = cellStyleForLocked; rows1.GetCell(23).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Oct") ? float.Parse(row.Oct) : 0);
                        rows1.CreateCell(24).CellStyle = cellStyleForLocked; rows1.GetCell(24).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Nov") ? float.Parse(row.Nov) : 0);
                        rows1.CreateCell(25).CellStyle = cellStyleForLocked; rows1.GetCell(25).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Dec") ? float.Parse(row.Dec) : 0);
                        rows1.CreateCell(26).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Status") ? row.Status : "");

                        ICellStyle cellStyle = workbook.CreateCellStyle();
                        cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
                        cellStyle.FillPattern = FillPattern.SparseDots;
                        rows1.GetCell(0).CellStyle = cellStyle;
                        rows1.GetCell(1).CellStyle = cellStyle;
                        rows1.GetCell(2).CellStyle = cellStyle;
                        rows1.GetCell(3).CellStyle = cellStyle;
                        rows1.GetCell(4).CellStyle = cellStyle;
                        rows1.GetCell(5).CellStyle = cellStyle;
                        rows1.GetCell(6).CellStyle = cellStyle;
                        rows1.GetCell(7).CellStyle = cellStyle;
                        rows1.GetCell(8).CellStyle = cellStyle;
                        rows1.GetCell(9).CellStyle = cellStyle;
                        rows1.GetCell(10).CellStyle = cellStyle;
                        rows1.GetCell(11).CellStyle = cellStyle;
                        rows1.GetCell(12).CellStyle = cellStyle;
                        rows1.GetCell(13).CellStyle = cellStyle;
                        rows1.GetCell(26).CellStyle = cellStyle;
                        startnum1++;
                    }
                }

                sheet1.SetColumnWidth(0, 4000);
                sheet1.SetColumnWidth(1, 3000);
                sheet1.SetColumnWidth(2, 3800);
                sheet1.SetColumnWidth(3, 3800);
                sheet1.SetColumnWidth(4, 3800);
                sheet1.SetColumnWidth(5, 3500);
                sheet1.SetColumnWidth(6, 8000);
                sheet1.SetColumnWidth(7, 3500);
                sheet1.SetColumnWidth(8, 8000);
                sheet1.SetColumnWidth(9, 4000);
                sheet1.SetColumnWidth(10, 4500);
                sheet1.SetColumnWidth(11, 3500);
                sheet1.SetColumnWidth(12, 3500);
                sheet1.SetColumnWidth(13, 3500);
                sheet1.SetColumnWidth(26, 3800);

                for (int i = 14; i <= 25; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                sheet1.ProtectSheet("P@ssw0rd123!");
                workbook.LockStructure();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "SpendingPhasing_Update " + Period.ToString("yyyy-MMM-dd") + "_Failed.xlsx");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<IActionResult> ImportPrimarySales(IList<IFormFile> file, bool isBulk = false, string year  = "", string month = "")
        {
            try
            {
                var updateResult = _updateService.ImportPrimarySales(file, isBulk, year, month);
                if (updateResult.Result)
                {
                    return Ok(new { status = updateResult.Result ? "success" : "error", msg = updateResult.Message, id = updateResult.ID });
                }
                else
                {
                    return Ok(new { status = "error", msg = updateResult.Message });
                }
            }
            catch (InvalidCastException ex)
            {
                return BadRequest(new { status = "error", msg = "Something wrong in cell : " + currentColumn + " (Note : " + ex.Message + " )" });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = "error", msg = e.Message });
            }
        }

        public async Task<IActionResult> ImportSecondarySales(IList<IFormFile> file, bool isBulk = false, string year = "", string month = "")
        {
            try
            {
                var updateResult = _updateService.ImportSecondarySales(file, isBulk, year, month);
                if (updateResult.Result)
                {
                    return Ok(new { status = updateResult.Result ? "success" : "error", msg = updateResult.Message, id = updateResult.ID });
                }
                else
                {
                    return Ok(new { status = "error", msg = updateResult.Message });
                }
            }
            catch (InvalidCastException ex)
            {
                return BadRequest(new { status = "error", msg = "Something wrong in cell : " + currentColumn + " (Note : " + ex.Message + " )" });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = "error", msg = e.Message });
            }
        }

        public async Task<IActionResult> ImportSpendingPhasing(IList<IFormFile> file, bool isBulk = false, string year = "", string month = "",
            string budgetOwner = "", List<string> categoryList = null, List<string> profitCenterList = null)
        {
            try
            {
                var updateResult = _updateService.ImportSpendingPhasing(file, isBulk, year, month, budgetOwner, categoryList, profitCenterList);
                if (updateResult.Result)
                {
                    return Ok(new { status = updateResult.Result ? "success" : "error", msg = updateResult.Message, id = updateResult.ID });
                }
                else
                {
                    return Ok(new { status = "error", msg = updateResult.Message });
                }
            }
            catch (InvalidCastException ex)
            {
                return BadRequest(new { status = "error", msg = "Something wrong in cell : " + currentColumn + " (Note : " + ex.Message + " )" });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = "error", msg = e.Message });
            }
        }

        public async Task<IActionResult> LockPrimarySales(bool isBulk = false, string year = "", string month = "", bool isLock = false)
        {
            try
            {
                var checkSecondarySalesHeadUpdate = await _updateService.CheckPrimarySalesHeadUpdate(year, month);
                if (checkSecondarySalesHeadUpdate == null)
                {
                    return Ok(new { status = "error", msg = "Failed Lock Data" });
                }
                else
                {
                    await _updateService.UpdatePrimarySalesHeadUpdate(_app.UserName, year, month, isLock);
                    return Ok(new { status = "success", msg = "Successfully Lock Data" });
                } 
            }
            catch (InvalidCastException ex)
            {
                return BadRequest(new { status = "error", msg = "Something wrong in cell : " + currentColumn + " (Note : " + ex.Message + " )" });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = "error", msg = e.Message });
            }
        }

        public async Task<IActionResult> LockSecondarySales(bool isBulk = false, string year = "", string month = "", bool isLock = false)
        {
            try
            {
                var checkSecondarySalesHeadUpdate = await _updateService.CheckSecondarySalesHeadUpdate(year, month);
                if (checkSecondarySalesHeadUpdate == null)
                {
                    return Ok(new { status = "error", msg = "Failed Lock Data" });
                }
                else
                {
                    await _updateService.UpdateSecondarySalesHeadUpdate(_app.UserName, year, month, isLock);
                    return Ok(new { status = "success", msg = "Successfully Lock Data" });
                }                    
            }
            catch (InvalidCastException ex)
            {
                return BadRequest(new { status = "error", msg = "Something wrong in cell : " + currentColumn + " (Note : " + ex.Message + " )" });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = "error", msg = e.Message });
            }
        }

        public async Task<IActionResult> LockSpendingPhasing(bool isBulk = false, string year = "", string month = "", bool isLock = false)
        {
            try
            {
                var checkPrimarySalesHeadUpdate = await _updateService.CheckSpendingPhasingHeadUpdate(year, month);
                if (checkPrimarySalesHeadUpdate == null)
                {
                    return Ok(new { status = "error", msg = "Failed Lock Data" });
                }
                else
                {
                    await _updateService.UpdateSpendingPhasingHeadUpdate(_app.UserName, year, month, isLock);
                    return Ok(new { status = "success", msg = "Successfully Lock Data" });
                }
            }
            catch (InvalidCastException ex)
            {
                return BadRequest(new { status = "error", msg = "Something wrong in cell : " + currentColumn + " (Note : " + ex.Message + " )" });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = "error", msg = e.Message });
            }
        }

        public IActionResult HasFailedPrimarySales(string year = "", string month = "")
        {
            var usercode = _app.UserName;
            var tempMappingPrimarySalesUpdate = _updateRepository.GetTempMappingPrimarySalesUpdate(usercode, year, month).GetAwaiter().GetResult();
            if (tempMappingPrimarySalesUpdate.Count > 0)
            {
                return Ok(new { status = "true" });
            }
            else
            {
                return Ok(new { status = "false" });
            }
        }

        public IActionResult HasFailedSecondarySales(string year = "", string month = "")
        {
            var usercode = _app.UserName;
            var tempMappingSecondarySalesUpdate = _updateRepository.GetTempMappingSecondarySalesUpdate(usercode, year, month).GetAwaiter().GetResult();
            if (tempMappingSecondarySalesUpdate.Count > 0)
            {
                return Ok(new { status = "true" });
            }
            else
            {
                return Ok(new { status = "false" });
            }
        }

        public IActionResult HasFailedSpendingPhasing(string year = "", string month = "")
        {
            var usercode = _app.UserName;
            var tempMappingSpendingPhasingUpdate = _updateRepository.GetTempMappingSpendingPhasingUpdate(usercode, year, month).GetAwaiter().GetResult();
            if (tempMappingSpendingPhasingUpdate.Count > 0)
            {
                return Ok(new { status = "true" });
            }
            else
            {
                return Ok(new { status = "false" });
            }
        }
    }
}
