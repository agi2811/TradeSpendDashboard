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
    public class MasterGLService : IMasterGLService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterGLRepository repository;
        private readonly IMapper mapper;

        public MasterGLService(
            ILogger<MasterGLService> logger,
            AppHelper appHelper,
            IMasterGLRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterGL, MasterGLDTO>();
                cfg.CreateMap<MasterGLDTO, MasterGL>();
            });
            mapper = new Mapper(config);
            this.mapper = mapper;
        }

        public async Task<MasterGLDTO> Add(MasterGLDTO model)
        {
            model.Id = 0;
            var entity = mapper.Map<MasterGL>(model);
            entity.GLName = model.GLDescription;
            entity.CreatedBy = appHelper.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = appHelper.UserName;
            entity.UpdatedDate = DateTime.Now;
            entity.IsActive = true;

            var result = await repository.Add(entity);
            return mapper.Map<MasterGLDTO>(result);
        }

        public async Task<MasterGLDTO> Delete(long id)
        {
            var data = await repository.Delete(id);
            var result = this.mapper.Map<MasterGLDTO>(data);
            return result;
        }

        public async Task<MasterGLDTO> Get(long id)
        {
            var data = await repository.Get(id);
            var result = this.mapper.Map<MasterGLDTO>(data);
            return result;
        }

        public async Task<List<MasterGLDTO>> GetAll()
        {
            var data = await repository.GetAll().ToListAsync();
            var result = mapper.Map<List<MasterGLDTO>>(data);
            return result;
        }

        public Task<PaginatedResult<MasterGLDTO>> GetAll(PaginationParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var data = await repository.GetByKey(key);
            return data;
        }

        public async Task<MasterGLDTO> Update(long id, MasterGLDTO entity)
        {
            var data = await repository.Get(id);
            data.GLCode = entity.GLCode;
            //data.GLName = entity.GLName;
            data.GLName = entity.GLDescription;
            data.GLDescription = entity.GLDescription;
            data.TypeId = entity.TypeId;
            data.UpdatedBy = appHelper.UserName;
            data.UpdatedDate = DateTime.Now;
            var update = await repository.Update(data);
            var result = this.mapper.Map<MasterGLDTO>(update);
            return result;
        }

        public async Task<List<dynamic>> GetGLTypeOption(string search)
        {
            try
            {
                var data = await repository.GetGLTypeOption(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all GL Type.", ex);
            }
        }

        public async Task<dynamic> GetGLTypeOptionById(string search)
        {
            try
            {
                var data = await repository.GetGLTypeOptionById(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all GL Type.", ex);
            }
        }

        public async Task<List<MasterGL>> GetByAllField(string code)
        {
            var data = await repository.GetByAllField(code);
            return data;
        }
    }
}
