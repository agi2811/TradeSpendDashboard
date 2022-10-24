using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Extensions;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using TradeSpendDashboard.Models.Entity.Flows;
using System.Globalization;
using System.Threading;
using TradeSpendDashboard.Models.Entity.Transaction;
using System;

namespace TradeSpendDashboard.Data.Repository
{
    public class GlobalRepository : CustomRepository<Global>, IGlobalRepository
    {
        public GlobalRepository(TradeSpendDashboardContext context) : base(context) { }

        #region Global

        public string ToTitleCase(string text)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(text);
        }

        public async Task<List<dynamic>> GetMonth(int isBudget)
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT [Month] FROM [dbo].[FN_Get_Month]() WHERE [Month] <> IIF({isBudget} = 0, 'Budget', '') ORDER BY [MonthNo] ASC", param).ToList();
            return dataDynamic;
        }

        public async Task<List<dynamic>> GetYearsReport()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT [Years] FROM [dbo].[FN_Get_Years_Report]() ORDER BY [Years] DESC", param).ToList();
            return dataDynamic;
        }

        public dynamic GetCurrentMonth()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT FORMAT([dbo].[SC_Get_Local_Date](), 'MMMM') [Month]", param).FirstOrDefault();
            return dataDynamic;
        }

        public dynamic GetCurrentYear()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT YEAR([dbo].[SC_Get_Local_Date]()) [Year]", param).FirstOrDefault();
            return dataDynamic;
        }

        public dynamic GetCurrentDate()
        {
            var param = new Dictionary<string, object>();
            var dataDynamic = TradeSpendDashboardContext.CollectionFromSql($"SELECT [dbo].[SC_Get_Local_Date]() [Date]", param).FirstOrDefault();
            return dataDynamic;
        }
        #endregion

        #region Flow
        //public MasterFlow getFlow(long Id)
        //{
        //    return ProductReturnContext.MasterFlow.Where(p => p.Id == Id).FirstOrDefault();
        //}

        //public Requests getRequestByRequestId(long Id)
        //{
        //    return ProductReturnContext.Requests.Where(p => p.Id == Id).FirstOrDefault();
        //}



        //public long getProcessFlowIDByNextFlow(long ProcessStatusFlowID)
        //{
        //    var dataNextFlow = (from p in TradeSpendDashboardContext.MasterFlowProcessStatusNext where p.ProcessStatusFlowID == ProcessStatusFlowID && p.IsActive == true select p).FirstOrDefault();
        //    if (dataNextFlow != null)
        //    {
        //        return dataNextFlow.NextProcessFlowID;
        //    }
        //    return 0;
        //}

        //public long getFlowProcessStatusIDByIsDraft(long FlowID)
        //{
        //    var dataFlowProcess = (from p in TradeSpendDashboardContext.MasterFlowProcess where p.FlowID == FlowID && p.IsStart == true && p.IsActive == true select p).FirstOrDefault();
        //    if (dataFlowProcess != null)
        //    {
        //        var dataFlowProcessStatus = (from p in TradeSpendDashboardContext.MasterFlowProcessStatus where p.FlowProcessID == dataFlowProcess.Id && p.IsDraft == true select p).FirstOrDefault();
        //        if (dataFlowProcessStatus != null) return dataFlowProcessStatus.Id;
        //    }

        //    return 0;
        //}

        //public long getStatusFlowByNextFlow(long NextFlowProcessID, long FlowProcessID)
        //{
        //    var data = (from p in TradeSpendDashboardContext.MasterFlowProcessStatusNext where p.NextProcessFlowID == NextFlowProcessID select p).ToList();
        //    if (data.Count() > 0)
        //    {
        //        if (data.Count() > 1)
        //        {
        //            var data1 = (from p in TradeSpendDashboardContext.MasterFlowProcessStatusNext
        //                         join q in TradeSpendDashboardContext.MasterFlowProcessStatus on p.ProcessStatusFlowID equals (q.Id)
        //                         where p.NextProcessFlowID == NextFlowProcessID && q.FlowProcessID == FlowProcessID
        //                         select p).FirstOrDefault();

        //            return data1.ProcessStatusFlowID;
        //        }
        //        return data.Select(p => p.ProcessStatusFlowID).FirstOrDefault();
        //    }

        //    return 0;
        //}

        //public long getDefaultNextFlowProcess(long FlowProcessID)
        //{
        //    long DefaultNextFlowProcess = 0;
        //    var dataFlowProcess = (from p in TradeSpendDashboardContext.MasterFlowProcess where p.Id == FlowProcessID select p).FirstOrDefault();
        //    if (dataFlowProcess != null)
        //    {
        //        DefaultNextFlowProcess = (long)(dataFlowProcess.DefaultNextFlowProcess != null ? dataFlowProcess.DefaultNextFlowProcess : 0);
        //    }

        //    return DefaultNextFlowProcess;
        //}



        //public long GetFlowIdByProccess(long flowProccesId)
        //{
        //    var data = (from p in TradeSpendDashboardContext.MasterFlowProcess where p.Id == flowProccesId select p).FirstOrDefault();
        //    if (data != null)
        //        return data.FlowID;

        //    return 0;
        //}

        //public MasterFlow GetFlowDataByID(long FlowID)
        //{
        //    var data = (from p in ProductReturnContext.MasterFlow where p.Id == FlowID select p).FirstOrDefault();

        //    return data;
        //}


        //public MasterFlowProcessStatus GetFlowProcessStatusDataByID(long id)
        //{
        //    var data = (from p in ProductReturnContext.MasterFlowProcessStatus where p.Id == id select p).FirstOrDefault();

        //    return data;
        //}

        //public bool IsDraft(long flowProcessStatusId)
        //{
        //    var data = (from p in TradeSpendDashboardContext.MasterFlowProcessStatus where p.Id == flowProcessStatusId && p.IsDraft == true select p).FirstOrDefault();
        //    if (data != null) return true;

        //    return false;
        //}

        //public long GetProcessFlowIdByIsStart(long flowId, bool isECommerce = false)
        //{
        //    var dataFlowProccess = (from p in TradeSpendDashboardContext.MasterFlowProcess where p.FlowID == flowId && p.IsStart == true && p.IsECommerce == isECommerce && p.IsActive select p).FirstOrDefault();
        //    if (dataFlowProccess == null) return 0;
        //    return dataFlowProccess.Id;
        //}

        //public List<MasterFlowProcessStatus> GetStatusByProcessId(long Id)
        //{
        //    var data = ProductReturnContext.MasterFlowProcessStatus.Where(a => a.FlowProcessID.Equals(Id) && a.IsActive).ToList();
        //    return data;
        //}

        //public List<MyRequestTaskModel> SP_GetTaskByUser(string userCode)
        //{
        //    try
        //    {
        //        var data = TradeSpendDashboardContext.Set<MyRequestTaskModel>().FromSqlRaw(@"exec [dbo].[SP_GetTaskByUser] '" + userCode + "'").ToList();
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;

        //    }
        //}

        #endregion



    }
}
