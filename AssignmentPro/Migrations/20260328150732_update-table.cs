using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentPro.Migrations
{
    /// <inheritdoc />
    public partial class updatetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CGPA",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompletionYear",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PresentSalary",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResumePath",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "University",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationDate",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "ExpectedSalary",
                table: "Applications",
                newName: "ExpectionSalary");

            migrationBuilder.AddColumn<decimal>(
                name: "CGPA",
                table: "Applications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompletionYear",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "Applications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Applications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "Applications",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "463bac72-2055-404d-9b13-9add07b8a781", new DateTime(2026, 3, 28, 21, 7, 31, 792, DateTimeKind.Local).AddTicks(9012), "AQAAAAIAAYagAAAAEOGL/ZvCcRsu5K23xXAPXBC47PMn+FApNPh6mD/kAPzIsY5PH+0gThCWDXGDMGM8/Q==", "bb071213-1762-4b92-80a6-3d07786c5452" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CGPA",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CompletionYear",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "University",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "ExpectionSalary",
                table: "Applications",
                newName: "ExpectedSalary");

            migrationBuilder.AddColumn<decimal>(
                name: "CGPA",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompletionYear",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PresentSalary",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResumePath",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApplicationDate",
                table: "Applications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Applications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CGPA", "CompletionYear", "ConcurrencyStamp", "CreatedAt", "Degree", "Name", "PasswordHash", "PresentSalary", "ResumePath", "SecurityStamp", "University" },
                values: new object[] { null, null, "f4221585-6651-4c73-9202-94a316239323", new DateTime(2026, 3, 25, 9, 40, 21, 70, DateTimeKind.Local).AddTicks(871), "MSc in CSE", "Admin User", "AQAAAAIAAYagAAAAEL8klkqk4RROCCT/nfPCKxgE7nSXjBo+b3G3CHrSADvHsLaeDUon79MUsyrDVO/yZw==", null, "default.pdf", "f569b02e-9883-4c63-964e-42fa9281862b", "Dhaka University" });
        }
    }
}
