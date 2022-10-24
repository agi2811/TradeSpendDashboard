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
    public class MasterCategoryMapService : IMasterCategoryMapService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterCategoryMapRepository repository;
        private readonly IMapper mapper;

        public MasterCategoryMapService(
            ILogger<MasterCategoryMapService> logger,
            AppHelper appHelper,
            IMasterCategoryMapRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterCategoryMap, MasterCategoryMapDTO>();
                cfg.CreateMap<MasterCategoryMapDTO, MasterCategoryMap>();
            });
            mapper = new Mapper(config);
            this.mapper = mapper;
        }

        public async Task<MasterCategoryMapDTO> Add(MasterCategoryMapDTO model)
        {
            model.Id = 0;
            var entity = mapper.Map<MasterCategoryMap>(model);
            entity.CreatedBy = appHelper.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = appHelper.UserName;
            entity.UpdatedDate = DateTime.Now;
            entity.IsActive = true;

            var result = await repository.Add(entity);
            return mapper.Map<MasterCategoryMapDTO>(result);
        }

        public async Task<MasterCategoryMapDTO> Delete(long id)
        {
            var data = await repository.Delete(id);
            var result = this.mapper.Map<MasterCategoryMapDTO>(data);
            return result;
        }

        public async Task<MasterCategoryMapDTO> Get(long id)
        {
            var data = await repository.Get(id);
            var result = this.mapper.Map<MasterCategoryMapDTO>(data);
            return result;
        }

        public async Task<List<MasterCategoryMapDTO>> GetAll()
        {
            var data = await repository.GetAll().ToListAsync();
            var result = mapper.Map<List<MasterCategoryMapDTO>>(data);
            return result;
        }

        public Task<PaginatedResult<MasterCategoryMapDTO>> GetAll(PaginationParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<List<dynamic>> GetAllData()
        {
            var data = await repository.GetAllData();
            return data;
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var data = await repository.GetByKey(key);
            return data;
        }

        public async Task<MasterCategoryMapDTO> Update(long id, MasterCategoryMapDTO entity)
        {
            var data = await repository.Get(id);
            data.CategoryWeb = entity.CategoryWeb;
            data.CategoryId = entity.CategoryId;
            data.UpdatedBy = appHelper.UserName;
            data.UpdatedDate = DateTime.Now;
            var update = await repository.Update(data);
            var result = this.mapper.Map<MasterCategoryMapDTO>(update);
            return result;
        }

        public async Task<List<dynamic>> GetCategoryOption(string search)
        {
            try
            {
                var data = await repository.GetCategoryOption(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Category.", ex);
            }
        }

        public async Task<dynamic> GetCategoryOptionById(string search)
        {
            try
            {
                var data = await repository.GetCategoryOptionById(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Category.", ex);
            }
        }

        public async Task<List<dynamic>> GetProfitCenterOptionByCategoryId(long categoryId)
        {
            try
            {
                var data = await repository.GetProfitCenterOptionByCategoryId(categoryId);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Profit Center.", ex);
            }
        }

        public async Task<List<MasterCategoryMap>> GetByAllField(string categoryWeb)
        {
            var data = await repository.GetByAllField(categoryWeb);
            return data;
        }
    }
}
