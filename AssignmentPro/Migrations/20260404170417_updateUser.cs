using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentPro.Migrations
{
    /// <inheritdoc />
    public partial class updateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "ImageUrl", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7db54da-5fae-49e0-941f-f26aad786ffe", new DateTime(2026, 4, 4, 23, 4, 17, 172, DateTimeKind.Local).AddTicks(1045), null, "AQAAAAIAAYagAAAAEPnjoqDc5GJC/rjj4JLqxgeJAjNuQeGcWePdC53w3MQAlE4z2iT/C+faMcapdydu1g==", "1190954a-0c37-4d97-ac20-4eb228347208" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "463bac72-2055-404d-9b13-9add07b8a781", new DateTime(2026, 3, 28, 21, 7, 31, 792, DateTimeKind.Local).AddTicks(9012), "AQAAAAIAAYagAAAAEOGL/ZvCcRsu5K23xXAPXBC47PMn+FApNPh6mD/kAPzIsY5PH+0gThCWDXGDMGM8/Q==", "bb071213-1762-4b92-80a6-3d07786c5452" });
        }
    }
}
