using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.DTO;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Flows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.Interface
{
    public interface IGlobalService 
    {
        string ToTitleCase(string text);
        dynamic GetCurrentMonth();
        dynamic GetCurrentYear();
        dynamic GetCurrentDate();
        //MasterFlow getFlow(long Id);
        //long getProcessFlowIDByNextFlow(long ProcessStatusFlowID);
        //MasterFlowProcessStatus GetFlowProcessStatusDataByID(long id);

        //long getFlowProcessStatusIDByIsDraft(long FlowID);
        //long getStatusFlowByNextFlow(long NextFlowProcessID, long FlowProcessID);
        //long getDefaultNextFlowProcess(long FlowProcessID);
        //long GetFlowIdByProccess(long flowProccesId);
        //MasterFlow GetFlowDataByID(long FlowID);
        //bool IsDraft(long flowProcessStatusId);
        //long GetProcessFlowIdByIsStart(long flowId);
        //List<MasterFlowProcessStatus> GetStatusByProcessId(long Id);
    }
}
