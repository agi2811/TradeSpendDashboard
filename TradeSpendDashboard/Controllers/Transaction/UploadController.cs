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
    public class UploadController : BaseController
    {
        private readonly ILogger<UploadController> _logger;
        private readonly ExcelHelper _excelHelper;
        private readonly IGlobalService _globalService;
        private readonly IUploadService _uploadService;
        private readonly IUploadRepository _uploadRepository;
        private readonly IMasterUsersSpendingService _masterUsersSpendingService;
        AppHelper _app;
        public static string currentColumn = "";

        public UploadController(
            ILogger<UploadController> logger,
            AppHelper app,
            IWebHostEnvironment environment,
            IMasterMenuService masterMenu,
            IGlobalService globalService,
            IUploadService uploadService,
            IUploadRepository uploadRepository,
            IMasterUsersSpendingService masterUsersSpendingService,
            ExcelHelper excelHelper
        ) : base(logger, app, environment, masterMenu)
        {
            this._logger = logger;
            this._app = app;
            this._excelHelper = excelHelper;
            this._globalService = globalService;
            this._uploadService = uploadService;
            this._uploadRepository = uploadRepository;
            this._masterUsersSpendingService = masterUsersSpendingService;
        }

        public IActionResult PrimarySalesUpload()
        {
            var dataCurrentMonth = _globalService.GetCurrentMonth();
            var dataCurrentYear = _globalService.GetCurrentYear();

            ViewBag.CurrentMonth = dataCurrentMonth.Month;
            ViewBag.CurrentYear = dataCurrentYear.Year;

            return View();
        }

        public IActionResult SecondarySalesUpload()
        {
            var dataCurrentMonth = _globalService.GetCurrentMonth();
            var dataCurrentYear = _globalService.GetCurrentYear();

            ViewBag.CurrentMonth = dataCurrentMonth.Month;
            ViewBag.CurrentYear = dataCurrentYear.Year;

            return View();
        }

        public async Task<IActionResult> SpendingPhasingUpload()
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
                var data = _uploadService.GetPrimarySalesUpload(year, month);
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
                var data = _uploadService.GetSecondarySalesUpload(year, month);
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
                var data = _uploadService.GetSpendingPhasingUpload(year, month);
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
                var dataLock = await _uploadService.CheckPrimarySalesHeadUpload(year, month);
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
                var dataLock = await _uploadService.CheckSecondarySalesHeadUpload(year, month);
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
                var dataLock = await _uploadService.CheckSpendingPhasingHeadUpload(year, month);
                return Ok(new { status = "success", result = dataLock });
            }
            catch (Exception ex)
            {
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

                for (int i = 3; i <= 14;  i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "PrimarySales " + Period.ToString("yyyy-MMM-dd") + ".xlsx");
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

                var dataTempUpload = await _uploadRepository.GetTempMappingPrimarySalesUpload(usercode, year, month);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataTempUpload.Count > 0)
                {
                    foreach (var row in dataTempUpload)
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
                return File(output.ToArray(), "application/vnd.ms-excel", "PrimarySales " + Period.ToString("yyyy-MMM-dd") + "_Failed.xlsx");
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
                return File(output.ToArray(), "application/vnd.ms-excel", "SecondarySales " + Period.ToString("yyyy-MMM-dd") + ".xlsx");
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

                var dataTempUpload = await _uploadRepository.GetTempMappingSecondarySalesUpload(usercode, year, month);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataTempUpload.Count > 0)
                {
                    foreach (var row in dataTempUpload)
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
                return File(output.ToArray(), "application/vnd.ms-excel", "SecondarySales " + Period.ToString("yyyy-MMM-dd") + "_Failed.xlsx");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<FileResult> DownloadTemplateSpendingPhasing(bool isBulk)
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
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "Year", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "Budget Owner", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Profit Center", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(3), "string", "Category", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(4), "string", "GL", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(5), "string", "GL Desc", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(6), "string", "GL Type", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(7), "string", "New Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(8), "string", "Customer", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(9), "string", "Old Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(10), "string", "MG3*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(11), "string", "MG4*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(12), "string", "Activity*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(13), "string", "Jan", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(14), "string", "Feb", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(15), "string", "Mar", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(16), "string", "Apr", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(17), "string", "May", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(18), "string", "Jun", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(19), "string", "Jul", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(20), "string", "Aug", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(21), "string", "Sep", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(22), "string", "Oct", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(23), "string", "Nov", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(24), "string", "Dec", "#DCE6F1", true, "center");

                sheet1.SetColumnWidth(0, 3000);
                sheet1.SetColumnWidth(1, 3800);
                sheet1.SetColumnWidth(2, 3800);
                sheet1.SetColumnWidth(3, 3800);
                sheet1.SetColumnWidth(4, 3500);
                sheet1.SetColumnWidth(5, 8000);
                sheet1.SetColumnWidth(6, 3500);
                sheet1.SetColumnWidth(7, 8000);
                sheet1.SetColumnWidth(8, 4000);
                sheet1.SetColumnWidth(9, 4500);
                sheet1.SetColumnWidth(10, 3500);
                sheet1.SetColumnWidth(11, 3500);
                sheet1.SetColumnWidth(12, 3500);

                for (int i = 13; i <= 24; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "SpendingPhasing " + Period.ToString("yyyy-MMM-dd") + ".xlsx");
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
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(0), "string", "Year", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(1), "string", "Budget Owner", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(2), "string", "Profit Center", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(3), "string", "Category", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(4), "string", "GL", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(5), "string", "GL Desc", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(6), "string", "GL Type", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(7), "string", "New Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(8), "string", "Customer", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(9), "string", "Old Channel", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(10), "string", "MG3*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(11), "string", "MG4*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(12), "string", "Activity*", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(13), "string", "Jan", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(14), "string", "Feb", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(15), "string", "Mar", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(16), "string", "Apr", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(17), "string", "May", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(18), "string", "Jun", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(19), "string", "Jul", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(20), "string", "Aug", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(21), "string", "Sep", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(22), "string", "Oct", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(23), "string", "Nov", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(24), "string", "Dec", "#DCE6F1", true, "center");
                excelHelper.SetData(sheet1, workbook, header1.CreateCell(25), "string", "Status", "#DCE6F1", true, "center");

                var dataTempUpload = await _uploadRepository.GetTempMappingSpendingPhasingUpload(usercode, year, month);
                int rownum1 = 0;
                int startnum1 = rownum1 + 1;
                if (dataTempUpload.Count > 0)
                {
                    foreach (var row in dataTempUpload)
                    {
                        var rows1 = sheet1.CreateRow(startnum1);
                        rows1.CreateCell(0).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Year") ? row.Year : "");
                        rows1.CreateCell(1).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "BudgetOwner") ? row.BudgetOwner : "");
                        rows1.CreateCell(2).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "ProfitCenter") ? row.ProfitCenter : "");
                        rows1.CreateCell(3).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Category") ? row.Category : "");
                        rows1.CreateCell(4).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "GL") ? row.GL : "");
                        rows1.CreateCell(5).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "GLDesc") ? row.GLDesc : "");
                        rows1.CreateCell(6).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "GLType") ? row.GLType : "");
                        rows1.CreateCell(7).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "NewChannel") ? row.NewChannel : "");
                        rows1.CreateCell(8).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Customer") ? row.Customer : "");
                        rows1.CreateCell(9).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "OldChannel") ? row.OldChannel : "");
                        rows1.CreateCell(10).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "MG3") ? row.MG3 : "");
                        rows1.CreateCell(11).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "MG4") ? row.MG4 : "");
                        rows1.CreateCell(12).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Activity") ? row.Activity : "");
                        rows1.CreateCell(13).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jan") ? float.Parse(row.Jan) : 0);
                        rows1.CreateCell(14).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Feb") ? float.Parse(row.Feb) : 0);
                        rows1.CreateCell(15).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Mar") ? float.Parse(row.Mar) : 0);
                        rows1.CreateCell(16).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Apr") ? float.Parse(row.Apr) : 0);
                        rows1.CreateCell(17).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "May") ? float.Parse(row.May) : 0);
                        rows1.CreateCell(18).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jun") ? float.Parse(row.Jun) : 0);
                        rows1.CreateCell(19).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Jul") ? float.Parse(row.Jul) : 0);
                        rows1.CreateCell(20).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Aug") ? float.Parse(row.Aug) : 0);
                        rows1.CreateCell(21).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Sep") ? float.Parse(row.Sep) : 0);
                        rows1.CreateCell(22).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Oct") ? float.Parse(row.Oct) : 0);
                        rows1.CreateCell(23).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Nov") ? float.Parse(row.Nov) : 0);
                        rows1.CreateCell(24).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Dec") ? float.Parse(row.Dec) : 0);
                        rows1.CreateCell(25).SetCellValue(DynamicCollectionHelper.CheckVariable(row, "Status") ? row.Status : "");
                        startnum1++;
                    }
                }

                sheet1.SetColumnWidth(0, 3000);
                sheet1.SetColumnWidth(1, 3800);
                sheet1.SetColumnWidth(2, 3800);
                sheet1.SetColumnWidth(3, 3800);
                sheet1.SetColumnWidth(4, 3500);
                sheet1.SetColumnWidth(5, 8000);
                sheet1.SetColumnWidth(6, 3500);
                sheet1.SetColumnWidth(7, 8000);
                sheet1.SetColumnWidth(8, 4000);
                sheet1.SetColumnWidth(9, 4500);
                sheet1.SetColumnWidth(10, 3500);
                sheet1.SetColumnWidth(11, 3500);
                sheet1.SetColumnWidth(12, 3500);
                sheet1.SetColumnWidth(25, 3800);

                for (int i = 13; i <= 24; i++)
                {
                    sheet1.SetColumnWidth(i, 2544);
                }

                MemoryStream output = new MemoryStream();
                workbook.Write(output);
                return File(output.ToArray(), "application/vnd.ms-excel", "SpendingPhasing " + Period.ToString("yyyy-MMM-dd") + "_Failed.xlsx");
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
                var uploadResult = _uploadService.ImportPrimarySales(file, isBulk, year, month);
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
                var uploadResult = _uploadService.ImportSecondarySales(file, isBulk, year, month);
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

        public async Task<IActionResult> ImportSpendingPhasing(IList<IFormFile> file, bool isBulk = false, string year = "", string month = "",
            string budgetOwner = "", List<string> categoryList = null, List<string> profitCenterList = null)
        {
            try
            {
                var uploadResult = _uploadService.ImportSpendingPhasing(file, isBulk, year, month, budgetOwner, categoryList, profitCenterList);
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

        public async Task<IActionResult> LockPrimarySales(bool isBulk = false, string year = "", string month = "", bool isLock = false)
        {
            try
            {
                var checkSecondarySalesHeadUpload = await _uploadService.CheckPrimarySalesHeadUpload(year, month);
                if (checkSecondarySalesHeadUpload == null)
                {
                    return Ok(new { status = "error", msg = "Failed Lock Data" });
                }
                else
                {
                    await _uploadService.UpdatePrimarySalesHeadUpload(_app.UserName, year, month, isLock);
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
                var checkSecondarySalesHeadUpload = await _uploadService.CheckSecondarySalesHeadUpload(year, month);
                if (checkSecondarySalesHeadUpload == null)
                {
                    return Ok(new { status = "error", msg = "Failed Lock Data" });
                }
                else
                {
                    await _uploadService.UpdateSecondarySalesHeadUpload(_app.UserName, year, month, isLock);
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
                var checkPrimarySalesHeadUpload = await _uploadService.CheckSpendingPhasingHeadUpload(year, month);
                if (checkPrimarySalesHeadUpload == null)
                {
                    return Ok(new { status = "error", msg = "Failed Lock Data" });
                }
                else
                {
                    await _uploadService.UpdateSpendingPhasingHeadUpload(_app.UserName, year, month, isLock);
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
            var tempMappingPrimarySalesUpload = _uploadRepository.GetTempMappingPrimarySalesUpload(usercode, year, month).GetAwaiter().GetResult();
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
            var tempMappingSecondarySalesUpload = _uploadRepository.GetTempMappingSecondarySalesUpload(usercode, year, month).GetAwaiter().GetResult();
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
            var tempMappingSpendingPhasingUpload = _uploadRepository.GetTempMappingSpendingPhasingUpload(usercode, year, month).GetAwaiter().GetResult();
            if (tempMappingSpendingPhasingUpload.Count > 0)
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
