using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSheet_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CompanyWorkingTimeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a6b39a8-a3ff-4530-9955-0b31c19310bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf95ef5f-25f6-4dce-9e98-67fa78c9349a");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Companies",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Companies",
                type: "time",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60aba34c-5d75-48cc-b064-3c2cb48a6fe8", null, "manager", "COMPANY MANAGER" },
                    { "ab622168-76f0-4a55-8c92-3e87957460c1", null, "admin", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60aba34c-5d75-48cc-b064-3c2cb48a6fe8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab622168-76f0-4a55-8c92-3e87957460c1");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Companies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5a6b39a8-a3ff-4530-9955-0b31c19310bf", null, "manager", "COMPANY MANAGER" },
                    { "bf95ef5f-25f6-4dce-9e98-67fa78c9349a", null, "admin", "ADMINISTRATOR" }
                });
        }
    }
}
