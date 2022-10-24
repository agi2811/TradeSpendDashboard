using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Entity.Transaction;
using TradeSpendDashboard.Models.Pagination;
using TradeSpendDashboard.Services.MasterData.interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services
{
    public class MasterUsersService : IMasterUsersService
    {
        private readonly ILogger _logger;
        private readonly AppHelper appHelper;
        private readonly IMasterUsersRepository repository;
        private readonly IMasterUsersRoleRepository _userRoleRepository;
        //private readonly IMasterUsersDistributorService _userDistributorRepository;
        private readonly IUserServices _userServices;
        private readonly IMapper mapper;

        public MasterUsersService(
            ILogger<MasterUsersService> logger,
            AppHelper appHelper,
            IMasterUsersRepository repository,
            IMasterUsersRoleRepository userRoleRepository,
            //IMasterUsersDistributorService userDistributorRepository,
            IUserServices userServices,
            IMapper mapper
        )
        {
            this._logger = logger;
            this._userRoleRepository = userRoleRepository;
            //this._userDistributorRepository = userDistributorRepository;
            this.repository = repository;
            this.appHelper = appHelper;
            this._userServices = userServices;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterUsers, MasterUsersDTO>();
                cfg.CreateMap<MasterUsersDTO, MasterUsers>();
                cfg.CreateMap<MasterUsersRole, MasterUsersRoleDTO>();
                cfg.CreateMap<MasterUsersRoleDTO, MasterUsersRole>();
            });
            mapper = new Mapper(config);

            this.mapper = mapper;
        }

        public async Task<MasterUsersDTO> SaveData(MasterUsersRoleDTO model)
        {
            try
            {
                var paramUser = new MasterUsersDTO();
                paramUser.UserCode = model.UserCode;
                var userData = await Add(paramUser);

                var dataUsersRole = new MasterUsersRole();
                dataUsersRole.RoleId = model.RoleId;
                dataUsersRole.UserCode = model.UserCode;
                dataUsersRole.CreatedBy = appHelper.UserName;
                dataUsersRole.CreatedDate = DateTime.Now;
                dataUsersRole.UpdatedBy = appHelper.UserName;
                dataUsersRole.UpdatedDate = DateTime.Now;
                dataUsersRole.IsActive = true;
                var roleData = await _userRoleRepository.Add(dataUsersRole);
                //var mapping = await _userDistributorRepository.GenerateMappingUserDistributor(model.UserCode);
                var data = mapper.Map<MasterUsersDTO>(userData);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error create MasterUsers", ex);
            }
        }


        public async Task<MasterUsersDTO> Add(MasterUsersDTO model)
        {
            try
            {
                var entity = new MasterUsers();
                var roleEntity = new MasterRole();
                var userRoleEntity = new MasterUsersRole();
                var detailDataDna = await _userServices.GetUserByUserCodeAsync(model.UserCode);
                // Insert data User 
                entity.UserCode = model.UserCode;
                entity.UserName = detailDataDna.UserName;
                entity.Name = detailDataDna.FullName;
                entity.Email = detailDataDna.Email;
                entity.CreatedBy = appHelper.UserName;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedBy = appHelper.UserName;
                entity.UpdatedDate = DateTime.Now;
                entity.IsActive = true;
                var result = await repository.Add(entity);
                var data = mapper.Map<MasterUsersDTO>(result);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("error create MasterUsers", ex);
            }
        }

        public async Task<List<MasterUsersDTO>> GetAll()
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                var dto = mapper.Map<List<MasterUsersDTO>>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterUsers.", ex);
            }
        }

        public List<dynamic> GetAllDynamic()
        {
            try
            {
                var data = repository.GetUsersAll();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error :", ex);
                _logger.LogDebug("Error debug : ", ex);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginatedResult<MasterUsersDTO>> GetAll(PaginationParam paging)
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                var dto = mapper.Map<PaginatedResult<MasterUsersDTO>>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterUsers.", ex);
            }
        }

        public async Task<MasterUsersDTO> Get(long Id)
        {
            try
            {
                var data = await repository.GetAll().ToListAsync();
                var dto = mapper.Map<MasterUsersDTO>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterUsers.", ex);
            }
        }

        public async Task<MasterUsersDTO> Delete(long Id)
        {
            try
            {
                var data = await repository.Delete(Id);
                var dto = mapper.Map<MasterUsersDTO>(data);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("error get all MasterUsers.", ex);
            }
        }

        public async Task<MasterUsersRoleDTO> Update(MasterUsersRoleDTO model)
        {
            try
            {
                var existingData = await _userRoleRepository.GetAsync(model.UserCode);
                if (existingData == null)
                {
                    var newData = new MasterUsersRole();
                    newData.RoleId = model.RoleId;
                    newData.UserCode = model.UserCode;
                    newData.CreatedBy = appHelper.UserName;
                    newData.CreatedDate = DateTime.Now;
                    newData.UpdatedBy = appHelper.UserName;
                    newData.UpdatedDate = DateTime.Now;
                    newData.IsActive = true;
                    var data = await _userRoleRepository.Add(newData);
                    //var mapping = await _userDistributorRepository.GenerateMappingUserDistributor(model.UserCode);
                    return this.mapper.Map<MasterUsersRoleDTO>(data);
                }
                else
                {
                    existingData.RoleId = model.RoleId;
                    existingData.UpdatedBy = appHelper.UserName;
                    existingData.UpdatedDate = DateTime.Now;
                    var result = await _userRoleRepository.Update(existingData);
                    //var mapping = await _userDistributorRepository.GenerateMappingUserDistributor(model.UserCode);
                    return this.mapper.Map<MasterUsersRoleDTO>(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error update MasterUsers.", ex);
            }
        }

        Task<PaginatedResult<MasterUsersDTO>> IService<MasterUsersDTO>.GetAll(PaginationParam param)
        {
            throw new NotImplementedException();
        }

        public Task<MasterUsersDTO> Update(long id, MasterUsersDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<MasterUsersDTO> GetByUserCode(string UserCode)
        {
            var data = await repository.Get(UserCode);
            var Data = this.mapper.Map<MasterUsersDTO>(data);
            return Data;
        }

        public async Task<bool> GenerateMappingAllUserData()
        {
            try
            {
                var verifyData = await repository.GetUsersSync();
                foreach (var value in verifyData)
                {
                    var userData = await _userServices.GetUserByUserCodeAsync(value.UserCode);
                    if (userData != null)
                    {
                        var newItem = new MasterUsers();
                        if (value.UserName != userData.UserName || value.Name != userData.FullName || value.Email != userData.Email)
                        {
                            newItem.UserCode = userData.UserCode;
                            newItem.UserName = userData.UserName;
                            newItem.Name = userData.FullName;
                            newItem.Email = userData.Email;
                            newItem.UpdatedBy = appHelper.UserName;
                            var result = await repository.UpdateSync(newItem);
                            var data = this.mapper.Map<MasterUsers>(result);
                        }
                    }
                    //var genUserDist = await _userDistributorRepository.GenerateMappingUserDistributor(value.UserCode);
                }
           
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in MasterUsers : ", ex);
                return false;
                throw new Exception("error create MasterUsers", ex);
            }
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
    }
}
