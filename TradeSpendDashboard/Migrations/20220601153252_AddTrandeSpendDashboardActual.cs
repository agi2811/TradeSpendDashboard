using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeSpendDashboard.Migrations
{
    public partial class AddTrandeSpendDashboardActual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeHeadPrimarySalesActual",
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
                    table.PrimaryKey("PK_TradeHeadPrimarySalesActual", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeHeadSecondarySalesActual",
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
                    table.PrimaryKey("PK_TradeHeadSecondarySalesActual", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeHeadSpendingActual",
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
                    table.PrimaryKey("PK_TradeHeadSpendingActual", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeHeadPrimarySalesActual");

            migrationBuilder.DropTable(
                name: "TradeHeadSecondarySalesActual");

            migrationBuilder.DropTable(
                name: "TradeHeadSpendingActual");
        }
    }
}
