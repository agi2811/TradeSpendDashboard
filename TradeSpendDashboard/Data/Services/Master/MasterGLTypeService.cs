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
    public class MasterGLTypeService : IMasterGLTypeService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterGLTypeRepository repository;
        private readonly IMapper mapper;

        public MasterGLTypeService(
            ILogger<MasterGLTypeService> logger,
            AppHelper appHelper,
            IMasterGLTypeRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterGLType, MasterGLTypeDTO>();
                cfg.CreateMap<MasterGLTypeDTO, MasterGLType>();
            });
            mapper = new Mapper(config);
            this.mapper = mapper;
        }

        public async Task<MasterGLTypeDTO> Add(MasterGLTypeDTO model)
        {
            model.Id = 0;
            var entity = mapper.Map<MasterGLType>(model);
            entity.CreatedBy = appHelper.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = appHelper.UserName;
            entity.UpdatedDate = DateTime.Now;
            entity.IsActive = true;

            var result = await repository.Add(entity);
            return mapper.Map<MasterGLTypeDTO>(result);
        }

        public async Task<MasterGLTypeDTO> Delete(long id)
        {
            var data = await repository.Delete(id);
            var result = this.mapper.Map<MasterGLTypeDTO>(data);
            return result;
        }

        public async Task<MasterGLTypeDTO> Get(long id)
        {
            var data = await repository.Get(id);
            var result = this.mapper.Map<MasterGLTypeDTO>(data);
            return result;
        }

        public async Task<List<MasterGLTypeDTO>> GetAll()
        {
            var data = await repository.GetAll().ToListAsync();
            var result = mapper.Map<List<MasterGLTypeDTO>>(data);
            return result;
        }

        public Task<PaginatedResult<MasterGLTypeDTO>> GetAll(PaginationParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var data = await repository.GetByKey(key);
            return data;
        }

        public async Task<MasterGLTypeDTO> Update(long id, MasterGLTypeDTO entity)
        {
            var data = await repository.Get(id);
            data.Type = entity.Type;
            data.UpdatedBy = appHelper.UserName;
            data.UpdatedDate = DateTime.Now;
            var update = await repository.Update(data);
            var result = this.mapper.Map<MasterGLTypeDTO>(update);
            return result;
        }

        public async Task<List<MasterGLType>> GetByAllField(string channel)
        {
            var data = await repository.GetByAllField(channel);
            return data;
        }
    }
}
