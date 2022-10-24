using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeSpendDashboard.Migrations
{
    public partial class AlterTableMasterRoleBudgetOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCategory",
                table: "MasterRoleBudgetOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProfitCenter",
                table: "MasterRoleBudgetOwner",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCategory",
                table: "MasterRoleBudgetOwner");

            migrationBuilder.DropColumn(
                name: "IsProfitCenter",
                table: "MasterRoleBudgetOwner");
        }
    }
}
