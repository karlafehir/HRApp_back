using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRApp_back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Added_EmployeeLeaveRecord_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualLeaveDays",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ManagerId",
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

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AnnualLeaveDays = table.Column<int>(type: "int", nullable: true),
                    SickLeaveDays = table.Column<int>(type: "int", nullable: true),
                    RemainingAnnualLeave = table.Column<int>(type: "int", nullable: true),
                    RemainingSickLeave = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveRecord_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveRecord_EmployeeId",
                table: "EmployeeLeaveRecord",
                column: "EmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeLeaveRecord");

            migrationBuilder.AddColumn<int>(
                name: "AnnualLeaveDays",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
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
    }
}
