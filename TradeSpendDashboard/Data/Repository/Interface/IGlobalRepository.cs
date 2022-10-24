using TradeSpendDashboard.Models.Entity.Flows;
using TradeSpendDashboard.Models.Entity.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Repository.Interface
{
    public interface IGlobalRepository : ICustomRepository<Global>
    {
        string ToTitleCase(string text);
        Task<List<dynamic>> GetMonth(int isBudget);
        Task<List<dynamic>> GetYearsReport();
        dynamic GetCurrentMonth();
        dynamic GetCurrentYear();
        dynamic GetCurrentDate();
        //MasterFlow getFlow(long Id);
        //Requests getRequestByRequestId(long Id);
        //long getProcessFlowIDByNextFlow(long ProcessStatusFlowID);
        //MasterFlowProcessStatus GetFlowProcessStatusDataByID(long id);
        //long getFlowProcessStatusIDByIsDraft(long FlowID);
        //long getStatusFlowByNextFlow(long NextFlowProcessID, long FlowProcessID);
        //long getDefaultNextFlowProcess(long FlowProcessID);
        //long GetFlowIdByProccess(long flowProccesId);
        //MasterFlow GetFlowDataByID(long FlowID);
        //bool IsDraft(long flowProcessStatusId);
        //long GetProcessFlowIdByIsStart(long flowId, bool isECommerce = false);
        //List<MasterFlowProcessStatus> GetStatusByProcessId(long Id);
        //List<MyRequestTaskModel> SP_GetTaskByUser(string userCode);
    }
}
