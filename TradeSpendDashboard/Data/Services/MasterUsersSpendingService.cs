using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Services.MasterData.interfaces;

namespace TradeSpendDashboard.Data.Services
{
    public class MasterUsersSpendingService : IMasterUsersSpendingService
    {
        private readonly ILogger _logger;
        private readonly AppHelper _appHelper;
        private readonly IMasterUsersSpendingRepository _repository;
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;
        public MasterUsersSpendingService(
            ILogger<MasterUsersSpendingService> logger,
            AppHelper appHelper,
            IMasterUsersSpendingRepository repository,
            IUserServices userServices,
            IMapper mapper
        )
        {
            this._logger = logger;
            this._repository = repository;
            this._appHelper = appHelper;
            this._userServices = userServices;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterUsersSpending, MasterUsersSpendingDTO>();
                cfg.CreateMap<MasterUsersSpendingDTO, MasterUsersSpending>();
            });
            mapper = new Mapper(config);

            this._mapper = mapper;
        }

        public async Task<dynamic> GetBOByUserCode(string userCode)
        {
            var data = await _repository.GetBOByUserCode(userCode);
            return data;
        }

        public async Task<List<dynamic>> GetByUserCode(string userCode)
        {
            var data = await _repository.GetByUserCode(userCode);
            return data;
        }

        public ValidationDTO SaveUsersSpending(string userCode, long budgetOwnerId, List<int> categoryList, List<int> profitCenterList)
        {
            List<int> distCategory = categoryList.Distinct().ToList();
            List<int> distprofitCenter = profitCenterList.Distinct().ToList();
            var update = _repository.SaveUsersSpending(userCode, budgetOwnerId, distCategory, distprofitCenter);
            return update;
        }
    }
}
