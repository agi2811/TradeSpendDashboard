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
using TradeSpendDashboard.Models.Entity.TradeSpendDashboard.Transaction.Update;
using TradeSpendDashboard.Models.Entity.Transaction;

namespace TradeSpendDashboard.Data.Services.Transaction
{
    public class UpdateService : IUpdateService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly ExcelHelper _excelHelper;
        private readonly MailHelper _mailHelper;
        private readonly IUpdateRepository _updateRepository;
        private readonly IGlobalRepository _globalRepository;
        private readonly IMapper mapper;

        public UpdateService(
            ILogger<UpdateService> logger,
            AppHelper appHelper,
            ExcelHelper excelHelper,
            IUpdateRepository updateRepository,
            IGlobalRepository globalRepository,
            MailHelper mailHelper,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.appHelper = appHelper;
            this._updateRepository = updateRepository;
            this._excelHelper = excelHelper;
            this._mailHelper = mailHelper;
            this._globalRepository = globalRepository;
        }

        public Task<List<dynamic>> GetPrimarySalesUpdate(string year, string month)
        {
            var data = _updateRepository.GetPrimarySalesUpdate(appHelper.UserName, year, month);
            return data;
        }

        public Task<List<dynamic>> GetSecondarySalesUpdate(string year, string month)
        {
            var data = _updateRepository.GetSecondarySalesUpdate(appHelper.UserName, year, month);
            return data;
        }

        public Task<List<dynamic>> GetSpendingPhasingUpdate(string year, string month)
        {
            var data = _updateRepository.GetSpendingPhasingUpdate(appHelper.UserName, year, month);
            return data;
        }

        public Task<List<dynamic>> GetYearsPrimarySalesUpdate()
        {
            var data = _updateRepository.GetYearsPrimarySalesUpdate();
            return data;
        }

        public Task<List<dynamic>> GetYearsSecondarySalesUpdate()
        {
            var data = _updateRepository.GetYearsSecondarySalesUpdate();
            return data;
        }

        public Task<List<dynamic>> GetYearsSpendingPhasingUpdate()
        {
            var data = _updateRepository.GetYearsSpendingPhasingUpdate();
            return data;
        }

        public async Task<dynamic> CheckPrimarySalesHeadUpdate(string year, string month)
        {
            var data = await _updateRepository.CheckPrimarySalesHeadUpdate(year, month);
            return data;
        }

        public async Task<dynamic> CheckSecondarySalesHeadUpdate(string year, string month)
        {
            var data = await _updateRepository.CheckSecondarySalesHeadUpdate(year, month);
            return data;
        }

        public async Task<dynamic> CheckSpendingPhasingHeadUpdate(string year, string month)
        {
            var data = await _updateRepository.CheckSpendingPhasingHeadUpdate(year, month);
            return data;
        }

        public async Task<long> CreatePrimarySalesHeadUpdate(string userCode, string year, string month)
        {
            var headUpdate = new TradeHeadPrimarySalesUpdate();
            var dataCurrentDate = _globalRepository.GetCurrentDate();
            DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);

