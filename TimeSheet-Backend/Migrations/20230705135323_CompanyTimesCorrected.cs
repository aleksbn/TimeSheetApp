using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSheet_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CompanyTimesCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60aba34c-5d75-48cc-b064-3c2cb48a6fe8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab622168-76f0-4a55-8c92-3e87957460c1");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Companies",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Companies",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bbd3c39-a4d5-433f-8f59-4c8a55a90291", null, "manager", "COMPANY MANAGER" },
                    { "4c508de0-7fce-44b4-8809-9f8164193e11", null, "admin", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bbd3c39-a4d5-433f-8f59-4c8a55a90291");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c508de0-7fce-44b4-8809-9f8164193e11");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Companies",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Companies",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60aba34c-5d75-48cc-b064-3c2cb48a6fe8", null, "manager", "COMPANY MANAGER" },
                    { "ab622168-76f0-4a55-8c92-3e87957460c1", null, "admin", "ADMINISTRATOR" }
                });
        }
    }
}
