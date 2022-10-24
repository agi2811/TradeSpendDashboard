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
    public class MasterProfitCenterService : IMasterProfitCenterService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterProfitCenterRepository repository;
        private readonly IMapper mapper;

        public MasterProfitCenterService(
            ILogger<MasterProfitCenterService> logger,
            AppHelper appHelper,
            IMasterProfitCenterRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterProfitCenter, MasterProfitCenterDTO>();
                cfg.CreateMap<MasterProfitCenterDTO, MasterProfitCenter>();
            });
            mapper = new Mapper(config);
            this.mapper = mapper;
        }

        public async Task<MasterProfitCenterDTO> Add(MasterProfitCenterDTO model)
        {
            model.Id = 0;
            var entity = mapper.Map<MasterProfitCenter>(model);
            entity.CreatedBy = appHelper.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = appHelper.UserName;
            entity.UpdatedDate = DateTime.Now;
            entity.IsActive = true;

            var result = await repository.Add(entity);
            return mapper.Map<MasterProfitCenterDTO>(result);
        }

        public async Task<MasterProfitCenterDTO> Delete(long id)
        {
            var data = await repository.Delete(id);
            var result = this.mapper.Map<MasterProfitCenterDTO>(data);
            return result;
        }

        public async Task<MasterProfitCenterDTO> Get(long id)
        {
            var data = await repository.Get(id);
            var result = this.mapper.Map<MasterProfitCenterDTO>(data);
            return result;
        }

        public async Task<List<MasterProfitCenterDTO>> GetAll()
        {
            var data = await repository.GetAll().ToListAsync();
            var result = mapper.Map<List<MasterProfitCenterDTO>>(data);
            return result;
        }

        public Task<PaginatedResult<MasterProfitCenterDTO>> GetAll(PaginationParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var data = await repository.GetByKey(key);
            return data;
        }

        public async Task<MasterProfitCenterDTO> Update(long id, MasterProfitCenterDTO entity)
        {
            var data = await repository.Get(id);
            data.ProfitCenter = entity.ProfitCenter;
            data.UpdatedBy = appHelper.UserName;
            data.UpdatedDate = DateTime.Now;
            var update = await repository.Update(data);
            var result = this.mapper.Map<MasterProfitCenterDTO>(update);
            return result;
        }

        public async Task<List<MasterProfitCenter>> GetByAllField(string profitCenter)
        {
            var data = await repository.GetByAllField(profitCenter);
            return data;
        }
    }
}
