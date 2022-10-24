using System;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Services.MasterData.interfaces
{
    public interface IApiGenerateClaimService
    {
        public Task<dynamic> Getdatagenerate(DateTime startDate);
    }
}