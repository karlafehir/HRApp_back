﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRApp_back.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Added_Project_Entity_To_Employee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProjectId",
                table: "Employees",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Projects_ProjectId",
                table: "Employees",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Projects_ProjectId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ProjectId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Employees");
        }
    }
}
