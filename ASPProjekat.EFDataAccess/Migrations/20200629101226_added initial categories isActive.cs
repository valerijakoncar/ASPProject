using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPProjekat.EFDataAccess.Migrations
{
    public partial class addedinitialcategoriesisActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsActive" },
                values: new object[] { new DateTime(2020, 6, 29, 12, 12, 22, 861, DateTimeKind.Local).AddTicks(334), true });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "IsActive" },
                values: new object[] { new DateTime(2020, 6, 29, 12, 12, 23, 2, DateTimeKind.Local).AddTicks(5720), true });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "IsActive" },
                values: new object[] { new DateTime(2020, 6, 29, 12, 12, 23, 2, DateTimeKind.Local).AddTicks(5918), true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsActive" },
                values: new object[] { new DateTime(2020, 6, 29, 12, 0, 31, 813, DateTimeKind.Local).AddTicks(705), false });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "IsActive" },
                values: new object[] { new DateTime(2020, 6, 29, 12, 0, 31, 940, DateTimeKind.Local).AddTicks(6801), false });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "IsActive" },
                values: new object[] { new DateTime(2020, 6, 29, 12, 0, 31, 940, DateTimeKind.Local).AddTicks(6999), false });
        }
    }
}
