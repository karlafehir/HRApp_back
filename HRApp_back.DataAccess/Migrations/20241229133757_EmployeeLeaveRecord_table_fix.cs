using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRApp_back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeLeaveRecord_table_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveRecord_Employees_EmployeeId",
                table: "EmployeeLeaveRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaveRecord",
                table: "EmployeeLeaveRecord");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaveRecord",
                newName: "EmployeeLeaveRecords");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaveRecord_EmployeeId",
                table: "EmployeeLeaveRecords",
                newName: "IX_EmployeeLeaveRecords_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaveRecords",
                table: "EmployeeLeaveRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveRecords_Employees_EmployeeId",
                table: "EmployeeLeaveRecords",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveRecords_Employees_EmployeeId",
                table: "EmployeeLeaveRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaveRecords",
                table: "EmployeeLeaveRecords");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaveRecords",
                newName: "EmployeeLeaveRecord");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaveRecords_EmployeeId",
                table: "EmployeeLeaveRecord",
                newName: "IX_EmployeeLeaveRecord_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaveRecord",
                table: "EmployeeLeaveRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveRecord_Employees_EmployeeId",
                table: "EmployeeLeaveRecord",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
