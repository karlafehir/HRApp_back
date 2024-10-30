using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRApp_back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class employee_add_holidays_sickdays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnnualLeaveDays",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RemainingAnnualLeave",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RemainingSickLeave",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SickLeaveDays",
                table: "Employees",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualLeaveDays",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RemainingAnnualLeave",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RemainingSickLeave",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SickLeaveDays",
                table: "Employees");
        }
    }
}
