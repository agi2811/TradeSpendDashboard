using TradeSpendDashboard.Models.DTO.Identity;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Services.MasterData.interfaces
{
    public interface IUserServices : IApiService<UserData>
    {
        Task<RequestResponse<UserData>> PutUserAsync(string id, UserData model);
        Task<UserData> GetUserByUserCodeAsync(string userCode);
        Task<List<ApplicationUser>> GetUserByKey(PaginationParam param);
    }
}
