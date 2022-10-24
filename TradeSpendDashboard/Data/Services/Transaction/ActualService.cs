using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Repository.Interface.Transaction;
using TradeSpendDashboard.Data.Services.Interface.Transaction;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO;
using TradeSpendDashboard.Models.Entity.Transaction;

namespace TradeSpendDashboard.Data.Services.Transaction
{
    public class ActualService : IActualService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly ExcelHelper _excelHelper;
        private readonly MailHelper _mailHelper;
        private readonly IActualRepository _actualRepository;
        private readonly IGlobalRepository _globalRepository;
        private readonly IMapper mapper;

        public ActualService(
            ILogger<ActualService> logger,
            AppHelper appHelper,
            ExcelHelper excelHelper,
            IActualRepository actualRepository,
            IGlobalRepository globalRepository,
            MailHelper mailHelper,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.appHelper = appHelper;
            this._actualRepository = actualRepository;
            this._excelHelper = excelHelper;
            this._mailHelper = mailHelper;
            this._globalRepository = globalRepository;
        }

        public Task<List<dynamic>> GetPrimarySalesActual(string year, string month)
        {
            var data = _actualRepository.GetPrimarySalesActual(appHelper.UserName, year, month);
            return data;
        }

        public Task<List<dynamic>> GetSecondarySalesActual(string year, string month)
        {
            var data = _actualRepository.GetSecondarySalesActual(appHelper.UserName, year, month);
            return data;
        }

        public Task<List<dynamic>> GetSpendingPhasingActual(string year, string month)
        {
            var data = _actualRepository.GetSpendingPhasingActual(appHelper.UserName, year, month);
            return data;
        }

        public Task<List<dynamic>> GetYearsPrimarySalesActual()
        {
            var data = _actualRepository.GetYearsPrimarySalesActual();
            return data;
        }

        public Task<List<dynamic>> GetYearsSecondarySalesActual()
        {
            var data = _actualRepository.GetYearsSecondarySalesActual();
            return data;
        }

        public Task<List<dynamic>> GetYearsSpendingPhasingActual()
        {
            var data = _actualRepository.GetYearsSpendingPhasingActual();
            return data;
        }

