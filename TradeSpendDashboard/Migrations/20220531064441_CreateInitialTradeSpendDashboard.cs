using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeSpendDashboard.Migrations
{
    public partial class CreateInitialTradeSpendDashboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorycalContract",
                columns: table => new
                {
                    Area = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Zone = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Distributor = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Distributor_Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Outlet_ID = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Oulet_Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Product_Description = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Parent_SKU = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Price_BeforeTax = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    Price_AfterTax = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    Direct_Discount = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    Quarter_reward = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    QtyMonth = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    SalesPerMonth = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    BudgetDir_Disc_excPPn = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    BudgetQuar_Rwrd_ExcPPn = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    CR = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    Nama_BAS = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "MasterBudgetOwner",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    BudgetOwner = table.Column<string>(nullable: true),
                    ValueTradeSpend = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBudgetOwner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterCategory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Category = table.Column<string>(nullable: true),
                    ProfitCenterId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterChannel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Channel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterChannel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterCustomerMap",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    CustomerMap = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    OldChannelId = table.Column<long>(nullable: false),
                    NewChannelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCustomerMap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterGL",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    GLCode = table.Column<string>(nullable: true),
                    GLName = table.Column<string>(nullable: true),
                    GLDescription = table.Column<string>(nullable: true),
                    TypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterGL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterGLType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterGLType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterMenu",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IdParent = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Url = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    Icon = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterMenuRole",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    MenuId = table.Column<long>(nullable: false),
                    Read = table.Column<bool>(nullable: false),
                    Create = table.Column<bool>(nullable: false),
                    Update = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterMenuRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterProfitCenter",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ProfitCenter = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterProfitCenter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterRole",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UserCode = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(200)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterUsersRole",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UserCode = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterUsersRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempGuideline",
                columns: table => new
                {
                    GuidlineId = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    SubChannelId = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ProductGroupId = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Discount = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Reward = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TempSalesProjection",
                columns: table => new
                {
                    SalesProjectionId = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Distributor = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Outlet = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    SubChannel = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ProductGroup = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    GrossSales = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Discount = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Reward = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TradeHeadPrimarySalesOutlook",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Year = table.Column<string>(type: "NVARCHAR(4)", nullable: false),
                    Month = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeHeadPrimarySalesOutlook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeHeadSecondarySalesOutlook",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Year = table.Column<string>(type: "NVARCHAR(4)", nullable: false),
                    Month = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeHeadSecondarySalesOutlook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeHeadSpendingOutlook",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Year = table.Column<string>(type: "NVARCHAR(4)", nullable: false),
                    Month = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeHeadSpendingOutlook", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorycalContract");

            migrationBuilder.DropTable(
                name: "MasterBudgetOwner");

            migrationBuilder.DropTable(
                name: "MasterCategory");

            migrationBuilder.DropTable(
                name: "MasterChannel");

            migrationBuilder.DropTable(
                name: "MasterCustomerMap");

            migrationBuilder.DropTable(
                name: "MasterGL");

            migrationBuilder.DropTable(
                name: "MasterGLType");

            migrationBuilder.DropTable(
                name: "MasterMenu");

            migrationBuilder.DropTable(
                name: "MasterMenuRole");

            migrationBuilder.DropTable(
                name: "MasterProfitCenter");

            migrationBuilder.DropTable(
                name: "MasterRole");

            migrationBuilder.DropTable(
                name: "MasterUsers");

            migrationBuilder.DropTable(
                name: "MasterUsersRole");

            migrationBuilder.DropTable(
                name: "TempGuideline");

            migrationBuilder.DropTable(
                name: "TempSalesProjection");

            migrationBuilder.DropTable(
                name: "TradeHeadPrimarySalesOutlook");

            migrationBuilder.DropTable(
                name: "TradeHeadSecondarySalesOutlook");

            migrationBuilder.DropTable(
                name: "TradeHeadSpendingOutlook");
        }
    }
}
