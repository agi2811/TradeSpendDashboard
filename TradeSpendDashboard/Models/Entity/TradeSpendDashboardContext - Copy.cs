using TradeSpend.Models.Entity.Flows;
using TradeSpend.Models.Entity.Master;
using TradeSpend.Models.Entity.Temp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TradeSpend.Models.Entity.TradeSpend.Transaction.Upload;

namespace TradeSpend.Models.Entity
{
    public partial class TradeSpendDashboardContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public TradeSpendDashboardContext(DbContextOptions<TradeSpendDashboardContext> options) : base(options)
        {
        }

        public TradeSpendDashboardContext(DbContextOptions<TradeSpendDashboardContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }
        // Start Master
        public virtual DbSet<TempGuideline> TempGuideline { get; set; }
        public virtual DbSet<TempSalesProjection> TempSalesProjection { get; set; }

        // Start Master
        public virtual DbSet<MasterUsers> MasterUsers { get; set; }
        public virtual DbSet<MasterRole> MasterRole { get; set; }
        public virtual DbSet<MasterUsersRole> MasterUsersRole { get; set; }
        public virtual DbSet<MasterMenu> MasterMenu { get; set; }
        public virtual DbSet<MasterMenuRole> MasterMenuRole { get; set; }
        public virtual DbSet<MasterCustomerMap> MasterCustomerMap { get; set; }
        public virtual DbSet<MasterChannel> MasterChannel { get; set; }
        public virtual DbSet<MasterBudgetOwner> MasterBudgetOwner { get; set; }
        public virtual DbSet<MasterProfitCenter> MasterProfitCenter { get; set; }
        public virtual DbSet<MasterCategory> MasterCategory { get; set; }
        public virtual DbSet<MasterGL> MasterGL { get; set; }
        public virtual DbSet<MasterGLType> MasterGLType { get; set; }
        // End Master

        // Start Request
        public virtual DbSet<TradeHeadPrimarySalesOutlook> TradeHeadPrimarySalesOutlook { get; set; }
        public virtual DbSet<TradeHeadSecondarySalesOutlook> TradeHeadSecondarySalesOutlook { get; set; }
        public virtual DbSet<TradeHeadSpendingOutlook> TradeHeadSpendingOutlook { get; set; }
        // End Request


        // Start Master Flow
        public virtual DbSet<HistorycalContract> HistorycalContract { get; set; }
        // End Master Flow

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            //.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TempGuideline>().HasNoKey();

            modelBuilder.Entity<TempSalesProjection>().HasNoKey();

            modelBuilder.Entity<HistorycalContract>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
