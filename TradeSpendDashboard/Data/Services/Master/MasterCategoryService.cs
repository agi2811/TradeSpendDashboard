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
    public class MasterCategoryService : IMasterCategoryService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterCategoryRepository repository;
        private readonly IMapper mapper;

        public MasterCategoryService(
            ILogger<MasterCategoryService> logger,
            AppHelper appHelper,
            IMasterCategoryRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterCategory, MasterCategoryDTO>();
                cfg.CreateMap<MasterCategoryDTO, MasterCategory>();
            });
            mapper = new Mapper(config);
            this.mapper = mapper;
        }

        public async Task<MasterCategoryDTO> Add(MasterCategoryDTO model)
        {
            model.Id = 0;
            var entity = mapper.Map<MasterCategory>(model);
            entity.CreatedBy = appHelper.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = appHelper.UserName;
            entity.UpdatedDate = DateTime.Now;
            entity.IsActive = true;

            var result = await repository.Add(entity);
            return mapper.Map<MasterCategoryDTO>(result);
        }

        public async Task<MasterCategoryDTO> Delete(long id)
        {
            var data = await repository.Delete(id);
            var result = this.mapper.Map<MasterCategoryDTO>(data);
            return result;
        }

        public async Task<MasterCategoryDTO> Get(long id)
        {
            var data = await repository.Get(id);
            var result = this.mapper.Map<MasterCategoryDTO>(data);
            return result;
        }

        public async Task<List<MasterCategoryDTO>> GetAll()
        {
            var data = await repository.GetAll().ToListAsync();
            var result = mapper.Map<List<MasterCategoryDTO>>(data);
            return result;
        }

        public Task<PaginatedResult<MasterCategoryDTO>> GetAll(PaginationParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var data = await repository.GetByKey(key);
            return data;
        }

        public async Task<MasterCategoryDTO> Update(long id, MasterCategoryDTO entity)
        {
            var data = await repository.Get(id);
            data.Category = entity.Category;
            data.ProfitCenterId = entity.ProfitCenterId;
            data.UpdatedBy = appHelper.UserName;
            data.UpdatedDate = DateTime.Now;
            var update = await repository.Update(data);
            var result = this.mapper.Map<MasterCategoryDTO>(update);
            return result;
        }

        public async Task<List<dynamic>> GetProfitCenterOption(string search)
        {
            try
            {
                var data = await repository.GetProfitCenterOption(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Profit Center.", ex);
            }
        }

        public async Task<dynamic> GetProfitCenterOptionById(string search)
        {
            try
            {
                var data = await repository.GetProfitCenterOptionById(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Profit Center.", ex);
            }
        }

        public async Task<List<MasterCategory>> GetByAllField(string category)
        {
            var data = await repository.GetByAllField(category);
            return data;
        }
    }
}
