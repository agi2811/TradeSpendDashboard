using System.Threading.Tasks;

namespace TradeSpendDashboard.Services.Job.interfaces
{
    public interface IJobServices
    {
        public Task<string> CallAPI();
        public string Test();
    }
}