using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services
{
    public class MasterUsersRoleService : IMasterUsersRoleService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterUsersRoleRepository repository;
        private readonly IMapper mapper;

        public MasterUsersRoleService(
            ILogger<MasterUsersRoleService> logger,
            AppHelper appHelper,
            IMasterUsersRoleRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterUsersRole, MasterUsersRoleDTO>();
                cfg.CreateMap<MasterUsersRoleDTO, MasterUsersRole>();
            });
            mapper = new Mapper(config);

            this.mapper = mapper;
        }

        public async Task<MasterUsersRoleDTO> Add(MasterUsersRoleDTO param)
        {
            try
            {
                var entity = new MasterUsersRole();
                entity.UserCode = param.UserCode;
                entity.RoleId = param.RoleId;
                entity.CreatedBy = appHelper.UserName;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedBy = appHelper.UserName;
                entity.UpdatedDate = DateTime.Now;
                entity.IsActive = true;
                var result = await repository.Add(entity);
                var data = mapper.Map<MasterUsersRoleDTO>(result);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error create Master Role", ex);
            }
        }

        public async Task<List<MasterUsersRoleDTO>> GetAll()
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                var dto = mapper.Map<List<MasterUsersRoleDTO>>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        public async Task<PaginatedResult<MasterUsersRoleDTO>> GetAll(PaginationParam param)
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                var dto = mapper.Map<PaginatedResult<MasterUsersRoleDTO>>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        public async Task<MasterUsersRoleDTO> Get(long Id)
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                var dto = mapper.Map<MasterUsersRoleDTO>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        public MasterUsersRoleDTO GetByUserCode(string usercode)
        {
            try
            {
                var data = repository.Get(usercode);
                var dto = mapper.Map<MasterUsersRoleDTO>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        public async Task<MasterUsersRoleDTO> Delete(long Id)
        {
            try
            {
                var data = await repository.Delete(Id);
                var dto = mapper.Map<MasterUsersRoleDTO>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        public async Task<MasterUsersRoleDTO> Update(long id, MasterUsersRoleDTO model)
        {
            try
            {
                var existingData = await repository.Get(id);
                existingData.UpdatedBy = appHelper.UserName;
                existingData.UpdatedDate = DateTime.Now;
                var result = await repository.Update(existingData);
                return this.mapper.Map<MasterUsersRoleDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("error update MasterRole.", ex);
            }
        }
    }
}