        public ValidationDTO ImportPrimarySales(IList<IFormFile> file, bool isBulk, string year, string month)
        {
            var validationResult = new ValidationDTO();
            var ErrorMessageResult = new List<ErrorMessage>();
            var fileName = "";
            var usercode = appHelper.UserName;

            if (file[0].FileName != "")
            {
                //var SeqAttachCount = repository.GetCountAttachFile("PrimarySales", usercode);
                fileName = UploadedFile(file, "Actual/PrimarySales", "PrimarySales").GetAwaiter().GetResult();
                XSSFWorkbook xssf;
                ISheet sheet = null;

                _actualRepository.TruncateTempMappingPrimarySales(usercode, year, month);
                using (FileStream files = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    string extension = Path.GetExtension(fileName);
                    var dataCurrentDate = _globalRepository.GetCurrentDate();
                    DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);
                    if (extension == ".xlsx")
                    {
                        xssf = new XSSFWorkbook(files);
                        sheet = xssf.GetSheetAt(0); //GetSheet("Sheet1");
                        DataTable dt = new DataTable("TradeTempPrimarySalesActual");
                        dt.Columns.Add("YearPeriod");
                        dt.Columns.Add("MonthPeriod");
                        dt.Columns.Add("Channel");
                        dt.Columns.Add("ProfitCenter");
                        dt.Columns.Add("Category");
                        dt.Columns.Add("Year");
                        dt.Columns.Add("Jan");
                        dt.Columns.Add("Feb");
                        dt.Columns.Add("Mar");
                        dt.Columns.Add("Apr");
                        dt.Columns.Add("May");
                        dt.Columns.Add("Jun");
                        dt.Columns.Add("Jul");
                        dt.Columns.Add("Aug");
                        dt.Columns.Add("Sep");
                        dt.Columns.Add("Oct");
                        dt.Columns.Add("Nov");
                        dt.Columns.Add("Dec");
                        dt.Columns.Add("InsertedBy");
                        dt.Columns.Add("InsertedOn");

                        for (var i = 1; i <= sheet.LastRowNum; i++)
                        {
                            if (sheet.GetRow(i) != null)
                            {
                                if ((sheet.GetRow(i).GetCell(0).StringCellValue != "" && sheet.GetRow(i).GetCell(0).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(1).StringCellValue != "" && sheet.GetRow(i).GetCell(1).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(2).StringCellValue != "" && sheet.GetRow(i).GetCell(2).StringCellValue != null))
                                {
                                    DataRow DataRow = dt.NewRow();
                                    DataRow["YearPeriod"] = year;
                                    DataRow["MonthPeriod"] = month;
                                    DataRow["Channel"] = sheet.GetRow(i).GetCell(0) == null ? "" : _excelHelper.GetStringVal(sheet, i, 0);
                                    DataRow["ProfitCenter"] = sheet.GetRow(i).GetCell(1) == null ? "" : _excelHelper.GetStringVal(sheet, i, 1);
                                    DataRow["Category"] = sheet.GetRow(i).GetCell(2) == null ? "" : _excelHelper.GetStringVal(sheet, i, 2);
                                    DataRow["Year"] = year;
                                    DataRow["Jan"] = sheet.GetRow(i).GetCell(3) == null || _excelHelper.GetStringVal(sheet, i, 3) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 3);
                                    DataRow["Feb"] = sheet.GetRow(i).GetCell(4) == null || _excelHelper.GetStringVal(sheet, i, 4) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 4);
                                    DataRow["Mar"] = sheet.GetRow(i).GetCell(5) == null || _excelHelper.GetStringVal(sheet, i, 5) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 5);
                                    DataRow["Apr"] = sheet.GetRow(i).GetCell(6) == null || _excelHelper.GetStringVal(sheet, i, 6) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 6);
                                    DataRow["May"] = sheet.GetRow(i).GetCell(7) == null || _excelHelper.GetStringVal(sheet, i, 7) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 7);
                                    DataRow["Jun"] = sheet.GetRow(i).GetCell(8) == null || _excelHelper.GetStringVal(sheet, i, 8) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 8);
                                    DataRow["Jul"] = sheet.GetRow(i).GetCell(9) == null || _excelHelper.GetStringVal(sheet, i, 9) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 9);
                                    DataRow["Aug"] = sheet.GetRow(i).GetCell(10) == null || _excelHelper.GetStringVal(sheet, i, 10) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 10);
                                    DataRow["Sep"] = sheet.GetRow(i).GetCell(11) == null || _excelHelper.GetStringVal(sheet, i, 11) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 11);
                                    DataRow["Oct"] = sheet.GetRow(i).GetCell(12) == null || _excelHelper.GetStringVal(sheet, i, 12) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 12);
                                    DataRow["Nov"] = sheet.GetRow(i).GetCell(13) == null || _excelHelper.GetStringVal(sheet, i, 13) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 13);
                                    DataRow["Dec"] = sheet.GetRow(i).GetCell(14) == null || _excelHelper.GetStringVal(sheet, i, 14) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 14);
                                    DataRow["InsertedBy"] = appHelper.UserName.ToString();
                                    DataRow["InsertedOn"] = dateNow;
                                    dt.Rows.Add(DataRow);
                                }
                            }
                        }

                        string tempName = "dbo.TradeTempPrimarySalesActual";
                        var conn = appHelper.ConnectionString();
                        using (SqlConnection sqlCon = new SqlConnection(conn))
                        {
                            sqlCon.Open();
                            using (SqlBulkCopy connect = new SqlBulkCopy(sqlCon))
                            {
                                connect.DestinationTableName = tempName;
                                connect.BatchSize = 5000;
                                connect.BulkCopyTimeout = 7000;
                                for (var i = 0; i <= 19; i++)
                                {
                                    connect.ColumnMappings.Add(i, i + 1);
                                }
                                connect.WriteToServer(dt);
                            }
                        }

                        ErrorMessageResult = _actualRepository.SpImportPrimarySalesActual(usercode, fileName, year, month);
                        if (ErrorMessageResult.FirstOrDefault().Status == "error")
                        {
                            validationResult.Result = false;
                            validationResult.Message = "Failed Upload : " + ErrorMessageResult.FirstOrDefault().Message;
                            validationResult.ID = ErrorMessageResult.FirstOrDefault().ID;
                            return validationResult;
                        }
                    }
                    else
                    {
                        validationResult.Result = false;
                        validationResult.Message = "Failed Upload, File Must be Excel";
                        validationResult.ID = 0;
                        return validationResult;
                    }
                }

                validationResult.Result = true;
                validationResult.Message = ErrorMessageResult.FirstOrDefault().Message;
                validationResult.ID = ErrorMessageResult.FirstOrDefault().ID;
                return validationResult;
            }
            else
            {
                validationResult.Result = false;
                validationResult.Message = "Failed Upload";
                validationResult.ID = 0;
                return validationResult;
            }
        }

        public ValidationDTO ImportSecondarySales(IList<IFormFile> file, bool isBulk, string year, string month)
        {
            var validationResult = new ValidationDTO();
            var ErrorMessageResult = new List<ErrorMessage>();
            var fileName = "";
            var usercode = appHelper.UserName;

            if (file[0].FileName != "")
            {
                //var SeqAttachCount = repository.GetCountAttachFile("SecondarySales", usercode);
                fileName = UploadedFile(file, "Actual/SecondarySales", "SecondarySales").GetAwaiter().GetResult();
                XSSFWorkbook xssf;
                ISheet sheet = null;

                _actualRepository.TruncateTempMappingSecondarySales(usercode, year, month);
                using (FileStream files = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    string extension = Path.GetExtension(fileName);
                    var dataCurrentDate = _globalRepository.GetCurrentDate();
                    DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);
                    if (extension == ".xlsx")
                    {
                        xssf = new XSSFWorkbook(files);
                        sheet = xssf.GetSheetAt(0); //GetSheet("Sheet1");
                        DataTable dt = new DataTable("TradeTempSecondarySalesActual");
                        dt.Columns.Add("YearPeriod");
                        dt.Columns.Add("MonthPeriod");
                        dt.Columns.Add("NewChannel");
                        dt.Columns.Add("OldChannel");
                        dt.Columns.Add("Customer");
                        dt.Columns.Add("Category");
                        dt.Columns.Add("Year");
                        dt.Columns.Add("Jan");
                        dt.Columns.Add("Feb");
                        dt.Columns.Add("Mar");
                        dt.Columns.Add("Apr");
                        dt.Columns.Add("May");
                        dt.Columns.Add("Jun");
                        dt.Columns.Add("Jul");
                        dt.Columns.Add("Aug");
                        dt.Columns.Add("Sep");
                        dt.Columns.Add("Oct");
                        dt.Columns.Add("Nov");
                        dt.Columns.Add("Dec");
                        dt.Columns.Add("InsertedBy");
                        dt.Columns.Add("InsertedOn");

                        for (var i = 1; i <= sheet.LastRowNum; i++)
                        {
                            if (sheet.GetRow(i) != null)
                            {
                                if ((sheet.GetRow(i).GetCell(0).StringCellValue != "" && sheet.GetRow(i).GetCell(0).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(1).StringCellValue != "" && sheet.GetRow(i).GetCell(1).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(2).StringCellValue != "" && sheet.GetRow(i).GetCell(2).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(3).StringCellValue != "" && sheet.GetRow(i).GetCell(3).StringCellValue != null))
                                {
                                    DataRow DataRow = dt.NewRow();
                                    DataRow["YearPeriod"] = year;
                                    DataRow["MonthPeriod"] = month;
                                    DataRow["NewChannel"] = sheet.GetRow(i).GetCell(0) == null ? "" : _excelHelper.GetStringVal(sheet, i, 0);
                                    DataRow["OldChannel"] = sheet.GetRow(i).GetCell(1) == null ? "" : _excelHelper.GetStringVal(sheet, i, 1);
                                    DataRow["Customer"] = sheet.GetRow(i).GetCell(2) == null ? "" : _excelHelper.GetStringVal(sheet, i, 2);
                                    DataRow["Category"] = sheet.GetRow(i).GetCell(3) == null ? "" : _excelHelper.GetStringVal(sheet, i, 3);
                                    DataRow["Year"] = year;
                                    DataRow["Jan"] = sheet.GetRow(i).GetCell(4) == null || _excelHelper.GetStringVal(sheet, i, 4) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 4);
                                    DataRow["Feb"] = sheet.GetRow(i).GetCell(5) == null || _excelHelper.GetStringVal(sheet, i, 5) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 5);
                                    DataRow["Mar"] = sheet.GetRow(i).GetCell(6) == null || _excelHelper.GetStringVal(sheet, i, 6) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 6);
                                    DataRow["Apr"] = sheet.GetRow(i).GetCell(7) == null || _excelHelper.GetStringVal(sheet, i, 7) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 7);
                                    DataRow["May"] = sheet.GetRow(i).GetCell(8) == null || _excelHelper.GetStringVal(sheet, i, 8) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 8);
                                    DataRow["Jun"] = sheet.GetRow(i).GetCell(9) == null || _excelHelper.GetStringVal(sheet, i, 9) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 9);
                                    DataRow["Jul"] = sheet.GetRow(i).GetCell(10) == null || _excelHelper.GetStringVal(sheet, i, 10) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 10);
                                    DataRow["Aug"] = sheet.GetRow(i).GetCell(11) == null || _excelHelper.GetStringVal(sheet, i, 11) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 11);
                                    DataRow["Sep"] = sheet.GetRow(i).GetCell(12) == null || _excelHelper.GetStringVal(sheet, i, 12) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 12);
                                    DataRow["Oct"] = sheet.GetRow(i).GetCell(13) == null || _excelHelper.GetStringVal(sheet, i, 13) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 13);
                                    DataRow["Nov"] = sheet.GetRow(i).GetCell(14) == null || _excelHelper.GetStringVal(sheet, i, 14) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 14);
                                    DataRow["Dec"] = sheet.GetRow(i).GetCell(15) == null || _excelHelper.GetStringVal(sheet, i, 15) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 15);
                                    DataRow["InsertedBy"] = appHelper.UserName.ToString();
                                    DataRow["InsertedOn"] = dateNow;
                                    dt.Rows.Add(DataRow);
                                }
                            }
                        }

                        string tempName = "dbo.TradeTempSecondarySalesActual";
                        var conn = appHelper.ConnectionString();
                        using (SqlConnection sqlCon = new SqlConnection(conn))
                        {
                            sqlCon.Open();
                            using (SqlBulkCopy connect = new SqlBulkCopy(sqlCon))
                            {
                                connect.DestinationTableName = tempName;
                                connect.BatchSize = 5000;
                                connect.BulkCopyTimeout = 7000;
                                for (var i = 0; i <= 20; i++)
                                {
                                    connect.ColumnMappings.Add(i, i + 1);
                                }
                                connect.WriteToServer(dt);
                            }
                        }

                        ErrorMessageResult = _actualRepository.SpImportSecondarySalesActual(usercode, fileName, year, month);
                        if (ErrorMessageResult.FirstOrDefault().Status == "error")
                        {
                            validationResult.Result = false;
                            validationResult.Message = "Failed Upload : " + ErrorMessageResult.FirstOrDefault().Message;
                            validationResult.ID = ErrorMessageResult.FirstOrDefault().ID;
                            return validationResult;
                        }
                    }
                    else
                    {
                        validationResult.Result = false;
                        validationResult.Message = "Failed Upload, File Must be Excel";
                        validationResult.ID = 0;
                        return validationResult;
                    }
                }

                validationResult.Result = true;
                validationResult.Message = ErrorMessageResult.FirstOrDefault().Message;
                validationResult.ID = ErrorMessageResult.FirstOrDefault().ID;
                return validationResult;
            }
            else
            {
                validationResult.Result = false;
                validationResult.Message = "Failed Upload";
                validationResult.ID = 0;
                return validationResult;
            }
        }

        public ValidationDTO InterfaceSpendingPhasing(string year, string month)
        {
            var validationResult = new ValidationDTO();
            var ErrorMessageInsert = new List<ErrorMessage>();
            var ErrorMessageResult = new List<ErrorMessage>();
            var usercode = appHelper.UserName;

            ErrorMessageInsert = _actualRepository.SpInsertSpendingPhasingActual(usercode, year, month, 1);
            ErrorMessageResult = _actualRepository.SpInterfaceSpendingPhasingActual(usercode, year, month);
            if (ErrorMessageResult.FirstOrDefault().Status == "error")
            {
                validationResult.Result = false;
                validationResult.Message = "Failed Import : " + ErrorMessageResult.FirstOrDefault().Message;
                validationResult.ID = ErrorMessageResult.FirstOrDefault().ID;
                return validationResult;
            }
            else
            {
                validationResult.Result = true;
                validationResult.Message = ErrorMessageResult.FirstOrDefault().Message;
                validationResult.ID = ErrorMessageResult.FirstOrDefault().ID;
                return validationResult;
            }
        }

        public async Task<string> UploadedFile(IList<IFormFile> files, string dir, string defaultFileNamexlsx, int seq = 0)
        {
            try
            {
                string strFiles = "";
                var fullPath = "";
                if (files.Count > 0)
                {

                    var file = files[0];
                    if (file != null && file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        strFiles = fileName.ToString();

                        // Get Mime Type
                        var ext = Path.GetExtension(fileName);
                        if (ext == ".xlsx")
                        {
                            var pathName = dir;
                            //var path = $"D:\Upload\" + pathName;
                            var path = $"{appHelper.UploadTargetDir}\\{pathName}";
                            bool isExists = System.IO.Directory.Exists(path);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(path);

                            var dataCurrentDate = _globalRepository.GetCurrentDate();
                            DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);
                            fullPath = Path.Combine(path, defaultFileNamexlsx + "_" + appHelper.UserName + "_" + dateNow.ToString("yyyyMMdd") + ".xlsx");

                            //string currentPath = Request.MapPath(fullPath);

                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }

                            using (FileStream output = System.IO.File.Create(fullPath))
                            {
                                await file.CopyToAsync(output);
                            }
                        }
                    }
                }
                return fullPath;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
