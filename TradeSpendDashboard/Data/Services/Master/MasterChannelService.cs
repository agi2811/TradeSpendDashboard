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
    public class MasterChannelService : IMasterChannelService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterChannelRepository repository;
        private readonly IMapper mapper;

        public MasterChannelService(
            ILogger<MasterChannelService> logger,
            AppHelper appHelper,
            IMasterChannelRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterChannel, MasterChannelDTO>();
                cfg.CreateMap<MasterChannelDTO, MasterChannel>();
            });
            mapper = new Mapper(config);
            this.mapper = mapper;
        }

        public async Task<MasterChannelDTO> Add(MasterChannelDTO model)
        {
            model.Id = 0;
            var entity = mapper.Map<MasterChannel>(model);
            entity.CreatedBy = appHelper.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = appHelper.UserName;
            entity.UpdatedDate = DateTime.Now;
            entity.IsActive = true;

            var result = await repository.Add(entity);
            return mapper.Map<MasterChannelDTO>(result);
        }

        public async Task<MasterChannelDTO> Delete(long id)
        {
            var data = await repository.Delete(id);
            var result = this.mapper.Map<MasterChannelDTO>(data);
            return result;
        }

        public async Task<MasterChannelDTO> Get(long id)
        {
            var data = await repository.Get(id);
            var result = this.mapper.Map<MasterChannelDTO>(data);
            return result;
        }

        public async Task<List<MasterChannelDTO>> GetAll()
        {
            var data = await repository.GetAll().ToListAsync();
            var result = mapper.Map<List<MasterChannelDTO>>(data);
            return result;
        }

        public Task<PaginatedResult<MasterChannelDTO>> GetAll(PaginationParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var data = await repository.GetByKey(key);
            return data;
        }

        public async Task<MasterChannelDTO> Update(long id, MasterChannelDTO entity)
        {
            var data = await repository.Get(id);
            data.Channel = entity.Channel;
            data.UpdatedBy = appHelper.UserName;
            data.UpdatedDate = DateTime.Now;
            var update = await repository.Update(data);
            var result = this.mapper.Map<MasterChannelDTO>(update);
            return result;
        }

        public async Task<List<MasterChannel>> GetByAllField(string channel)
        {
            var data = await repository.GetByAllField(channel);
            return data;
        }
    }
}
