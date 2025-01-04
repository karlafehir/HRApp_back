using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRApp_back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Added_ManagerId_To_Projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "managerId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "managerId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "Salary",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
