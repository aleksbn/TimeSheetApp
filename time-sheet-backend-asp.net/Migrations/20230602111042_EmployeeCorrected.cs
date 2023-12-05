using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSheet_Backend.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyID",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "953d078a-9ebf-4795-97e5-8eb125d03177");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edb1fc5d-f4f9-49e7-b1e1-5318dafe8ded");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyID",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5a6b39a8-a3ff-4530-9955-0b31c19310bf", null, "manager", "COMPANY MANAGER" },
                    { "bf95ef5f-25f6-4dce-9e98-67fa78c9349a", null, "admin", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "0207987124935",
                column: "CompanyID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "0505991963258",
                column: "CompanyID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "0712992512497",
                column: "CompanyID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "0811993988659",
                column: "CompanyID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "0912986710077",
                column: "CompanyID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "2210986124578",
                column: "CompanyID",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyID",
                table: "Employees",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyID",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a6b39a8-a3ff-4530-9955-0b31c19310bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf95ef5f-25f6-4dce-9e98-67fa78c9349a");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyID",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "953d078a-9ebf-4795-97e5-8eb125d03177", null, "admin", "ADMINISTRATOR" },
                    { "edb1fc5d-f4f9-49e7-b1e1-5318dafe8ded", null, "manager", "COMPANY MANAGER" }
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "0207987124935",
                column: "CompanyID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "0505991963258",
                column: "CompanyID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "0712992512497",
                column: "CompanyID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "0811993988659",
                column: "CompanyID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "0912986710077",
                column: "CompanyID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: "2210986124578",
                column: "CompanyID",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyID",
                table: "Employees",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
