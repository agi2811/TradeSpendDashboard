using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using TradeSpendDashboard.Services.MasterData.interfaces;
using System;
using System.Threading.Tasks;
using TradeSpendDashboard.Data.Services.Interface.Transaction;
using TradeSpendDashboard.Data.Repository.Interface;

namespace TradeSpendDashboard.Controllers
{
    public class ParameterController : BaseController
    {
        private readonly ILogger<ParameterController> _logger;
        private AppHelper _app;
        private IUserServices _userServices;
        private IMasterUsersService _masterUserServices;
        private IMasterRoleService _masterRoleServices;
        private IMasterRoleBudgetOwnerService _masterRoleBudgetOwnerService;
        private readonly IGlobalRepository _globalRepository;
        private readonly IUploadService _uploadService;
        private readonly IActualService _actualService;
        private readonly IUpdateService _updateService;
        private readonly IMapper mapper;

        public ParameterController(
            ILogger<ParameterController> logger,
            IUserServices userServices,
            IMasterRoleService masterRoleServices,
            IMasterUsersService masterUserServices,
            IMasterRoleBudgetOwnerService masterRoleBudgetOwnerService,
            IGlobalRepository globalRepository,
            IUploadService uploadService,
            IActualService actualService,
            IUpdateService updateService,
            IMapper mapper,
            IMasterMenuService masterMenu,
            AppHelper app,
            IWebHostEnvironment environment
        ) : base(logger, app, environment, masterMenu)
        {
            this._logger = logger;
            this._app = app;
            this._userServices = userServices;
            this._masterUserServices = masterUserServices;
            this._masterRoleServices = masterRoleServices;
            this._masterRoleBudgetOwnerService = masterRoleBudgetOwnerService;
            this._globalRepository = globalRepository;
            this._uploadService = uploadService;
            this._actualService = actualService;
            this._updateService = updateService;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MasterCustomerMap, MasterCustomerMapDTO>();
                cfg.CreateMap<MasterCustomerMapDTO, MasterCustomerMap>();
            });
            mapper = new Mapper(config);

            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetRoleOption(string search = "")
        {
            try
            {
                var data = await _masterRoleServices.GetAllDynamic();
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetRoleOptionById(string search = "")
        {
            try
            {
                var data = await _masterRoleServices.GetRoleOptionById(search);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetUserOption(PaginationParam param)
        {
            try
            {
                var data = await _userServices.GetUserByKey(param);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetDataRoleBudgetOwnerByRoleBOId(long roleId, long budgetOwnerId)
        {
            try
            {
                var dataByKey = await _masterRoleBudgetOwnerService.GetDataByRoleBOId(roleId, budgetOwnerId);
                return Ok(new { status = "success", result = dataByKey });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetYearsPrimarySalesOption(string key = "", string transaction = "")
        {
            try
            {
                var data = new object();
                if (transaction == "Upload")
                {
                    data = await _uploadService.GetYearsPrimarySalesUpload();
                }
                else if (transaction == "Actual")
                {
                    data = await _actualService.GetYearsPrimarySalesActual();
                }
                else if (transaction == "Update")
                {
                    data = await _updateService.GetYearsPrimarySalesUpdate();
                }

                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetYearsSecondarySalesOption(string key = "", string transaction = "")
        {
            try
            {
                var data = new object();
                if (transaction == "Upload")
                {
                    data = await _uploadService.GetYearsSecondarySalesUpload();
                }
                else if (transaction == "Actual")
                {
                    data = await _actualService.GetYearsSecondarySalesActual();
                }
                else if (transaction == "Update")
                {
                    data = await _updateService.GetYearsSecondarySalesUpdate();
                }

                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetYearsSpendingPhasingOption(string key = "", string transaction = "")
        {
            try
            {
                var data = new object();
                if (transaction == "Upload")
                {
                    data = await _uploadService.GetYearsSpendingPhasingUpload();
                }
                else if (transaction == "Actual")
                {
                    data = await _actualService.GetYearsSpendingPhasingActual();
                }
                else if (transaction == "Update")
                {
                    data = await _updateService.GetYearsSpendingPhasingUpdate();
                }

                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetMonthOption(int isBudget)
        {
            try
            {
                var data = await _globalRepository.GetMonth(isBudget);
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }

        public async Task<IActionResult> GetYearsReportOption()
        {
            try
            {
                var data = await _globalRepository.GetYearsReport();
                return Ok(new { status = "success", result = data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", result = "Cannot Get Data : " + ex.Message });
            }
        }
    }
}
