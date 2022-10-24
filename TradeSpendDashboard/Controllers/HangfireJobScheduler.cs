using Hangfire;
using System;

namespace TradeSpendDashboard.Controllers
{

    public class HangfireJobScheduler
    {
        private static string timeZoneId { get {return "SE Asia Standard Time"; } }
        public static void ScheduleRecurringJobs()
        {
            //RecurringJob.AddOrUpdate<GenerateClaimDailyClaimController>(nameof(GenerateClaimDailyClaimController), job => job.Run(JobCancellationToken.Null),"* 0 30 2 *", TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
            
        }
    }
}


