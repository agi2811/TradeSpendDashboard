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
using TradeSpendDashboard.Models.Entity;

namespace TradeSpendDashboard.Data.Services
{
    public class MasterBudgetOwnerService : IMasterBudgetOwnerService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterBudgetOwnerRepository repository;
        private readonly IMapper mapper;

        public MasterBudgetOwnerService(
            ILogger<MasterBudgetOwnerService> logger,
            AppHelper appHelper,
            IMasterBudgetOwnerRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterBudgetOwner, MasterBudgetOwnerDTO>();
                cfg.CreateMap<MasterBudgetOwnerDTO, MasterBudgetOwner>();
            });
            mapper = new Mapper(config);
            this.mapper = mapper;
        }

        public async Task<MasterBudgetOwnerDTO> Add(MasterBudgetOwnerDTO model)
        {
            model.Id = 0;
            var entity = mapper.Map<MasterBudgetOwner>(model);
            entity.CreatedBy = appHelper.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = appHelper.UserName;
            entity.UpdatedDate = DateTime.Now;
            entity.IsActive = true;

            var result = await repository.Add(entity);
            return mapper.Map<MasterBudgetOwnerDTO>(result);
        }

        public async Task<MasterBudgetOwnerDTO> Delete(long id)
        {
            var data = await repository.Delete(id);
            var result = this.mapper.Map<MasterBudgetOwnerDTO>(data);
            return result;
        }

        public async Task<MasterBudgetOwnerDTO> Get(long id)
        {
            var data = await repository.Get(id);
            var result = this.mapper.Map<MasterBudgetOwnerDTO>(data);
            return result;
        }

        public async Task<List<MasterBudgetOwnerDTO>> GetAll()
        {
            var data = await repository.GetAll().ToListAsync();
            var result = mapper.Map<List<MasterBudgetOwnerDTO>>(data);
            return result;
        }

        public Task<PaginatedResult<MasterBudgetOwnerDTO>> GetAll(PaginationParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var data = await repository.GetByKey(key);
            return data;
        }

        public async Task<MasterBudgetOwnerDTO> Update(long id, MasterBudgetOwnerDTO entity)
        {
            var data = await repository.Get(id);
            data.BudgetOwner = entity.BudgetOwner;
            data.UpdatedBy = appHelper.UserName;
            data.UpdatedDate = DateTime.Now;
            var update = await repository.Update(data);
            var result = this.mapper.Map<MasterBudgetOwnerDTO>(update);
            return result;
        }

        public async Task<List<MasterBudgetOwner>> GetByAllField(string budgetOwner)
        {
            var data = await repository.GetByAllField(budgetOwner);
            return data;
        }
    }
}
