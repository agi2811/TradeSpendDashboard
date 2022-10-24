using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace TradeSpendDashboard.Models
{
    public class FileModel
    {
    }

    public class UploadExcelModel
    {
        public DataTable datatable { get; set; }
        public ISheet sheet { get; set; }
        public string Username { get; set; }
    }

    public class UploadExcelModelPeriod
    {
        public DataTable datatable { get; set; }
        public ISheet sheet { get; set; }
        public string Username { get; set; }
        public string Period { get; set; }

    }
}