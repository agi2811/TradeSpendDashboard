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
    public class MasterRoleService : IMasterRoleService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterRoleRepository repository;
        private readonly IMapper mapper;

        public MasterRoleService(
            ILogger<MasterRoleService> logger,
            AppHelper appHelper,
            IMasterRoleRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterRole, MasterRoleDTO>();
                cfg.CreateMap<MasterRoleDTO, MasterRole>();
            });
            mapper = new Mapper(config);

            this.mapper = mapper;
        }

        public async Task<MasterRoleDTO> Add(MasterRoleDTO param)
        {
            try
            {
                var entity = new MasterRole();
                entity.Code = param.Code;
                entity.Name = param.Name;
                entity.CreatedBy = appHelper.UserName;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedBy = appHelper.UserName;
                entity.UpdatedDate = DateTime.Now;
                entity.IsActive = true;
                var result = await repository.Add(entity);
                var data = mapper.Map<MasterRoleDTO>(result);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error create Master Role", ex);
            }
        }

        public async Task<List<MasterRoleDTO>> GetAll(string key = "")
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                if (key != "")
                {
                    data = data.Where(a => a.Name.Contains(key)).ToList();
                }
                var dto = mapper.Map<List<MasterRoleDTO>>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        public async Task<List<MasterRoleDTO>> GetAll()
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                var dto = mapper.Map<List<MasterRoleDTO>>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        public async Task<PaginatedResult<MasterRoleDTO>> GetAll(PaginationParam paging)
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                var dto = mapper.Map<PaginatedResult<MasterRoleDTO>>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        public async Task<MasterRoleDTO> Get(long Id)
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                var dto = mapper.Map<MasterRoleDTO>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        public async Task<MasterRoleDTO> Delete(long Id)
        {
            try
            {
                var data = await repository.Delete(Id);
                var dto = mapper.Map<MasterRoleDTO>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        public async Task<MasterRoleDTO> Update(long id, MasterRoleDTO model)
        {
            try
            {
                var existingData = await repository.Get(id);
                existingData.UpdatedBy = appHelper.UserName;
                existingData.UpdatedDate = DateTime.Now;
                var result = await repository.Update(existingData);
                return this.mapper.Map<MasterRoleDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("error update MasterRole.", ex);
            }
        }

        public async Task<List<dynamic>> GetAllDynamic()
        {
            try
            {
                var data = await repository.GetAllDynamic();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        public async Task<dynamic> GetRoleOptionById(string search)
        {
            try
            {
                var data = await repository.GetRoleOptionById(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterRole.", ex);
            }
        }

        Task<PaginatedResult<MasterRoleDTO>> IService<MasterRoleDTO>.GetAll(PaginationParam param)
        {
            throw new NotImplementedException();
        }

        public  MasterRole GetById(long Id)
        {
            var data =  repository.GetById(Id);
            return data;
        }
    }
}
