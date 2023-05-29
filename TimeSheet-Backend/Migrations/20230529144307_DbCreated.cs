using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSheet_Backend.Migrations
{
    /// <inheritdoc />
    public partial class DbCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Departments_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartOfEmployment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourlyRate = table.Column<double>(type: "float", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WorkingTimes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(13)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingTimes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkingTimes_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "Address", "City", "Country", "Email", "Name" },
                values: new object[] { 1, "Street of the Unknown Hero 12", "Belgrade", "Serbia", "official@vixtra.com", "Vixtra Corporation" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "CompanyID", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Finances" },
                    { 2, 1, "Manufactoring" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Address", "CompanyID", "DateOfBirth", "Degree", "DepartmentID", "Email", "FirstName", "HourlyRate", "JobTitle", "LastName", "Phone", "StartOfEmployment" },
                values: new object[,]
                {
                    { "0207987124935", "Janka Veselinovica 20, Belgrade", 1, new DateTime(1987, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Information Technologies", 2, "covla@gmail.com", "Goran", 10.0, "QoS assurance manager", "Vlacic", "+381621549302", new DateTime(2020, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "0505991963258", "Ive Andrica 25, Belgrade", 1, new DateTime(1991, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master of Economy", 1, "jasnaiv@gmail.com", "Jasna", 7.0, "Accounty", "Ivkovic", "+38166419526", new DateTime(2017, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "0712992512497", "Nemanjina 110, Belgrade", 1, new DateTime(1992, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Graduated Economist", 1, "ivrank@gmail.com", "Ivana", 8.0, "Commercialist", "Rankovic", "+38160124578", new DateTime(2019, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "0811993988659", "Laye Kostica 182, Belgrade", 1, new DateTime(1993, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Organisational Sciences", 2, "makigavra@gmail.com", "Marija", 10.0, "Software Developer", "Gavranovic", "+381645123698", new DateTime(2013, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "0912986710077", "Dusana Baranina 1/C/10, Bijeljina", 1, new DateTime(1986, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Information Technologies", 1, "aleksbn@gmail.com", "Aleksandar", 12.0, "Director of Finances", "Matic", "+38765123789", new DateTime(2012, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "2210986124578", "Kirila i Metodija 29, Belgrade", 1, new DateTime(1986, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master of Information Technologies", 2, "mirko@gmail.com", "Mirko", 12.0, "Team Leader", "Simanic", "+38765142369", new DateTime(2013, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "WorkingTimes",
                columns: new[] { "ID", "Date", "EmployeeID", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912986710077", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 2, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912986710077", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 3, new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912986710077", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 4, new DateTime(2023, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912986710077", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 5, new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912986710077", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 6, new DateTime(2023, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "0712992512497", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 7, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "0712992512497", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 8, new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "0712992512497", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 9, new DateTime(2023, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "0712992512497", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 10, new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "0712992512497", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 11, new DateTime(2023, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "0505991963258", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 12, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "0505991963258", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 13, new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "0505991963258", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 14, new DateTime(2023, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "0505991963258", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 15, new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "0505991963258", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 16, new DateTime(2023, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "2210986124578", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 17, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "2210986124578", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 18, new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "2210986124578", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 19, new DateTime(2023, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "2210986124578", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 20, new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "2210986124578", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 21, new DateTime(2023, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "0207987124935", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 22, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "0207987124935", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 23, new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "0207987124935", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 24, new DateTime(2023, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "0207987124935", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 25, new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "0207987124935", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 26, new DateTime(2023, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "0811993988659", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 27, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "0811993988659", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 28, new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "0811993988659", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 29, new DateTime(2023, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "0811993988659", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { 30, new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "0811993988659", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyID",
                table: "Departments",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyID",
                table: "Employees",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentID",
                table: "Employees",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimes_EmployeeID",
                table: "WorkingTimes",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingTimes");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
