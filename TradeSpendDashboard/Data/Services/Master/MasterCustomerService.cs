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
    public class MasterCustomerService : IMasterCustomerService
    {
        private readonly ILogger logger;
        private readonly AppHelper appHelper;
        private readonly IMasterCustomerRepository repository;
        private readonly IMapper mapper;


        public MasterCustomerService(
            ILogger<MasterCustomerService> logger,
            AppHelper appHelper,
            IMasterCustomerRepository repository,
            IMapper mapper
            )
        {
            this.logger = logger;
            this.repository = repository;
            this.appHelper = appHelper;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterCustomerMap, MasterCustomerMapDTO>();
                cfg.CreateMap<MasterCustomerMapDTO, MasterCustomerMap>();
                cfg.CreateMap<PaginatedResult<MasterCustomerMapDTO>, PaginatedResult<MasterCustomerMap>>();
            });
            mapper = new Mapper(config);

            this.mapper = mapper;
        }

        public async Task<MasterCustomerMapDTO> Add(MasterCustomerMapDTO model)
        {
            try
            {
                var entity = mapper.Map<MasterCustomerMap>(model);
                entity.Customer = model.Customer;
                entity.CustomerMap = model.Customer;
                //entity.CustomerMap = model.CustomerMap;
                entity.OldChannelId = model.OldChannelId;
                entity.NewChannelId = model.NewChannelId;
                entity.CreatedBy = appHelper.UserName;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedBy = appHelper.UserName;
                entity.UpdatedDate = DateTime.Now;
                entity.IsActive = true;

                var result = await repository.Add(entity);
                return mapper.Map<MasterCustomerMapDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("error create Master Customer", ex);
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
                throw new Exception("error get Master Customer.", ex);
            }
        }

        public async Task<List<MasterCustomerMapDTO>> GetAll()
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                var dto = mapper.Map<List<MasterCustomerMapDTO>>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Master Customer.", ex);
            }
        }

        public async Task<PaginatedResult<MasterCustomerMapDTO>> GetAll(PaginationParam param)
        {
            try
            {
                var data = await repository.GetAll(param);
                var result = this.mapper.Map<PaginatedResult<MasterCustomerMapDTO>>(data);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all pagination Master Customer with pagination.", ex);
            }
        }

        public async Task<PaginatedResult<MasterCustomerMap>> GetAllCustom(PaginationParam param)
        {
            try
            {
                var data = await repository.GetAll(param);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all pagination Master Customer with pagination.", ex);
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
                throw new Exception("error get all pagination Master Customer with pagination.", ex);
            }
        }

        public async Task<List<MasterCustomerMap>> GetAllByUserCustomOption(string search = "")
        {
            try
            {
                var data = await repository.GetAll().Skip(0).Take(100).Where(a => a.Customer.Contains(search)).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all pagination Master Customer with pagination.", ex);
            }
        }

        public async Task<dynamic> GetByCode(string code)
        {
            var data = await repository.GetByCode(code);
            return this.mapper.Map<MasterCustomerMapDTO>(data);
        }

        public async Task<MasterCustomerMapDTO> Get(long id)
        {
            var data = await repository.Get(id);
            return this.mapper.Map<MasterCustomerMapDTO>(data);
        }

        public async Task<MasterCustomerMapDTO> Update(long id, MasterCustomerMapDTO model)
        {
            try
            {
                var entity = await repository.Get(id);
                entity.Customer = model.Customer;
                entity.CustomerMap = model.Customer;
                //entity.CustomerMap = model.CustomerMap;
                entity.OldChannelId = model.OldChannelId;
                entity.NewChannelId = model.NewChannelId;
                //entity.CreatedBy = appHelper.UserName;
                //entity.CreatedDate = DateTime.Now;
                entity.UpdatedBy = appHelper.UserName;
                entity.UpdatedDate = DateTime.Now;
                entity.IsActive = true;

                var result = await repository.Update(entity);
                return mapper.Map<MasterCustomerMapDTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("error create Master Customer", ex);
            }
        }

        public async Task<MasterCustomerMapDTO> Delete(long id)
        {
            var data = await repository.Get(id);
            if (data != null)
                data.IsActive = false;

            var update = await repository.Update(data);
            var result = this.mapper.Map<MasterCustomerMapDTO>(update);
            return result;
        }

        public async Task<MasterCustomerMapDTO> Activate(long id)
        {
            var data = await repository.GetAllById(id);
            if (data != null)
                data.IsActive = !data.IsActive;

            var update = await repository.Update(data);
            var result = this.mapper.Map<MasterCustomerMapDTO>(update);
            return result;
        }

        public async Task<List<MasterCustomerMap>> GetAllCustomer()
        {
            var data = await repository.GetAllCustomer().ToListAsync();
            var result = mapper.Map<List<MasterCustomerMap>>(data);
            return result;
        }

        public async Task<List<MasterCustomerMap>> GetByAllField(string customer)
        {
            var data = await repository.GetByAllField(customer);
            return data;
        }

        public async Task<List<dynamic>> GetChannelOption(string search)
        {
            try
            {
                var data = await repository.GetChannelOption(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Channel.", ex);
            }
        }

        public async Task<dynamic> GetChannelOptionById(string search)
        {
            try
            {
                var data = await repository.GetChannelOptionById(search);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all Channel.", ex);
            }
        }
    }
}
