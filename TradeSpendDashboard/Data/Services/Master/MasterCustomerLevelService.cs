using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services
{
    public class MasterCustomerLevelService : IMasterCustomerLevelService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterCustomerLevelRepository repository;
        private readonly IMapper mapper;


        public MasterCustomerLevelService(
            ILogger<MasterCustomerLevelService> logger,
            AppHelper appHelper,
            IMasterCustomerLevelRepository repository,
            IMapper mapper
            )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterCustomerLevel, MasterCustomerLevelDTO>();
                cfg.CreateMap<MasterCustomerLevelDTO, MasterCustomerLevel>();
                cfg.CreateMap<PaginatedResult<MasterCustomerLevelDTO>, PaginatedResult<MasterCustomerLevel>>();
            });
            mapper = new Mapper(config);

            this.mapper = mapper;
        }

        public async Task<MasterCustomerLevelDTO> Add(MasterCustomerLevelDTO model)
        {
            try
            {
                var entity = mapper.Map<MasterCustomerLevel>(model);
                entity.CustomerLevel1 = model.CustomerLevel1;
                entity.CustomerId = model.CustomerId;
                entity.CreatedBy = appHelper.UserName;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedBy = appHelper.UserName;
                entity.UpdatedDate = DateTime.Now;
                entity.IsActive = true;

                var result = await repository.Add(entity);
                return mapper.Map<MasterCustomerLevelDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("error create Master Customer Level", ex);
            }
        }

        public async Task<dynamic> Get(string code)
        {
            try
            {
                var result = await repository.GetByCode(code);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("error get Master Customer Level.", ex);
            }
        }

        public async Task<List<MasterCustomerLevelDTO>> GetAll()
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                var dto = mapper.Map<List<MasterCustomerLevelDTO>>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Master Customer Level.", ex);
            }
        }

        public async Task<PaginatedResult<MasterCustomerLevelDTO>> GetAll(PaginationParam param)
        {
            try
            {
                var data = await repository.GetAll(param);
                var result = this.mapper.Map<PaginatedResult<MasterCustomerLevelDTO>>(data);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all pagination Master Customer Level with pagination.", ex);
            }
        }

        public async Task<PaginatedResult<MasterCustomerLevel>> GetAllCustom(PaginationParam param)
        {
            try
            {
                var data = await repository.GetAll(param);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all pagination Master Customer Level with pagination.", ex);
            }
        }

        public async Task<List<dynamic>> GetAllCustomOption(string search = "")
        {
            try
            {
                var data = await repository.GetAllDynamic(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all pagination Master Customer Level with pagination.", ex);
            }
        }

        public async Task<List<MasterCustomerLevel>> GetAllByUserCustomOption(string search = "")
        {
            try
            {
                var data = await repository.GetAll().Skip(0).Take(100).Where(a => a.CustomerLevel1.Contains(search)).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all pagination Master Customer Level with pagination.", ex);
            }
        }

        public async Task<dynamic> GetByCode(string code)
        {
            var data = await repository.GetByCode(code);
            return this.mapper.Map<MasterCustomerLevelDTO>(data);
        }

        public async Task<MasterCustomerLevelDTO> Get(long id)
        {
            var data = await repository.Get(id);
            return this.mapper.Map<MasterCustomerLevelDTO>(data);
        }

        public async Task<MasterCustomerLevelDTO> Update(long id, MasterCustomerLevelDTO model)
        {
            try
            {
                var entity = await repository.Get(id);
                entity.CustomerLevel1 = model.CustomerLevel1;
                entity.CustomerId = model.CustomerId;
                entity.UpdatedBy = appHelper.UserName;
                entity.UpdatedDate = DateTime.Now;
                entity.IsActive = true;

                var result = await repository.Update(entity);
                return mapper.Map<MasterCustomerLevelDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("error create Master Customer Level", ex);
            }
        }

        public async Task<MasterCustomerLevelDTO> Delete(long id)
        {
            var data = await repository.Delete(id);
            var result = this.mapper.Map<MasterCustomerLevelDTO>(data);
            return result;
        }

        public async Task<List<dynamic>> GetAllCustomerLevel()
        {
            var data = await repository.GetAllCustomerLevel();
            return data;
        }

        public async Task<List<MasterCustomerLevel>> GetByAllField(string customerLevel1)
        {
            var data = await repository.GetByAllField(customerLevel1);
            return data;
        }

        public async Task<List<dynamic>> GetCustomerOption(string search)
        {
            try
            {
                var data = await repository.GetCustomerOption(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Customer.", ex);
            }
        }

        public async Task<dynamic> GetCustomerOptionById(string search)
        {
            try
            {
                var data = await repository.GetCustomerOptionById(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Customer.", ex);
            }
        }

        public async Task<List<dynamic>> GetOldChannelOptionByCustomerId(long customerId)
        {
            try
            {
                var data = await repository.GetOldChannelOptionByCustomerId(customerId);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Old Channel.", ex);
            }
        }

        public async Task<List<dynamic>> GetNewChannelOptionByCustomerId(long customerId)
        {
            try
            {
                var data = await repository.GetNewChannelOptionByCustomerId(customerId);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all New Channel.", ex);
            }
        }
    }
}
