using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeSpendDashboard.Models.Entity.Flows;
using TradeSpendDashboard.Model.DTO;

namespace TradeSpendDashboard.Data.Services
{
    public class GlobalService : IGlobalService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IGlobalRepository repository;
        private readonly IMapper mapper;

        public GlobalService(
            ILogger<GlobalDTO> logger,
            AppHelper appHelper,
            IGlobalRepository repository,
            IMapper mapper
            )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Global, GlobalDTO>();
                cfg.CreateMap<GlobalDTO, Global>();
            });
            mapper = new Mapper(config);

            this.mapper = mapper;
        }

        public dynamic GetCurrentMonth()
        {
            var data = repository.GetCurrentMonth();
            return data;
        }

        public dynamic GetCurrentYear()
        {
            var data = repository.GetCurrentYear();
            return data;
        }

        public dynamic GetCurrentDate()
        {
            var data = repository.GetCurrentDate();
            return data;
        }

        //public long getDefaultNextFlowProcess(long FlowProcessID)
        //{
        //    var data = repository.getDefaultNextFlowProcess(FlowProcessID);
        //    return data;
        //}

        //public MasterFlow getFlow(long Id)
        //{
        //    var data = repository.getFlow(Id);
        //    return data;
        //}

        //public MasterFlow GetFlowDataByID(long FlowID)
        //{
        //    var data = repository.GetFlowDataByID(FlowID);
        //    return data;
        //}

        //public long GetFlowIdByProccess(long flowProccesId)
        //{
        //    var data = repository.GetFlowIdByProccess(flowProccesId);
        //    return data;
        //}

        //public MasterFlowProcessStatus GetFlowProcessStatusDataByID(long id)
        //{
        //        var data = repository.GetFlowProcessStatusDataByID(id);
        //        return data;
        //}

        //public long getFlowProcessStatusIDByIsDraft(long FlowID)
        //{
        //    var data = repository.getFlowProcessStatusIDByIsDraft(FlowID);
        //    return data;
        //}

        //public long GetProcessFlowIdByIsStart(long flowId)
        //{
        //    var data = repository.GetProcessFlowIdByIsStart(flowId);
        //    return data;
        //}

        //public long getProcessFlowIDByNextFlow(long ProcessStatusFlowID)
        //{
        //    var data = repository.getProcessFlowIDByNextFlow(ProcessStatusFlowID);
        //    return data;
        //}

        //public List<MasterFlowProcessStatus> GetStatusByProcessId(long Id)
        //{
        //    var data = repository.GetStatusByProcessId(Id);
        //    return data;
        //}

        //public long getStatusFlowByNextFlow(long NextFlowProcessID, long FlowProcessID)
        //{
        //    var data = repository.getStatusFlowByNextFlow(NextFlowProcessID, FlowProcessID);
        //    return data;
        //}

        //public bool IsDraft(long flowProcessStatusId)
        //{
        //    var data = repository.IsDraft(flowProcessStatusId);
        //    return data;
        //}

        public string ToTitleCase(string text)
        {
            var data = repository.ToTitleCase(text);
            return data;
        }
    }
}
