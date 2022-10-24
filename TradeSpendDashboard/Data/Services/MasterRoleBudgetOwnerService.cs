using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.DTO;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services
{
    public class MasterRoleBudgetOwnerService : IMasterRoleBudgetOwnerService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterRoleBudgetOwnerRepository repository;
        private readonly IMapper mapper;

        public MasterRoleBudgetOwnerService(
            ILogger<MasterRoleBudgetOwnerService> logger,
            AppHelper appHelper,
            IMasterRoleBudgetOwnerRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterRoleBudgetOwner, MasterRoleBudgetOwnerDTO>();
                cfg.CreateMap<MasterRoleBudgetOwnerDTO, MasterRoleBudgetOwner>();
            });
            mapper = new Mapper(config);

            this.mapper = mapper;
        }

        public async Task<MasterRoleBudgetOwner> GetDataById(long Id)
        {
            var data = await repository.GetDataById(Id);
            return data;
        }

        public async Task<MasterRoleBudgetOwner> GetDataByRoleBOId(long roleId, long budgetOwnerId)
        {
            var data = await repository.GetDataByRoleBOId(roleId, budgetOwnerId);
            return data;
        }
    }
}
