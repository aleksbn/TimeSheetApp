using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSheet_Backend.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokensAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14277e31-3df8-4884-8ee3-38dbe21d2a99");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e153cd6-9785-4d5b-9305-697bf0f09ab1");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateExpired = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_AppUser",
                        column: x => x.AppUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "953d078a-9ebf-4795-97e5-8eb125d03177", null, "admin", "ADMINISTRATOR" },
                    { "edb1fc5d-f4f9-49e7-b1e1-5318dafe8ded", null, "manager", "COMPANY MANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppUser",
                table: "RefreshTokens",
                column: "AppUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "953d078a-9ebf-4795-97e5-8eb125d03177");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edb1fc5d-f4f9-49e7-b1e1-5318dafe8ded");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14277e31-3df8-4884-8ee3-38dbe21d2a99", null, "admin", "ADMINISTRATOR" },
                    { "4e153cd6-9785-4d5b-9305-697bf0f09ab1", null, "manager", "COMPANY MANAGER" }
                });
        }
    }
}
