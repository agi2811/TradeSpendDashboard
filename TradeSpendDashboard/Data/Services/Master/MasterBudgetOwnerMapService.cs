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
    public class MasterBudgetOwnerMapService : IMasterBudgetOwnerMapService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterBudgetOwnerMapRepository repository;
        private readonly IMapper mapper;

        public MasterBudgetOwnerMapService(
            ILogger<MasterBudgetOwnerMapService> logger,
            AppHelper appHelper,
            IMasterBudgetOwnerMapRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterBudgetOwnerMap, MasterBudgetOwnerMapDTO>();
                cfg.CreateMap<MasterBudgetOwnerMapDTO, MasterBudgetOwnerMap>();
            });
            mapper = new Mapper(config);
            this.mapper = mapper;
        }

        public async Task<MasterBudgetOwnerMapDTO> Add(MasterBudgetOwnerMapDTO model)
        {
            model.Id = 0;
            var entity = mapper.Map<MasterBudgetOwnerMap>(model);
            entity.CreatedBy = appHelper.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = appHelper.UserName;
            entity.UpdatedDate = DateTime.Now;
            entity.IsActive = true;

            var result = await repository.Add(entity);
            return mapper.Map<MasterBudgetOwnerMapDTO>(result);
        }

        public async Task<MasterBudgetOwnerMapDTO> Delete(long id)
        {
            var data = await repository.Delete(id);
            var result = this.mapper.Map<MasterBudgetOwnerMapDTO>(data);
            return result;
        }

        public async Task<MasterBudgetOwnerMapDTO> Get(long id)
        {
            var data = await repository.Get(id);
            var result = this.mapper.Map<MasterBudgetOwnerMapDTO>(data);
            return result;
        }

        public async Task<List<MasterBudgetOwnerMapDTO>> GetAll()
        {
            var data = await repository.GetAll().ToListAsync();
            var result = mapper.Map<List<MasterBudgetOwnerMapDTO>>(data);
            return result;
        }

        public Task<PaginatedResult<MasterBudgetOwnerMapDTO>> GetAll(PaginationParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var data = await repository.GetByKey(key);
            return data;
        }

        public async Task<MasterBudgetOwnerMapDTO> Update(long id, MasterBudgetOwnerMapDTO entity)
        {
            var data = await repository.Get(id);
            data.ValueTradeSpend = entity.ValueTradeSpend;
            data.BudgetOwnerId = entity.BudgetOwnerId;
            data.UpdatedBy = appHelper.UserName;
            data.UpdatedDate = DateTime.Now;
            var update = await repository.Update(data);
            var result = this.mapper.Map<MasterBudgetOwnerMapDTO>(update);
            return result;
        }

        public async Task<List<dynamic>> GetBudgetOwnerOption(string search)
        {
            try
            {
                var data = await repository.GetBudgetOwnerOption(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Budget Owner.", ex);
            }
        }

        public async Task<List<dynamic>> GetBudgetOwnerOptionByRoleId(string search)
        {
            try
            {
                var data = await repository.GetBudgetOwnerOptionByRoleId(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Budget Owner.", ex);
            }
        }

        public async Task<dynamic> GetBudgetOwnerOptionById(string search)
        {
            try
            {
                var data = await repository.GetBudgetOwnerOptionById(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Budget Owner.", ex);
            }
        }

        public async Task<List<MasterBudgetOwnerMap>> GetByAllField(string valueTradeSpend)
        {
            var data = await repository.GetByAllField(valueTradeSpend);
            return data;
        }
    }
}
