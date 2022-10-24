using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace TradeSpendDashboard
{
    public class ExcelHelper
    {
        public string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        public int GetOffset(int page, int limit)
        {
            try
            {
                int offset = (page - 1) * limit;
                return offset;
            }
            catch (Exception err)
            {
                return 0;
                throw;
            }
        }

        public static Color FromHex(string hex)
        {
            FromHex(hex, out var a, out var r, out var g, out var b);

            return Color.FromArgb(a, r, g, b);
        }

        public static void FromHex(string hex, out byte a, out byte r, out byte g, out byte b)
        {
            hex = ToRgbaHex(hex);
            if (hex == null || !uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var packedValue))
            {
                throw new ArgumentException("Hexadecimal string is not in the correct format.", nameof(hex));
            }

            a = (byte)(packedValue >> 0);
            r = (byte)(packedValue >> 24);
            g = (byte)(packedValue >> 16);
            b = (byte)(packedValue >> 8);
        }


        private static string ToRgbaHex(string hex)
        {
            hex = hex.StartsWith("#") ? hex.Substring(1) : hex;

            if (hex.Length == 8)
            {
                return hex;
            }

            if (hex.Length == 6)
            {
                return hex + "FF";
            }

            if (hex.Length < 3 || hex.Length > 4)
            {
                return null;
            }

            //Handle values like #3B2
            string red = char.ToString(hex[0]);
            string green = char.ToString(hex[1]);
            string blue = char.ToString(hex[2]);
            string alpha = hex.Length == 3 ? "F" : char.ToString(hex[3]);


            return string.Concat(red, red, green, green, blue, blue, alpha, alpha);
        }

        public void SetBackgroundColorHex(XSSFWorkbook workbook, ISheet sheet, ICell cell, string HexCode, string align = "center")
        {
            //Color CellColor = ColorTranslator.FromHtml(HexCode);
            Color CellColor = FromHex(HexCode);
            XSSFCellStyle myStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            XSSFColor myColor = new XSSFColor(CellColor);
            myStyle.SetFillBackgroundColor(myColor);
            myStyle.SetFillForegroundColor(myColor);
            myStyle.WrapText = true;
            myStyle.FillPattern = FillPattern.SolidForeground;

            myStyle.VerticalAlignment = VerticalAlignment.Center;
            switch (align.ToLower())
            {
                case "center":
                    myStyle.Alignment = HorizontalAlignment.Center;
                    break;
                case "left":
                    myStyle.Alignment = HorizontalAlignment.Left;
                    break;
                case "right":
                    myStyle.Alignment = HorizontalAlignment.Right;
                    break;
                default:
                    myStyle.Alignment = HorizontalAlignment.Justify;
                    break;
            }
            cell.CellStyle = myStyle;
        }

        public void SetData(ISheet sheet, XSSFWorkbook workbook, ICell cell, string typevalue, string value, string color, bool fit, string align)
        {
            try
            {
                #region Set Cell Style
                switch (typevalue.ToLower())
                {
                    case "formula":
                        cell.SetCellFormula(value);
                        break;
                    case "decimal":
                        cell.SetCellValue(Convert.ToDouble(Math.Round(Convert.ToDecimal(value), 2)));
                        break;
                    case "number":
                        cell.SetCellValue(Convert.ToInt64(value));
                        break;
                    default:
                        cell.SetCellValue(value);
                        break;
                }

                if (fit)
                {
                    sheet.AutoSizeColumn(cell.ColumnIndex);
                }

                if (color != "")
                {
                    SetBackgroundColorHex(workbook, sheet, cell, color, align);
                }
                #endregion End Set Cell Style
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }

        public void SetMerge(ISheet sheet, XSSFWorkbook workbook, ICell cell, string value, int startRow, int lastRow, int startCell, int lastCell, string align = "center", bool SetCellColor = false, string color = "")
        {
            try
            {
                ICellStyle cellStyle = workbook.CreateCellStyle();

                var cra = new NPOI.SS.Util.CellRangeAddress(startRow, lastRow, startCell, lastCell);
                sheet.AddMergedRegion(cra);

                #region Set Cell Style
                cell.SetCellValue(value);
                cell.CellStyle = cellStyle;

                if (SetCellColor)
                {
                    SetBackgroundColorHex(workbook, sheet, cell, color, align);
                }
                #endregion End Set Cell Style
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }

        public int SetMergeResult(ISheet sheet, IWorkbook workbook, ICell cell, string value, int startRow, int lastRow, int startCell, int lastCell)
        {
            try
            {
                IDataFormat format = workbook.CreateDataFormat();
                ICellStyle cellStyle = workbook.CreateCellStyle();
                cellStyle.VerticalAlignment = VerticalAlignment.Center;
                cellStyle.Alignment = HorizontalAlignment.Center;
                cell.SetCellValue(value);
                cell.CellStyle = cellStyle;
                var cra = new NPOI.SS.Util.CellRangeAddress(startRow, lastRow, startCell, lastCell);
                return sheet.AddMergedRegion(cra);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }

        public void SetDateFormat(IWorkbook workbook, ICell cell, string value)
        {
            try
            {
                IDataFormat format = workbook.CreateDataFormat();
                short formatId = format.GetFormat("dd MMM yyyy");
                //short formatId = format.GetFormat("dd MMM yyyy");
                //set value for the cell
                if (!string.IsNullOrEmpty(value))
                    cell.SetCellValue(Convert.ToDateTime(value));

                ICellStyle cellStyle = workbook.CreateCellStyle();
                cellStyle.DataFormat = formatId;
                cell.CellStyle = cellStyle;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }

        public void SetDateTimeFormat(IWorkbook workbook, ICell cell, string value)
        {
            try
            {
                IDataFormat format = workbook.CreateDataFormat();
                short formatId = format.GetFormat("dd MMM yyyy hh:mm:ss");
                //set value for the cell
                if (!string.IsNullOrEmpty(value))
                    cell.SetCellValue(Convert.ToDateTime(value));

                ICellStyle cellStyle = workbook.CreateCellStyle();
                cellStyle.DataFormat = formatId;
                cell.CellStyle = cellStyle;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }

        public void SetCellType(ISheet sheet, int NumberRow, int CellNumber, string type, IWorkbook workbook = null)
        {
            switch (type)
            {
                case "string":
                    sheet.GetRow(NumberRow).GetCell(CellNumber).SetCellType(CellType.String);
                    break;
                case "number":
                    sheet.GetRow(NumberRow).GetCell(CellNumber).SetCellType(CellType.Numeric);
                    break;
                case "date":
                    IDataFormat dataFormatCustom = workbook.CreateDataFormat();
                    var cell = sheet.GetRow(NumberRow).GetCell(CellNumber);
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("dd-MMM-yyyy");
                    break;
                default:
                    break;
            }
        }

        public string GetStringVal(ISheet sheet, int NumRow, int CellNum)
        {
            try
            {
                var check = sheet.GetRow(NumRow).GetCell(CellNum);
                SetCellType(sheet, NumRow, CellNum, "string");

                var val = sheet.GetRow(NumRow).GetCell(CellNum).StringCellValue;
                return val;
            }
            catch (Exception err)
            {
                return "";
                throw;
            }
        }

        public String GetDateVal(ISheet sheet, int NumRow, int CellNum, string format = "")
        {
            try
            {
                ICell cellDate = sheet.GetRow(NumRow).GetCell(CellNum);
                if (DateUtil.IsCellDateFormatted(cellDate))
                {
                    DateTime date = cellDate.DateCellValue;
                    ICellStyle style = cellDate.CellStyle;
                    // Excel uses lowercase m for month whereas .Net uses uppercase
                    string formatcell = style.GetDataFormatString().Replace('m', 'M');
                    if (format != "")
                    {
                        formatcell = format;
                    }
                    return date.ToString(formatcell);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int GetIntVal(ISheet sheet, int NumRow, int CellNum)
        {
            try
            {
                SetCellType(sheet, NumRow, CellNum, "number");
                int val = Convert.ToInt32(sheet.GetRow(NumRow).GetCell(CellNum).NumericCellValue);
                return val;
            }
            catch (Exception err)
            {
                return 0;
                throw;
            }
        }

        public double GetDoubleVal(ISheet sheet, int NumRow, int CellNum)
        {
            try
            {
                SetCellType(sheet, NumRow, CellNum, "number");
                Double val = Convert.ToDouble(sheet.GetRow(NumRow).GetCell(CellNum).NumericCellValue);
                return val;
            }
            catch (Exception err)
            {
                return 0;
                throw;
            }
        }

        public decimal GetDecimalVal(ISheet sheet, int NumRow, int CellNum)
        {
            try
            {
                SetCellType(sheet, NumRow, CellNum, "number");
                Decimal val = Convert.ToDecimal(sheet.GetRow(NumRow).GetCell(CellNum).NumericCellValue);
                return val;
            }
            catch (Exception err)
            {
                return 0;
                throw;
            }
        }

        public decimal GetDecimalValFromString(ISheet sheet, int NumRow, int CellNum)
        {
            try
            {
                Decimal val = 0;
                if (sheet.GetRow(NumRow).GetCell(CellNum).CellType == CellType.String)
                    val = Convert.ToDecimal(sheet.GetRow(NumRow).GetCell(CellNum).StringCellValue);
                else if (sheet.GetRow(NumRow).GetCell(CellNum).CellType == CellType.Numeric)
                    val = Convert.ToDecimal(sheet.GetRow(NumRow).GetCell(CellNum).NumericCellValue);
                else
                    throw new Exception("Invalid data type at column Comp. Scrap (row: " + NumRow + ").");
                return val;
            }
            catch (Exception err)
            {
                return 0;
                throw;
            }
        }

        public long GetLongVal(ISheet sheet, int NumRow, int CellNum)
        {
            try
            {
                SetCellType(sheet, NumRow, CellNum, "number");
                long val = Convert.ToInt64(sheet.GetRow(NumRow).GetCell(CellNum).NumericCellValue);
                return val;
            }
            catch (Exception err)
            {
                return 0;
                throw;
            }
        }

        public void SetCellColor(IWorkbook workbook, ICell Cell, string Color = "blue")
        {
            IFont font = workbook.CreateFont();
            ICellStyle styleCell = workbook.CreateCellStyle();
            font.FontHeight = 11;
            font.FontHeightInPoints = 11;
            styleCell.FillPattern = FillPattern.SolidForeground;
            styleCell.VerticalAlignment = VerticalAlignment.Center;
            styleCell.Alignment = HorizontalAlignment.Center;

            font.Boldweight = 700;

            switch (Color.ToLower())
            {
                case "green":
                    styleCell.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Green.Index;
                    font.Color = (NPOI.HSSF.Util.HSSFColor.White.Index);
                    break;
                case "blue":
                    styleCell.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;
                    font.Color = (NPOI.HSSF.Util.HSSFColor.White.Index);
                    break;
                case "yellow":
                    styleCell.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
                    font.Color = (NPOI.HSSF.Util.HSSFColor.White.Index);
                    break;
                case "red":
                    styleCell.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
                    font.Color = (NPOI.HSSF.Util.HSSFColor.White.Index);
                    break;
            }

            styleCell.SetFont(font);
            Cell.CellStyle = styleCell;
        }

        public string GetMonthName(int mth)
        {
            DateTimeFormatInfo dateTimeInfo = new DateTimeFormatInfo();
            return dateTimeInfo.GetAbbreviatedMonthName(mth);
        }

        public List<string> MonthList()
        {
            List<string> data = new List<string>();
            data.Add("Jan");
            data.Add("Feb");
            data.Add("Mar");
            data.Add("Apr");
            data.Add("May");
            data.Add("Jun");
            data.Add("Jul");
            data.Add("Aug");
            data.Add("Sep");
            data.Add("Oct");
            data.Add("Nov");
            data.Add("Dec");
            return data;
        }

        public ICell GetCell(XSSFWorkbook workbook, ISheet sheet, IRow row, int cellNum)
        {
            row.CreateCell(cellNum);
            var cell = row.GetCell(cellNum);
            return cell;
        }

        public void SetCellValue(XSSFWorkbook workbook, ISheet sheet, IRow row, int cellNum, string Val, string color = "#ffffff")
        {
            var current_cell = GetCell(workbook, sheet, row, cellNum);
            current_cell.SetCellValue(Val);
            SetBackgroundColorHex(workbook, sheet, current_cell, color, "center");
        }

        public string GetFormattedCellValue(ICell cell, IFormulaEvaluator eval = null)
        {
            if (cell != null)
            {
                switch (cell.CellType)
                {
                    case CellType.String:
                        return cell.StringCellValue;

                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            DateTime date = cell.DateCellValue;
                            ICellStyle style = cell.CellStyle;
                            // Excel uses lowercase m for month whereas .Net uses uppercase
                            string format = style.GetDataFormatString().Replace('m', 'M');
                            return date.ToString(format);
                        }
                        else
                        {
                            return cell.NumericCellValue.ToString();
                        }

                    case CellType.Boolean:
                        return cell.BooleanCellValue ? "TRUE" : "FALSE";

                    case CellType.Formula:
                        if (eval != null)
                            return GetFormattedCellValue(eval.EvaluateInCell(cell));
                        else
                            return cell.CellFormula;

                    case CellType.Error:
                        return FormulaError.ForInt(cell.ErrorCellValue).String;
                }
            }
            // null or blank cell, or unknown cell type
            return string.Empty;
        }
    }
}
