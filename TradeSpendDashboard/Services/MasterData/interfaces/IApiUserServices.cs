using TradeSpendDashboard.Models.DTO.Identity;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Services.MasterData.interfaces
{
    public interface IApiUserServices : IApiService<UserData>
    {
        Task<RequestResponse<UserData>> PutUserAsync(string id, UserData model);
        Task<UserData> GetUserByUserCodeAsync(string userCode);
    }
}
