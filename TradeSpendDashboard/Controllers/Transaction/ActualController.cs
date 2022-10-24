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
    public class ActualController : BaseController
    {
        private readonly ILogger<ActualController> _logger;
        private readonly ExcelHelper _excelHelper;
        private readonly IGlobalService _globalService;
        private readonly IActualService _actualService;
        private readonly IActualRepository _actualRepository;
        AppHelper _app;
        public static string currentColumn = "";

        public ActualController(
            ILogger<ActualController> logger,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu,
            IGlobalService globalService,
            IActualService actualService,
            IActualRepository actualRepository,
            ExcelHelper excelHelper
        ) : base(logger, app, environment, masterMenu)
        {
            this._logger = logger;
            this._app = app;
            this._excelHelper = excelHelper;
            this._globalService = globalService;
            this._actualService = actualService;
            this._actualRepository = actualRepository;
        }

        public IActionResult PrimarySalesActual()
        {
            var dataCurrentMonth = _globalService.GetCurrentMonth();
            var dataCurrentYear = _globalService.GetCurrentYear();

            ViewBag.CurrentMonth = dataCurrentMonth.Month;
            ViewBag.CurrentYear = dataCurrentYear.Year;

            return View();
        }

        public IActionResult SecondarySalesActual()
        {
            var dataCurrentMonth = _globalService.GetCurrentMonth();
            var dataCurrentYear = _globalService.GetCurrentYear();

            ViewBag.CurrentMonth = dataCurrentMonth.Month;
            ViewBag.CurrentYear = dataCurrentYear.Year;

            return View();
        }

        public IActionResult SpendingPhasingActual()
        {
            var dataCurrentMonth = _globalService.GetCurrentMonth();
            var dataCurrentYear = _globalService.GetCurrentYear();

            ViewBag.CurrentMonth = dataCurrentMonth.Month;
            ViewBag.CurrentYear = dataCurrentYear.Year;

            return View();
        }

        public IActionResult GetDataPrimarySales(string year, string month)
        {
            try
            {
                this._logger.LogInformation("GetData :");
                var data = _actualService.GetPrimarySalesActual(year, month);
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
                var data = _actualService.GetSecondarySalesActual(year, month);
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
                var data = _actualService.GetSpendingPhasingActual(year, month);
                return Ok(new { status = "success", result = data.Result });
            }
            catch (Exception ex)
            {
                this._logger.LogInformation("Error :", ex);
                this._logger.LogDebug("Error debug : ", ex);
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<FileResult> DownloadTemplatePrimarySales(bool isBulk)
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
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "Profit Center", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Category", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(3), "string", "Jan", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(4), "string", "Feb", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(5), "string", "Mar", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(6), "string", "Apr", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(7), "string", "May", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(8), "string", "Jun", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(9), "string", "Jul", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(10), "string", "Aug", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(11), "string", "Sep", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(12), "string", "Oct", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(13), "string", "Nov", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(14), "string", "Dec", "#DCE6F1", true, "center");

                sheet1.SetColumnWidth(0, 8000);
                sheet1.SetColumnWidth(1, 3800);
                sheet1.SetColumnWidth(2, 3800);

                for (int i = 3; i <= 14; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "PrimarySales_Actual " + Period.ToString("yyyy-MMM-dd") + ".xlsx");
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
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "Profit Center", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Category", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(3), "string", "Jan", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(4), "string", "Feb", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(5), "string", "Mar", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(6), "string", "Apr", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(7), "string", "May", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(8), "string", "Jun", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(9), "string", "Jul", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(10), "string", "Aug", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(11), "string", "Sep", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(12), "string", "Oct", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(13), "string", "Nov", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(14), "string", "Dec", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(15), "string", "Status", "#DCE6F1", true, "center");

                var dataTempActual = await _actualRepository.GetTempMappingPrimarySalesActual(usercode, year, month);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataTempActual.Count > 0)
                {
                    foreach (var row in dataTempActual)
                    {
                        var rows1 = sheet1.CreateRow(startnum1);
                        rows1.CreateCell(0).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Channel") ? row.Channel : "");
                        rows1.CreateCell(1).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "ProfitCenter") ? row.ProfitCenter : "");
                        rows1.CreateCell(2).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Category") ? row.Category : "");
                        rows1.CreateCell(3).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jan") ? float.Parse(row.Jan) : 0);
                        rows1.CreateCell(4).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Feb") ? float.Parse(row.Feb) : 0);
                        rows1.CreateCell(5).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Mar") ? float.Parse(row.Mar) : 0);
                        rows1.CreateCell(6).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Apr") ? float.Parse(row.Apr) : 0);
                        rows1.CreateCell(7).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "May") ? float.Parse(row.May) : 0);
                        rows1.CreateCell(8).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jun") ? float.Parse(row.Jun) : 0);
                        rows1.CreateCell(9).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jul") ? float.Parse(row.Jul) : 0);
                        rows1.CreateCell(10).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Aug") ? float.Parse(row.Aug) : 0);
                        rows1.CreateCell(11).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Sep") ? float.Parse(row.Sep) : 0);
                        rows1.CreateCell(12).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Oct") ? float.Parse(row.Oct) : 0);
                        rows1.CreateCell(13).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Nov") ? float.Parse(row.Nov) : 0);
                        rows1.CreateCell(14).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Dec") ? float.Parse(row.Dec) : 0);
                        rows1.CreateCell(15).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Status") ? row.Status : "");
                        startnum1++;
                    }
                }

                sheet1.SetColumnWidth(0, 8000);
                sheet1.SetColumnWidth(1, 3800);
                sheet1.SetColumnWidth(2, 3800);
                sheet1.SetColumnWidth(15, 3800);

                for (int i = 3; i <= 14; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "PrimarySales_Actual " + Period.ToString("yyyy-MMM-dd") + "_Failed.xlsx");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<FileResult> DownloadTemplateSecondarySales(bool isBulk)
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
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "New Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "Old Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Customer", "#DCE6F1", true, "center");
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

                sheet1.SetColumnWidth(0, 8000);
                sheet1.SetColumnWidth(1, 4500);
                sheet1.SetColumnWidth(2, 4000);
                sheet1.SetColumnWidth(3, 3800);

                for (int i = 4; i <= 15; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "SecondarySales_Actual " + Period.ToString("yyyy-MMM-dd") + ".xlsx");
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
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "New Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "Old Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Customer", "#DCE6F1", true, "center");
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

                var dataTempActual = await _actualRepository.GetTempMappingSecondarySalesActual(usercode, year, month);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataTempActual.Count > 0)
                {
                    foreach (var row in dataTempActual)
                    {
                        var rows1 = sheet1.CreateRow(startnum1);
                        rows1.CreateCell(0).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "NewChannel") ? row.NewChannel : "");
                        rows1.CreateCell(1).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "OldChannel") ? row.OldChannel : "");
                        rows1.CreateCell(2).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Customer") ? row.Customer : "");
                        rows1.CreateCell(3).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Category") ? row.Category : "");
                        rows1.CreateCell(4).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jan") ? float.Parse(row.Jan) : 0);
                        rows1.CreateCell(5).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Feb") ? float.Parse(row.Feb) : 0);
                        rows1.CreateCell(6).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Mar") ? float.Parse(row.Mar) : 0);
                        rows1.CreateCell(7).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Apr") ? float.Parse(row.Apr) : 0);
                        rows1.CreateCell(8).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "May") ? float.Parse(row.May) : 0);
                        rows1.CreateCell(9).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jun") ? float.Parse(row.Jun) : 0);
                        rows1.CreateCell(10).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jul") ? float.Parse(row.Jul) : 0);
                        rows1.CreateCell(11).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Aug") ? float.Parse(row.Aug) : 0);
                        rows1.CreateCell(12).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Sep") ? float.Parse(row.Sep) : 0);
                        rows1.CreateCell(13).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Oct") ? float.Parse(row.Oct) : 0);
                        rows1.CreateCell(14).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Nov") ? float.Parse(row.Nov) : 0);
                        rows1.CreateCell(15).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Dec") ? float.Parse(row.Dec) : 0);
                        rows1.CreateCell(16).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Status") ? row.Status : "");
                        startnum1++;
                    }
                }

                sheet1.SetColumnWidth(0, 8000);
                sheet1.SetColumnWidth(1, 4500);
                sheet1.SetColumnWidth(2, 4000);
                sheet1.SetColumnWidth(3, 3800);
                sheet1.SetColumnWidth(16, 3800);

                for (int i = 4; i <= 15; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "SecondarySales_Actual " + Period.ToString("yyyy-MMM-dd") + "_Failed.xlsx");
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
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "GL", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "GL Name", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(3), "string", "Category Group", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(4), "string", "Legend", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(5), "string", "Customer", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(6), "string", "MG4 Desc", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(7), "string", "Jan", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(8), "string", "Feb", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(9), "string", "Mar", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(10), "string", "Apr", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(11), "string", "May", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(12), "string", "Jun", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(13), "string", "Jul", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(14), "string", "Aug", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(15), "string", "Sep", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(16), "string", "Oct", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(17), "string", "Nov", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(18), "string", "Dec", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(19), "string", "Status", "#DCE6F1", true, "center");

                var dataTempActual = await _actualRepository.GetTempMappingSpendingPhasingActual(usercode, year, month);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataTempActual.Count > 0)
                {
                    foreach (var row in dataTempActual)
                    {
                        var rows1 = sheet1.CreateRow(startnum1);
                        rows1.CreateCell(0).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Channel") ? row.Channel : "");
                        rows1.CreateCell(1).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "GL") ? row.GL : "");
                        rows1.CreateCell(2).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "GLName") ? row.GLName : "");
                        rows1.CreateCell(3).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "CategoryGroup") ? row.CategoryGroup : "");
                        rows1.CreateCell(4).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Legend") ? row.Legend : "");
                        rows1.CreateCell(5).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Customer") ? row.Customer : "");
                        rows1.CreateCell(6).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "MaterialGroup4Desc") ? row.MaterialGroup4Desc : "");
                        rows1.CreateCell(7).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jan") ? float.Parse(row.Jan) : 0);
                        rows1.CreateCell(8).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Feb") ? float.Parse(row.Feb) : 0);
                        rows1.CreateCell(9).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Mar") ? float.Parse(row.Mar) : 0);
                        rows1.CreateCell(10).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Apr") ? float.Parse(row.Apr) : 0);
                        rows1.CreateCell(11).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "May") ? float.Parse(row.May) : 0);
                        rows1.CreateCell(12).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jun") ? float.Parse(row.Jun) : 0);
                        rows1.CreateCell(13).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jul") ? float.Parse(row.Jul) : 0);
                        rows1.CreateCell(14).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Aug") ? float.Parse(row.Aug) : 0);
                        rows1.CreateCell(15).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Sep") ? float.Parse(row.Sep) : 0);
                        rows1.CreateCell(16).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Oct") ? float.Parse(row.Oct) : 0);
                        rows1.CreateCell(17).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Nov") ? float.Parse(row.Nov) : 0);
                        rows1.CreateCell(18).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Dec") ? float.Parse(row.Dec) : 0);
                        rows1.CreateCell(19).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Status") ? row.Status : "");
                        startnum1++;
                    }
                }

                sheet1.SetColumnWidth(0, 3800);
                sheet1.SetColumnWidth(1, 3500);
                sheet1.SetColumnWidth(2, 8000);
                sheet1.SetColumnWidth(3, 3800);
                sheet1.SetColumnWidth(4, 3000);
                sheet1.SetColumnWidth(5, 4000);
                sheet1.SetColumnWidth(6, 4000);
                sheet1.SetColumnWidth(19, 3800);

                for (int i = 7; i <= 18; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "SpendingPhasing_Actual " + Period.ToString("yyyy-MMM-dd") + "_Failed.xlsx");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<IActionResult> ImportPrimarySales(IList<IFormFile> file, bool isBulk = false, string year = "", string month = "")
        {
            try
            {
                var uploadResult = _actualService.ImportPrimarySales(file, isBulk, year, month);
                if (uploadResult.Result)
                {
                    return Ok(new { status = uploadResult.Result ? "success" : "error", msg = uploadResult.Message, id = uploadResult.ID });
                }
                else
                {
                    return Ok(new { status = "error", msg = uploadResult.Message });
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
                var uploadResult = _actualService.ImportSecondarySales(file, isBulk, year, month);
                if (uploadResult.Result)
                {
                    return Ok(new { status = uploadResult.Result ? "success" : "error", msg = uploadResult.Message, id = uploadResult.ID });
                }
                else
                {
                    return Ok(new { status = "error", msg = uploadResult.Message });
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

        public async Task<IActionResult> InterfaceSpendingPhasing(string year = "", string month = "")
        {
            try
            {
                var uploadResult = _actualService.InterfaceSpendingPhasing(year, month);
                if (uploadResult.Result)
                {
                    return Ok(new { status = uploadResult.Result ? "success" : "error", msg = uploadResult.Message, id = uploadResult.ID });
                }
                else
                {
                    return Ok(new { status = "error", msg = uploadResult.Message });
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
            var tempMappingPrimarySalesUpload = _actualRepository.GetTempMappingPrimarySalesActual(usercode, year, month).GetAwaiter().GetResult();
            if (tempMappingPrimarySalesUpload.Count > 0)
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
            var tempMappingSecondarySalesUpload = _actualRepository.GetTempMappingSecondarySalesActual(usercode, year, month).GetAwaiter().GetResult();
            if (tempMappingSecondarySalesUpload.Count > 0)
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
            var tempMappingSecondarySalesUpload = _actualRepository.GetTempMappingSpendingPhasingActual(usercode, year, month).GetAwaiter().GetResult();
            if (tempMappingSecondarySalesUpload.Count > 0)
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