            try
            {
                headUpdate.Year = year;
                headUpdate.Month = month;
                headUpdate.IsLocked = false;
                headUpdate.IsActive = true;
                headUpdate.CreatedDate = dateNow;
                headUpdate.CreatedBy = userCode.ToString();
                headUpdate.UpdatedDate = dateNow;
                headUpdate.UpdatedBy = userCode.ToString();
                headUpdate = await _updateRepository.AddPrimarySalesHeadUpdate(headUpdate);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<long> CreateSecondarySalesHeadUpdate(string userCode, string year, string month)
        {
            var headUpdate = new TradeHeadSecondarySalesUpdate();
            var dataCurrentDate = _globalRepository.GetCurrentDate();
            DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);

            try
            {
                headUpdate.Year = year;
                headUpdate.Month = month;
                headUpdate.IsLocked = false;
                headUpdate.IsActive = true;
                headUpdate.CreatedDate = dateNow;
                headUpdate.CreatedBy = userCode.ToString();
                headUpdate.UpdatedDate = dateNow;
                headUpdate.UpdatedBy = userCode.ToString();
                headUpdate = await _updateRepository.AddSecondarySalesHeadUpdate(headUpdate);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<long> CreateSpendingPhasingHeadUpdate(string userCode, string year, string month)
        {
            var headUpdate = new TradeHeadSpendingUpdate();
            var dataCurrentDate = _globalRepository.GetCurrentDate();
            DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);

            try
            {
                headUpdate.Year = year;
                headUpdate.Month = month;
                headUpdate.IsLocked = false;
                headUpdate.IsActive = true;
                headUpdate.CreatedDate = dateNow;
                headUpdate.CreatedBy = userCode.ToString();
                headUpdate.UpdatedDate = dateNow;
                headUpdate.UpdatedBy = userCode.ToString();
                headUpdate = await _updateRepository.AddSpendingPhasingHeadUpdate(headUpdate);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<long> UpdatePrimarySalesHeadUpdate(string userCode, string year, string month, bool isLock = false)
        {
            var dataCurrentDate = _globalRepository.GetCurrentDate();
            DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);

            try
            {
                var headUpdate = await _updateRepository.GetPrimarySalesHeadUpdate(year, month);
                if (headUpdate != null)
                {
                    headUpdate.Year = year;
                    headUpdate.Month = month;
                    headUpdate.IsActive = true;
                    if (isLock == true)
                    {
                        headUpdate.IsLocked = true;
                        headUpdate.UpdatedDate = dateNow;
                        headUpdate.UpdatedBy = userCode.ToString();
                    }
                    var updated = await _updateRepository.UpdatePrimarySalesHeadUpdate(headUpdate);
                }

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }   
        }

        public async Task<long> UpdateSecondarySalesHeadUpdate(string userCode, string year, string month, bool isLock = false)
        {
            var dataCurrentDate = _globalRepository.GetCurrentDate();
            DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);

            try
            {
                var headUpdate = await _updateRepository.GetSecondarySalesHeadUpdate(year, month);
                if (headUpdate != null)
                {
                    headUpdate.Year = year;
                    headUpdate.Month = month;
                    headUpdate.IsActive = true;
                    if (isLock == true)
                    {
                        headUpdate.IsLocked = true;
                        headUpdate.UpdatedDate = dateNow;
                        headUpdate.UpdatedBy = userCode.ToString();
                    }
                    var updated = await _updateRepository.UpdateSecondarySalesHeadUpdate(headUpdate);
                }

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<long> UpdateSpendingPhasingHeadUpdate(string userCode, string year, string month, bool isLock = false)
        {
            var dataCurrentDate = _globalRepository.GetCurrentDate();
            DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);

            try
            {
                var headUpdate = await _updateRepository.GetSpendingPhasingHeadUpdate(year, month);
                if (headUpdate != null)
                {
                    headUpdate.Year = year;
                    headUpdate.Month = month;
                    headUpdate.IsActive = true;
                    if (isLock == true)
                    {
                        headUpdate.IsLocked = true;
                        headUpdate.UpdatedDate = dateNow;
                        headUpdate.UpdatedBy = userCode.ToString();
                    }
                    var updated = await _updateRepository.UpdateSpendingPhasingHeadUpdate(headUpdate);
                }

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
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
                fileName = UploadedFile(file, "Update/PrimarySales", "PrimarySales").GetAwaiter().GetResult();
                XSSFWorkbook xssf;
                ISheet sheet = null;

                _updateRepository.TruncateTempMappingPrimarySales(usercode, year, month);
                using (FileStream files = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    string extension = Path.GetExtension(fileName);
                    var dataCurrentDate = _globalRepository.GetCurrentDate();
                    DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);
                    if (extension == ".xlsx")
                    {
                        xssf = new XSSFWorkbook(files);
                        sheet = xssf.GetSheetAt(0); //GetSheet("Sheet1");
                        DataTable dt = new DataTable("TradeTempPrimarySalesUpdate");
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
                                    (sheet.GetRow(i).GetCell(2).StringCellValue != "" && sheet.GetRow(i).GetCell(2).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(3).StringCellValue != "" && sheet.GetRow(i).GetCell(3).StringCellValue != null))
                                {
                                    if (year + " - " + month != _excelHelper.GetStringVal(sheet, i, 0))
                                    {
                                        validationResult.Result = false;
                                        validationResult.Message = "Period data does not match";
                                        validationResult.ID = 0;
                                        return validationResult;
                                    }

                                    DataRow DataRow = dt.NewRow();
                                    DataRow["YearPeriod"] = year;
                                    DataRow["MonthPeriod"] = month;
                                    DataRow["Channel"] = sheet.GetRow(i).GetCell(1) == null ? "" : _excelHelper.GetStringVal(sheet, i, 1);
                                    DataRow["ProfitCenter"] = sheet.GetRow(i).GetCell(2) == null ? "" : _excelHelper.GetStringVal(sheet, i, 2);
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

                        string tempName = "dbo.TradeTempPrimarySalesUpdate";
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

                        ErrorMessageResult = _updateRepository.SpImportPrimarySalesUpdate(usercode, fileName, year, month);
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
                fileName = UploadedFile(file, "Update/SecondarySales", "SecondarySales").GetAwaiter().GetResult();
                XSSFWorkbook xssf;
                ISheet sheet = null;

                _updateRepository.TruncateTempMappingSecondarySales(usercode, year, month);
                using (FileStream files = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    string extension = Path.GetExtension(fileName);
                    var dataCurrentDate = _globalRepository.GetCurrentDate();
                    DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);
                    if (extension == ".xlsx")
                    {
                        xssf = new XSSFWorkbook(files);
                        sheet = xssf.GetSheetAt(0); //GetSheet("Sheet1");
                        DataTable dt = new DataTable("TradeTempSecondarySalesUpdate");
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
                                    (sheet.GetRow(i).GetCell(3).StringCellValue != "" && sheet.GetRow(i).GetCell(3).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(4).StringCellValue != "" && sheet.GetRow(i).GetCell(4).StringCellValue != null))
                                {
                                    if (year + " - " + month != _excelHelper.GetStringVal(sheet, i, 0))
                                    {
                                        validationResult.Result = false;
                                        validationResult.Message = "Period data does not match";
                                        validationResult.ID = 0;
                                        return validationResult;
                                    }

                                    DataRow DataRow = dt.NewRow();
                                    DataRow["YearPeriod"] = year;
                                    DataRow["MonthPeriod"] = month;
                                    DataRow["NewChannel"] = sheet.GetRow(i).GetCell(1) == null ? "" : _excelHelper.GetStringVal(sheet, i, 1);
                                    DataRow["OldChannel"] = sheet.GetRow(i).GetCell(2) == null ? "" : _excelHelper.GetStringVal(sheet, i, 2);
                                    DataRow["Customer"] = sheet.GetRow(i).GetCell(3) == null ? "" : _excelHelper.GetStringVal(sheet, i, 3);
                                    DataRow["Category"] = sheet.GetRow(i).GetCell(4) == null ? "" : _excelHelper.GetStringVal(sheet, i, 4);
                                    DataRow["Year"] = year;
                                    DataRow["Jan"] = sheet.GetRow(i).GetCell(5) == null || _excelHelper.GetStringVal(sheet, i, 5) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 5);
                                    DataRow["Feb"] = sheet.GetRow(i).GetCell(6) == null || _excelHelper.GetStringVal(sheet, i, 6) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 6);
                                    DataRow["Mar"] = sheet.GetRow(i).GetCell(7) == null || _excelHelper.GetStringVal(sheet, i, 7) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 7);
                                    DataRow["Apr"] = sheet.GetRow(i).GetCell(8) == null || _excelHelper.GetStringVal(sheet, i, 8) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 8);
                                    DataRow["May"] = sheet.GetRow(i).GetCell(9) == null || _excelHelper.GetStringVal(sheet, i, 9) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 9);
                                    DataRow["Jun"] = sheet.GetRow(i).GetCell(10) == null || _excelHelper.GetStringVal(sheet, i, 10) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 10);
                                    DataRow["Jul"] = sheet.GetRow(i).GetCell(11) == null || _excelHelper.GetStringVal(sheet, i, 11) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 11);
                                    DataRow["Aug"] = sheet.GetRow(i).GetCell(12) == null || _excelHelper.GetStringVal(sheet, i, 12) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 12);
                                    DataRow["Sep"] = sheet.GetRow(i).GetCell(13) == null || _excelHelper.GetStringVal(sheet, i, 13) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 13);
                                    DataRow["Oct"] = sheet.GetRow(i).GetCell(14) == null || _excelHelper.GetStringVal(sheet, i, 14) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 14);
                                    DataRow["Nov"] = sheet.GetRow(i).GetCell(15) == null || _excelHelper.GetStringVal(sheet, i, 15) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 15);
                                    DataRow["Dec"] = sheet.GetRow(i).GetCell(16) == null || _excelHelper.GetStringVal(sheet, i, 16) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 16);
                                    DataRow["InsertedBy"] = appHelper.UserName.ToString();
                                    DataRow["InsertedOn"] = dateNow;
                                    dt.Rows.Add(DataRow);
                                }
                            }
                        }

                        string tempName = "dbo.TradeTempSecondarySalesUpdate";
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

                        ErrorMessageResult = _updateRepository.SpImportSecondarySalesUpdate(usercode, fileName, year, month);
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

        public ValidationDTO ImportSpendingPhasing(IList<IFormFile> file, bool isBulk, string year, string month, string budgetOwner, List<string> categoryList, List<string> profitCenterList)
        {
            var validationResult = new ValidationDTO();
            var ErrorMessageResult = new List<ErrorMessage>();
            var fileName = "";
            var usercode = appHelper.UserName;

            if (file[0].FileName != "")
            {
                //var SeqAttachCount = repository.GetCountAttachFile("SpendingPhasing", usercode);
                fileName = UploadedFile(file, "Update/SpendingPhasing", "SpendingPhasing").GetAwaiter().GetResult();
                XSSFWorkbook xssf;
                ISheet sheet = null;

                _updateRepository.TruncateTempMappingSpendingPhasing(usercode, year, month);
                using (FileStream files = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    string extension = Path.GetExtension(fileName);
                    var dataCurrentDate = _globalRepository.GetCurrentDate();
                    var categoryListString = categoryList[0] != null ? string.Join(",", categoryList.Select(x => x.ToString()).ToArray()) : "-";
                    var profitCenterListString = profitCenterList[0] != null ? string.Join(",", profitCenterList.Select(x => x.ToString()).ToArray()) : "-";

                    DateTime dateNow = Convert.ToDateTime(dataCurrentDate.Date);
                    if (extension == ".xlsx")
                    {
                        xssf = new XSSFWorkbook(files);
                        sheet = xssf.GetSheetAt(0); //GetSheet("Sheet1");
                        DataTable dt = new DataTable("TradeTempSpendingUpdate");
                        dt.Columns.Add("YearPeriod");
                        dt.Columns.Add("MonthPeriod");
                        dt.Columns.Add("BudgetOwnerComp");
                        dt.Columns.Add("BudgetOwner");
                        dt.Columns.Add("ProfitCenterComp");
                        dt.Columns.Add("ProfitCenter");
                        dt.Columns.Add("CategoryComp");
                        dt.Columns.Add("Category");
                        dt.Columns.Add("GL");
                        dt.Columns.Add("GLDesc");
                        dt.Columns.Add("GLType");
                        dt.Columns.Add("NewChannel");
                        dt.Columns.Add("Customer");
                        dt.Columns.Add("OldChannel");
                        dt.Columns.Add("MG3");
                        dt.Columns.Add("MG4");
                        dt.Columns.Add("Activity");
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
                                    (sheet.GetRow(i).GetCell(3).StringCellValue != "" && sheet.GetRow(i).GetCell(3).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(4).StringCellValue != "" && sheet.GetRow(i).GetCell(4).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(5).StringCellValue != "" && sheet.GetRow(i).GetCell(5).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(6).StringCellValue != "" && sheet.GetRow(i).GetCell(6).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(7).StringCellValue != "" && sheet.GetRow(i).GetCell(7).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(8).StringCellValue != "" && sheet.GetRow(i).GetCell(8).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(9).StringCellValue != "" && sheet.GetRow(i).GetCell(9).StringCellValue != null) ||
                                    (sheet.GetRow(i).GetCell(10).StringCellValue != "" && sheet.GetRow(i).GetCell(10).StringCellValue != null))
                                {
                                    if (year + " - " + month != _excelHelper.GetStringVal(sheet, i, 0))
                                    {
                                        validationResult.Result = false;
                                        validationResult.Message = "Period data does not match";
                                        validationResult.ID = 0;
                                        return validationResult;
                                    }

                                    DataRow DataRow = dt.NewRow();
                                    DataRow["YearPeriod"] = year;
                                    DataRow["MonthPeriod"] = month;
                                    DataRow["BudgetOwnerComp"] = budgetOwner;
                                    DataRow["BudgetOwner"] = sheet.GetRow(i).GetCell(2) == null ? "" : _excelHelper.GetStringVal(sheet, i, 2);
                                    DataRow["ProfitCenterComp"] = profitCenterListString;
                                    DataRow["ProfitCenter"] = sheet.GetRow(i).GetCell(3) == null ? "" : _excelHelper.GetStringVal(sheet, i, 3);
                                    DataRow["CategoryComp"] = categoryListString;
                                    DataRow["Category"] = sheet.GetRow(i).GetCell(4) == null ? "" : _excelHelper.GetStringVal(sheet, i, 4);
                                    DataRow["GL"] = sheet.GetRow(i).GetCell(5) == null ? "" : _excelHelper.GetStringVal(sheet, i, 5);
                                    DataRow["GLDesc"] = sheet.GetRow(i).GetCell(6) == null ? "" : _excelHelper.GetStringVal(sheet, i, 6);
                                    DataRow["GLType"] = sheet.GetRow(i).GetCell(7) == null ? "" : _excelHelper.GetStringVal(sheet, i, 7);
                                    DataRow["NewChannel"] = sheet.GetRow(i).GetCell(8) == null ? "" : _excelHelper.GetStringVal(sheet, i, 8);
                                    DataRow["Customer"] = sheet.GetRow(i).GetCell(9) == null ? "" : _excelHelper.GetStringVal(sheet, i, 9);
                                    DataRow["OldChannel"] = sheet.GetRow(i).GetCell(10) == null ? "" : _excelHelper.GetStringVal(sheet, i, 10);
                                    DataRow["MG3"] = sheet.GetRow(i).GetCell(11) == null ? "" : _excelHelper.GetStringVal(sheet, i, 11);
                                    DataRow["MG4"] = sheet.GetRow(i).GetCell(12) == null ? "" : _excelHelper.GetStringVal(sheet, i, 12);
                                    DataRow["Activity"] = sheet.GetRow(i).GetCell(13) == null ? "" : _excelHelper.GetStringVal(sheet, i, 13);
                                    DataRow["Year"] = sheet.GetRow(i).GetCell(1) == null ? "" : _excelHelper.GetStringVal(sheet, i, 1); ;
                                    DataRow["Jan"] = sheet.GetRow(i).GetCell(14) == null || _excelHelper.GetStringVal(sheet, i, 14) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 14);
                                    DataRow["Feb"] = sheet.GetRow(i).GetCell(15) == null || _excelHelper.GetStringVal(sheet, i, 15) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 15);
                                    DataRow["Mar"] = sheet.GetRow(i).GetCell(16) == null || _excelHelper.GetStringVal(sheet, i, 16) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 16);
                                    DataRow["Apr"] = sheet.GetRow(i).GetCell(17) == null || _excelHelper.GetStringVal(sheet, i, 17) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 17);
                                    DataRow["May"] = sheet.GetRow(i).GetCell(18) == null || _excelHelper.GetStringVal(sheet, i, 18) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 18);
                                    DataRow["Jun"] = sheet.GetRow(i).GetCell(19) == null || _excelHelper.GetStringVal(sheet, i, 19) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 19);
                                    DataRow["Jul"] = sheet.GetRow(i).GetCell(20) == null || _excelHelper.GetStringVal(sheet, i, 20) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 20);
                                    DataRow["Aug"] = sheet.GetRow(i).GetCell(21) == null || _excelHelper.GetStringVal(sheet, i, 21) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 21);
                                    DataRow["Sep"] = sheet.GetRow(i).GetCell(22) == null || _excelHelper.GetStringVal(sheet, i, 22) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 22);
                                    DataRow["Oct"] = sheet.GetRow(i).GetCell(23) == null || _excelHelper.GetStringVal(sheet, i, 23) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 23);
                                    DataRow["Nov"] = sheet.GetRow(i).GetCell(24) == null || _excelHelper.GetStringVal(sheet, i, 24) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 24);
                                    DataRow["Dec"] = sheet.GetRow(i).GetCell(25) == null || _excelHelper.GetStringVal(sheet, i, 25) == "" ? "0" : _excelHelper.GetStringVal(sheet, i, 25);
                                    DataRow["InsertedBy"] = appHelper.UserName.ToString();
                                    DataRow["InsertedOn"] = dateNow;
                                    dt.Rows.Add(DataRow);
                                }
                            }
                        }

                        string tempName = "dbo.TradeTempSpendingUpdate";
                        var conn = appHelper.ConnectionString();
                        using (SqlConnection sqlCon = new SqlConnection(conn))
                        {
                            sqlCon.Open();
                            using (SqlBulkCopy connect = new SqlBulkCopy(sqlCon))
                            {
                                connect.DestinationTableName = tempName;
                                connect.BatchSize = 5000;
                                connect.BulkCopyTimeout = 7000;
                                for (var i = 0; i <= 31; i++)
                                {
                                    connect.ColumnMappings.Add(i, i + 1);
                                }
                                connect.WriteToServer(dt);
                            }
                        }

                        ErrorMessageResult = _updateRepository.SpImportSpendingPhasingUpdate(usercode, fileName, year, month);
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

        public Task<List<dynamic>> DownloadSpendingPhasingUpdate(string year, string month, string budgetOwner, List<string> categoryList, List<string> profitCenterList)
        {
            var data = _updateRepository.DownloadSpendingPhasingUpdate(appHelper.UserName, year, month, budgetOwner, categoryList, profitCenterList);
            return data;
        }
    }
}
